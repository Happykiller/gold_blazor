using Gold.Client.Usecase;
using Gold.Service;
using Gold.Usecase;
using static Gold.Client.Usecase.GetOperationsUsecase;

namespace Gold.Client.Pages
{
    public partial class Counter
    {
        private bool loading = false;
        private bool getAccountError;
        private bool getoperationsError;
        int currentAccount = 2;

        private GraphQlService graphQlService;
        private GetAccountUsecase getAccountUsecase;
        private GetOperationsUsecase getOperationsUsecase;
        private SetRecoUsecase setRecoUsecase;

        private IEnumerable<Operation>? operations;
        private GetAccountUsecase.Account? account;

        public Counter() {
            graphQlService = new GraphQlService();
            getAccountUsecase = new GetAccountUsecase(graphQlService);
            getOperationsUsecase = new GetOperationsUsecase();
            setRecoUsecase = new SetRecoUsecase(graphQlService);
        }

        async Task Load(int account_id)
        {
            loading = true;
            account = await getAccountUsecase.Execute(account_id);
            operations = await getOperationsUsecase.Execute(account_id);
            loading = false;
        }

        async Task Reco(int operation_id)
        {
            loading = true;
            await setRecoUsecase.Execute(operation_id);
            account = await getAccountUsecase.Execute(currentAccount);
            operations = await getOperationsUsecase.Execute(currentAccount);
            loading = false;
        }
    }
}