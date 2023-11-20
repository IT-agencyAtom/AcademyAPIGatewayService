using CrmIntegration.Adapters.Kommo.Models;

namespace CrmIntegration.Adapters.Kommo
{
    public interface IKommoAdapter
    {
        Task<AccessTokenOutput> GetTokensAsync(AccessTokenInput input, CancellationToken cancellationToken = default);
        Task<IList<ContactOutput>> GetContactsAsync(GetContactsInput input, CancellationToken cancellationToken = default);
        Task<ContactOutput> GetContactByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<LeadOutput> GetLeadByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IList<LeadOutput>> GetLeadsAsync(GetLeadsInput input, CancellationToken cancellationToken = default);
        Task UpdateContactAsync(UpdateContactInput input, CancellationToken cancellationToken = default);
        Task<IList<CreateComplexLeadOutput>> CreateComplexLeadAsync(IList<CreateComplexLeadInput> input, CancellationToken cancellationToken = default);
        Task CreateCatalogElementsAsync(CreateCatalogElementsInput input, CancellationToken cancellationToken = default);
        Task CreateTaskAsync(IList<CreateTaskInput> input, CancellationToken cancellationToken = default);
        Task<CatalogElementOutput> GetCatalogElementAsync(GetCatalogElementInput input, CancellationToken cancellationToken = default);
    }
}
