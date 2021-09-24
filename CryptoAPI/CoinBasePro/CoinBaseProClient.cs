namespace CryptoAPI.CoinBasePro
{
    using CoinbasePro;
    using CoinbasePro.Network.Authentication;
    using CoinbasePro.Services.Accounts;
    using CoinbasePro.Services.CoinbaseAccounts;
    using CoinbasePro.Services.Currencies;
    using CoinbasePro.Services.Deposits;
    using CoinbasePro.Services.Fees;
    using CoinbasePro.Services.Fills;
    using CoinbasePro.Services.Fundings;
    using CoinbasePro.Services.Limits;
    using CoinbasePro.Services.Orders;
    using CoinbasePro.Services.Payments;
    using CoinbasePro.Services.Products;
    using CoinbasePro.Services.Profiles;
    using CoinbasePro.Services.Reports;
    using CoinbasePro.Services.StablecoinConversions;
    using CoinbasePro.Services.UserAccount;
    using CoinbasePro.Services.Withdrawals;
    using CoinbasePro.WebSocket;

    public class CoinbaseProClient : ICoinbaseProClient
    {
        private readonly CoinbasePro.CoinbaseProClient client;

        public CoinbaseProClient()
        {
            var passPhrase = @"wp26jkwciis";
            var secretKey = @"5rdGklDkNNHzChYiH1MLEYMpUFg4mMLCw7YBObya4kKfYw848u2VrnMNq6PTI7hVFNWBUc9Z7Hb0lWJCJrAWQw==";
            var apiKey = @"db38607c05687aa61ea5bb8f80cc07ae";

            var authenticator = new Authenticator(passPhrase, secretKey, apiKey);

            client = new CoinbasePro.CoinbaseProClient(authenticator, true);
        }

        public IAccountsService AccountsService => client.AccountsService;

        public ICoinbaseAccountsService CoinbaseAccountsService => client.CoinbaseAccountsService;

        public IOrdersService OrdersService => client.OrdersService;

        public IPaymentsService PaymentsService => client.PaymentsService;

        public IWithdrawalsService WithdrawalsService => client.WithdrawalsService;

        public IDepositsService DepositsService => client.DepositsService;

        public IProductsService ProductsService => client.ProductsService;

        public ICurrenciesService CurrenciesService => client.CurrenciesService;

        public IFillsService FillsService => client.FillsService;

        public IFundingsService FundingsService => client.FundingsService;

        public IReportsService ReportsService => client.ReportsService;

        public IUserAccountService UserAccountService => client.UserAccountService;

        public IStablecoinConversionsService StablecoinConversionsService => client.StablecoinConversionsService;

        public IFeesService FeesService => client.FeesService;

        public IProfilesService ProfilesService => client.ProfilesService;

        public ILimitsService LimitsService => client.LimitsService;

        public IWebSocket WebSocket => client.WebSocket;
    }
}
