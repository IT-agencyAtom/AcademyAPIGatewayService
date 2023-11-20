using CrmIntegration.Adapters.Altegio.Models;

namespace CrmIntegration.Adapters.Altegio
{
    public interface IAltegioAdapter
    {
        Task<LoginOutput> LoginAsync(LoginInput input, CancellationToken cancellationToken = default);
        Task<IList<ClientOutput>> GetClientsAsync(GetClientsInput input, CancellationToken cancellationToken = default);
        Task<ClientOutput> GetClientAsync(GetClientInput input, CancellationToken cancellationToken = default);
        Task UpdateClientAsync(UpdateClientInput input, CancellationToken cancellationToken = default);
        Task<ClientOutput> CreateClientAsync(CreateClientInput input, CancellationToken cancellationToken = default);
        Task<IList<CustomerSubscriptionOutput>> GetCustomerSubscriptionsAsync(CustomerSubscriptionInput input, CancellationToken cancellationToken = default);
        Task CreateGoodsTransactionAsync(CreateGoodsTransactionInput input, CancellationToken cancellationToken = default);
        Task<CreateDocumentOutput> CreateDocumentAsync(CreateDocumentInput input, CancellationToken cancellationToken = default);
    }
}
