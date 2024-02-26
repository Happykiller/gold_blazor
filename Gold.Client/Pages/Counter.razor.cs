using Gold.Client.Usecase;
using Gold.Usecase;
using static Gold.Client.Usecase.GetOperationsUsecase;

namespace Gold.Client.Pages
{
    public partial class Counter
    {
        private bool getAccountError;
        private bool getoperationsError;
        int currentAccount = 2;

        private IEnumerable<Operation>? operations = Array.Empty<Operation>();
        private GetAccountUsecase.Account? account;

        async Task Load(int account_id)
        {
            var getAccountUsecase = new GetAccountUsecase();
            account = await getAccountUsecase.Execute(account_id);
            var getOperationsUsecase = new GetOperationsUsecase();
            operations = await getOperationsUsecase.Execute(account_id);
        }
    }
}