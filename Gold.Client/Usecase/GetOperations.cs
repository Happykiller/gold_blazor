using Gold.Service;
using System.Text.Json;

namespace Gold.Client.Usecase
{
    public class GetOperationsUsecase
    {
        public class RespOperations
        {
            public Operations? data { get; set; }
        }

        public class Operations
        {
            public IList<Operation>? operations { get; set; }
        }

        public class Operation
        {
            public int? id { get; set; }
            public int? account_id { get; set; }
            public Detail? account { get; set; }
            public int? account_id_dest { get; set; }
            public Detail? account_dest { get; set; }
            public string? description { get; set; }
            public float? amount { get; set; }
            public string? date { get; set; }
            public int? status_id { get; set; }
            public int? type_id { get; set; }
            public int? third_id { get; set; }
            public Detail? third { get; set; }
            public int? category_id { get; set; }
            public Detail? category { get; set; }
        }

        public class Detail
        {
            public int? id { get; set; }
            public string? label { get; set; }
        }

        public async Task<IList<Operation>> Execute(int account_id)
        {
            GraphQlService graphQlService = new GraphQlService();
            string qry = @"query {
                operations (
                dto: {
                    account_id: " + account_id + @"
                }
                ) {
                id
                account_id
                account {
                    id
                    label
                }
                account_id_dest
                account_dest {
                    id
                    label
                }
                amount
                date
                status_id
                type_id
                third_id
                third {
                    id
                    label
                }
                category_id
                category {
                    id
                    label
                }
                description
                creator_id
                creation_date
                modificator_id
                modification_date
                }
            }";
            string response = await graphQlService.Execute(qry);
            var resp = JsonSerializer.Deserialize<RespOperations>(response);
            return resp?.data?.operations;
        }
    }
}
