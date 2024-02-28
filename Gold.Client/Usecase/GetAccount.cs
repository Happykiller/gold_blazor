using Gold.Service;
using System;
using System.Text.Json;
using System.Xml.Linq;

namespace Gold.Usecase
{
    public class GetAccountUsecase
    {
        private readonly GraphQlService _graphQlService;

        public GetAccountUsecase(GraphQlService graphQlService)
        {
            _graphQlService = graphQlService;
        }

        public class Account
        {
            public int? id { get; set; }
            public int? type_id { get; set; }
            public string? label { get; set; }
            public string? description { get; set; }
            public float? balance_reconcilied { get; set; }
            public float? balance_not_reconcilied { get; set; }
        }

        public class RespAccount
        {
            public TheAccount? data { get; set; }
        }

        public class TheAccount
        {
            public Account? account { get; set; }
        }


        public async Task<Account> Execute(int account_id)
        {
            string qry = @"query {
                  account (
                    dto: {
                      account_id: " + account_id + @"
                    }
                  ) {
                    id
                    type_id
                    parent_account_id
                    label
                    description
                    balance_reconcilied
                    balance_not_reconcilied
                  }
                }";
            string response = await _graphQlService.Execute(qry);
            var respAccount = JsonSerializer.Deserialize<RespAccount>(response);
            return respAccount?.data?.account;
        }
    }
}
