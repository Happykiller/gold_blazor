using Gold.Usecase;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Gold.Components.Pages
{
    public partial class Home
    {
        private IEnumerable<Usecase.GetAccountsUsecase.Account>? accounts = Array.Empty<Usecase.GetAccountsUsecase.Account>();
        private bool getAccountsError;
        private bool shouldRender;
        List<int> list = new List<int>() { 4,5,6,7,8,9,10,11,12,14,15,17,18,19,20,21,22,23,33,34,35,38 };
        private float balance_reconcilied_total = 0;
        private float balance_not_reconcilied_total = 0;

        protected override bool ShouldRender() => shouldRender;

        protected override async Task OnInitializedAsync()
        {
            var getAccountsUsecase = new GetAccountsUsecase();
            accounts = await getAccountsUsecase.Execute();
            shouldRender = true;
        }

    }
}