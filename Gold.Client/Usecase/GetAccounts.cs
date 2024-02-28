using Gold.Service;
using System.Text.Json;
using System.Xml.Linq;
using static Gold.Client.Usecase.GetOperationsUsecase;
using static Gold.Usecase.GetAccountsUsecase;

namespace Gold.Usecase
{
    public class GetAccountsUsecase
    {
        public class RespAccounts
        {
            public Accounts? data { get; set; }
        }

        public class Accounts
        {
            public IList<Account>? accounts { get; set; }
        }

        public class Account
        {
            public int? id { get; set; }
            public int? parent_account_id { get; set; } 
            public int? type_id { get; set; }
            public string? label { get; set; }
            public string? description { get; set; }
            public float? balance_reconcilied { get; set; }
            public string? balance_reconcilied_color { get; set; }
            public float? balance_not_reconcilied { get; set; }
            public string? balance_not_reconcilied_color { get; set; }
        }

        public async Task<IList<Account>> Execute()
        {
            GraphQlService graphQlService = new GraphQlService();
            string qry = @"query {
                  accounts {
                    id
                    type_id
                    parent_account_id
                    label
                    description
                    balance_reconcilied
                    balance_not_reconcilied
                  }
                }";
            string response = await graphQlService.Execute(qry);
            var respJson = JsonSerializer.Deserialize<RespAccounts>(response);


            foreach (var account in respJson?.data?.accounts)
            {
                if (account.balance_reconcilied > 0)
                {
                    account.balance_reconcilied_color = "#27ae60";
                } else
                {
                    account.balance_reconcilied_color = "#e74c3c";
                }

                if (account.balance_not_reconcilied > 0)
                {
                    account.balance_not_reconcilied_color = "#27ae60";
                }
                else
                {
                    account.balance_not_reconcilied_color = "#e74c3c";
                }
            }

            return respJson?.data?.accounts;
        }
    }
}
