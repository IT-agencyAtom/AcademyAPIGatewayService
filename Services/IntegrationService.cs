using CrmIntegration.Adapters.Altegio;
using CrmIntegration.Adapters.Altegio.Models;
using CrmIntegration.Adapters.Kommo;
using CrmIntegration.Adapters.Kommo.Models;
using CrmIntegration.Options;
using CrmIntegration.Services.Models;
using CrmIntegration.Utils;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CrmIntegration.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IKommoAdapter _kommoAdapter;
        private readonly IAltegioAdapter _altegioAdapter;
        private readonly AltegioIntegrationOptions _altegioIntegrationOptions;
        private readonly KommoIntegrationOptions _kommoIntegrationOptions;

        public IntegrationService(IKommoAdapter kommoAdapter,
            IAltegioAdapter altegioAdapter,
            IOptions<AltegioIntegrationOptions> altegioIntegrationOptions,
            IOptions<KommoIntegrationOptions> kommoIntegrationOptions)
        {
            _kommoAdapter = kommoAdapter;
            _altegioAdapter = altegioAdapter;
            _altegioIntegrationOptions = altegioIntegrationOptions.Value;
            _kommoIntegrationOptions = kommoIntegrationOptions.Value;
        }

        private UpdateContactInput GetPreparedUpdateContactInput(int mainContactId, long clientId)
        {
            var input = new UpdateContactInput
            {
                Id = mainContactId,
                CustomFieldsValues = new List<UpdateContactCustomFieldsValue>
                {
                    new UpdateContactCustomFieldsValue
                    {
                        FieldId = _kommoIntegrationOptions.FieldIds[FieldCodes.AltegioCustomFieldId],
                        Values = new List<UpdateContactCustomFieldValue>
                        {
                            new UpdateContactCustomFieldValue
                            {
                                Value = clientId.ToString()
                            }
                        }
                    }
                }
            };
            return input;
        }

        private List<CreateComplexLeadInput> GetPreparedComplexLeadInput(HandleBookingCreatedInput input)
        {
            var createComplexLeadInput = new List<CreateComplexLeadInput>
            {
                new CreateComplexLeadInput
                {
                    Name = $"Altegio leads #{input.ClientId}",
                    PipelineId = _kommoIntegrationOptions.FieldIds[FieldCodes.PipelineId],
                    StatusId = _kommoIntegrationOptions.FieldIds[FieldCodes.NewLeadStatusId],
                    Embedded = new ComplexLeadEmbedded
                    {
                        Contacts = new List<ComplexLeadContact>
                        {
                            new ComplexLeadContact
                            {
                                FirstName = input.Name,
                                CustomFieldsValues = new List<ComplexLeadCustomFieldsValue>
                                {
                                    new ComplexLeadCustomFieldsValue
                                    {
                                        FieldCode = FieldCodes.Phone,
                                        Values = new List<ComplexLeadValue>
                                        {
                                            new ComplexLeadValue
                                            {
                                                EnumCode = EnumCodes.Mobile,
                                                Value = input.Phone
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return createComplexLeadInput;
        }

        private CreateCatalogElementsInput GetPreparedCatalogElementsInput(HandleBookingCreatedInput input, long contactId)
        {
            var createCatalogElementsInput = new CreateCatalogElementsInput
            {
                CatalogId = _kommoIntegrationOptions.FieldIds[FieldCodes.InvoiceCatalogId],
                Items = new List<CreateCatalogElementsListItem>
                {
                    new CreateCatalogElementsListItem
                    {
                        CustomFieldsValues = new List<CreateCatalogElementsCustomFieldsValue>
                        {
                            new CreateCatalogElementsCustomFieldsValue
                            {
                                FieldId = _kommoIntegrationOptions.FieldIds[FieldCodes.BillStatus],
                                Values = new List<CreateCatalogElementsValue>
                                {
                                    new CreateCatalogElementsValue
                                    {
                                        EnumId = _kommoIntegrationOptions.FieldIds[FieldCodes.EnumPaid]
                                    }
                                }
                            },
                            new CreateCatalogElementsCustomFieldsValue
                            {
                                FieldId = _kommoIntegrationOptions.FieldIds[FieldCodes.Payer],
                                Values = new List<CreateCatalogElementsValue>
                                {
                                    new CreateCatalogElementsValue
                                    {
                                        Value = new CatalogEntityItem
                                        {
                                            EntityType = "contacts",
                                            EntityId = contactId
                                        }
                                    }
                                }
                            },
                            new CreateCatalogElementsCustomFieldsValue
                            {
                                FieldId = _kommoIntegrationOptions.FieldIds[FieldCodes.BillPaymentDate],
                                Values = new List<CreateCatalogElementsValue>
                                {
                                    new CreateCatalogElementsValue
                                    {
                                        Value = DateTime.UtcNow.ToEpoch()
                                    }
                                }
                            },
                            new CreateCatalogElementsCustomFieldsValue
                            {
                                FieldId = _kommoIntegrationOptions.FieldIds[FieldCodes.Items],
                                Values = new List<CreateCatalogElementsValue>
                                {
                                    new CreateCatalogElementsValue
                                    {
                                        Value = new CatalogInvoiceItem
                                        {
                                            ProductId = input.Service.Id,
                                            Description = input.Service.Title,
                                            UnitPrice = input.Service.CostPerUnit,
                                            Quantity = input.Service.Amount
                                        }
                                    }
                                }
                            },
                            new CreateCatalogElementsCustomFieldsValue
                            {
                                FieldId = _kommoIntegrationOptions.FieldIds[FieldCodes.BillPrice],
                                Values = new List<CreateCatalogElementsValue>
                                {
                                    new CreateCatalogElementsValue
                                    {
                                        Value = input.Service.Cost
                                    }
                                }
                            },
                            new CreateCatalogElementsCustomFieldsValue
                            {
                                FieldId = _kommoIntegrationOptions.FieldIds[FieldCodes.LinkedEntity],
                                Values = new List<CreateCatalogElementsValue>
                                {
                                    new CreateCatalogElementsValue
                                    {
                                        Value = new CatalogEntityItem
                                        {
                                            EntityType = "contacts",
                                            EntityId = contactId
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return createCatalogElementsInput;
        }

        private async Task<long?> GetGoodsId(long productId, CancellationToken cancellationToken = default)
        {
            var getCatalogElementInput = new GetCatalogElementInput
            {
                CatalogId = _kommoIntegrationOptions.FieldIds[FieldCodes.ProductCatalogId],
                ElementId = productId
            };
            var product = await _kommoAdapter.GetCatalogElementAsync(getCatalogElementInput, cancellationToken);
            var externalId = product?.CustomFieldsValues?
                .FirstOrDefault(p => p.FieldCode == FieldCodes.ExternalId).Values?
                .FirstOrDefault()?.Value;

            if (long.TryParse(externalId, out long goodsId))
            {
                return goodsId;
            }

            return null;
        }

        public async Task HandleLeadStatusChangedAsync(HandleLeadStatusChangedInput input, CancellationToken cancellationToken = default)
        {
            if (input.NewStatusId != _kommoIntegrationOptions.FieldIds[FieldCodes.ExpectedStatusId])
            {
                return;
            }

            var lead = await _kommoAdapter.GetLeadByIdAsync(input.LeadId, cancellationToken);
            if (lead == null)
            {
                Debug.WriteLine($"No lead found for {input.LeadId} id.");
                return;
            }

            if (lead?.Embedded?.Contacts?.Any() != true)
            {
                Debug.WriteLine($"No lead contacts found for {input.LeadId} id.");
                return;
            }

            var leadMainContact = lead?.Embedded?.Contacts?.FirstOrDefault(p => p.IsMain);
            if (leadMainContact == null)
            {
                Debug.WriteLine($"No main lead contact found for {input.LeadId} id.");
                return;
            }

            var mainContact = await _kommoAdapter.GetContactByIdAsync(leadMainContact.Id, cancellationToken);
            if (mainContact == null)
            {
                Debug.WriteLine($"No contact found for {mainContact.Id} id.");
                return;
            }

            var phones = mainContact.CustomFieldsValues
                .FirstOrDefault(p => p.FieldCode == FieldCodes.Phone)?.Values?
                .OrderByDescending(p => p.EnumCode == "MOB");
            if (phones?.Any() != true)
            {
                Debug.WriteLine($"No phone found for {mainContact.Id} id.");
                return;
            }

            var clientInput = new GetClientsInput
            {
                CompanyId = _altegioIntegrationOptions.CompanyId,
                Operation = Operations.Or,
                Fields = new string[] { "id", "name", "phone" },
                Filters = new List<ClientFilter>()
            };

            foreach (var phone in phones)
            {
                clientInput.Filters.Add(new ClientFilter
                {
                    Type = FilterTypes.QuickSearch,
                    State = new ClientFilterState
                    {
                        Value = phone.Value
                    }
                });
            }

            var updateClientInput = new UpdateClientInput
            {
                CompanyId = _altegioIntegrationOptions.CompanyId,
                Name = mainContact.Name,
                Phone = phones.FirstOrDefault().Value,
                CustomFields = new Dictionary<string, string>
                {
                    { _altegioIntegrationOptions.KommoCustomFieldCode, mainContact.Id.ToString() }
                }
            };

            var clients = await _altegioAdapter.GetClientsAsync(clientInput, cancellationToken);
            if (clients?.Any() == true)
            {
                foreach (var client in clients)
                {
                    updateClientInput.Id = client.Id;
                    await _altegioAdapter.UpdateClientAsync(updateClientInput, cancellationToken);

                    var updateContactInput = GetPreparedUpdateContactInput(mainContact.Id, client.Id);
                    await _kommoAdapter.UpdateContactAsync(updateContactInput, cancellationToken);
                }
            }
            else
            {
                CreateClientInput createClientInput = updateClientInput;
                var client = await _altegioAdapter.CreateClientAsync(createClientInput, cancellationToken);

                var updateContactInput = GetPreparedUpdateContactInput(mainContact.Id, client.Id);
                await _kommoAdapter.UpdateContactAsync(updateContactInput, cancellationToken);
            }
        }

        public async Task HandleBookingCreatedAsync(HandleBookingCreatedInput input, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(input.Phone))
            {
                Debug.WriteLine("Phone required.");
                return;
            }

            var customerSubscriptionInput = new CustomerSubscriptionInput
            {
                CompanyId = _altegioIntegrationOptions.CompanyId,
                Phone = input.Phone
            };
            var customerSubscriptions = await _altegioAdapter.GetCustomerSubscriptionsAsync(customerSubscriptionInput, cancellationToken);
            if (customerSubscriptions?.Any() == true)
            {
                Debug.WriteLine("User has a subscription.");
                return;
            }

            var getClientInput = new GetClientInput
            {
                CompanyId = _altegioIntegrationOptions.CompanyId,
                Id = input.ClientId
            };
            var client = await _altegioAdapter.GetClientAsync(getClientInput, cancellationToken);
            long? leadId = null;

            if (client?.CustomFields?.TryGetValue(_altegioIntegrationOptions.KommoCustomFieldCode, out long contactId) != true)
            {
                var getContactsInput = new GetContactsInput
                {
                    Query = input.Phone,
                    With = "leads"
                };
                var contacts = await _kommoAdapter.GetContactsAsync(getContactsInput, cancellationToken);

                if (contacts?.Any() == true)
                {
                    var firstContact = contacts.FirstOrDefault();
                    contactId = firstContact.Id;
                    leadId = firstContact.Embedded?.Leads?.FirstOrDefault()?.Id;
                }
                else
                {
                    var createComplexLeadInput = GetPreparedComplexLeadInput(input);
                    var complexLead = await _kommoAdapter.CreateComplexLeadAsync(createComplexLeadInput, cancellationToken);
                    if (complexLead == null)
                    {
                        Debug.WriteLine("Creat lead failed.");
                        return;
                    }

                    var firstLead = complexLead.FirstOrDefault();
                    contactId = firstLead.ContactId;
                    leadId = firstLead.Id;
                }

                var updateClientInput = new UpdateClientInput
                {
                    Id = input.ClientId,
                    CompanyId = _altegioIntegrationOptions.CompanyId,
                    Name = client.Name,
                    Phone = client.Phone,
                    CustomFields = new Dictionary<string, string>
                    {
                        { _altegioIntegrationOptions.KommoCustomFieldCode, contactId.ToString() }
                    }
                };

                await _altegioAdapter.UpdateClientAsync(updateClientInput, cancellationToken);
            }

            if (leadId == null)
            {
                var getLeadsInput = new GetLeadsInput
                {
                    Query = contactId.ToString(),
                    With = "contacts"
                };
                var leads = await _kommoAdapter.GetLeadsAsync(getLeadsInput, cancellationToken);
                if (leads?.Any() != true)
                {
                    Debug.WriteLine($"Lead not found for ContactId: {contactId}");
                    return;
                }

                leadId = leads.FirstOrDefault().Id;
            }

            var createCatalogElementsInput = GetPreparedCatalogElementsInput(input, contactId);
            await _kommoAdapter.CreateCatalogElementsAsync(createCatalogElementsInput, cancellationToken);

            var createTaskInput = new List<CreateTaskInput>
            {
                new CreateTaskInput
                {
                    CompleteTill = DateTime.UtcNow.Add(_kommoIntegrationOptions.TaskCompleteTill).ToEpoch(),
                    Text = _kommoIntegrationOptions.TaskTitle,
                    EntityType = "leads",
                    EntityId = leadId.Value,
                    TaskTypeId = 1
                }
            };
            await _kommoAdapter.CreateTaskAsync(createTaskInput, cancellationToken);
        }

        public async Task HandleInvoiceCreatedAsync(HandleInvoiceCreatedInput input, CancellationToken cancellationToken = default)
        {
            if (input.UnitPrice <= 1000)
            {
                return;
            }

            var contact = await _kommoAdapter.GetContactByIdAsync(input.ContactId, cancellationToken);
            var altegioIdValue = contact?.CustomFieldsValues?
                .FirstOrDefault(p => p.FieldId == _kommoIntegrationOptions.FieldIds[FieldCodes.AltegioCustomFieldId])?.Values?
                .FirstOrDefault()?.Value;
            if (!long.TryParse(altegioIdValue, out long altegioId))
            {
                Debug.WriteLine("AltegioId is empty.");
                return;
            }

            var goodsId = await GetGoodsId(input.ProductId, cancellationToken);
            if (goodsId == null)
            {
                Debug.WriteLine("Altegio.GoodsId is empty.");
                return;
            }

            var createDocumentInput = new CreateDocumentInput
            {
                CompanyId = _altegioIntegrationOptions.CompanyId,
                CreateDate = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                StorageId = _altegioIntegrationOptions.GoodsStorageId,
                TypeId = (int)DocumentTypes.Sale
            };
            var document = await _altegioAdapter.CreateDocumentAsync(createDocumentInput, cancellationToken);
            if (document == null)
            {
                Debug.WriteLine("Create document failed.");
                return;
            }

            var createGoodsTransactionInput = new CreateGoodsTransactionInput
            {
                CompanyId = _altegioIntegrationOptions.CompanyId,
                ClientId = altegioId,
                GoodId = goodsId.Value,
                OperationUnitType = (int)OperationUnitTypes.ForSale,
                Cost = input.Cost,
                Amount = input.Quantity,
                CostPerUnit = input.UnitPrice,
                Discount = input.Discount,
                DocumentId = document.Id
            };
            await _altegioAdapter.CreateGoodsTransactionAsync(createGoodsTransactionInput, cancellationToken);
        }
    }
}
