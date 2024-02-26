using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Gold.Components.Pages
{

    public partial class Gold
    {
        private IEnumerable<Operation>? operations = Array.Empty<Operation>();
        private Account? account = new Account();
        private bool getoperationsError;
        private bool getAccountError;
        private bool shouldRender;
        // PROD
        // private string host = "https://api.gold.happykiller.net/graphql";
        // DEV
        private string host = "http://localhost:3000/graphql"; 
        private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjb2RlIjoiZmFybyIsImlkIjoxLCJpYXQiOjE3MDg5NDI1NzUsImV4cCI6MTcwODk3MTM3NX0.CbcMTKkesqDtgWiny2BRrHM-SJKZTank30giyKGBZiA";

        protected override bool ShouldRender() => shouldRender;

        protected override async Task OnInitializedAsync()
        {
            var client = ClientFactory.CreateClient();

            //////////////////////////// Operations
            var request = new HttpRequestMessage(HttpMethod.Post, host);
            // add authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var postBody = new { query = @"query {
  operations (
    dto: {
      account_id: 2
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
}", variables = new { } };
            request.Content = new StringContent(JsonSerializer.Serialize(postBody), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var resp = JsonSerializer.Deserialize<RespOperations>(json);
                operations = resp?.data?.operations;
            }
            else
            {
                getoperationsError = true;
            }

            ////////////////////////////////// Account details
            var requestAccount = new HttpRequestMessage(HttpMethod.Post, host);
            // add authorization header
            requestAccount.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var postBodyAccount = new { query = @"query {
  account (
    dto: {
      account_id: 2
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
}", variables = new { } };
            requestAccount.Content = new StringContent(JsonSerializer.Serialize(postBodyAccount), Encoding.UTF8, "application/json");

            var responseAccount = await client.SendAsync(requestAccount);

            if (responseAccount.IsSuccessStatusCode)
            {
                var jsonAccount = await responseAccount.Content.ReadAsStringAsync();
                var respAccount = JsonSerializer.Deserialize<RespAccount>(jsonAccount);
                account = respAccount?.data?.account;
            }
            else
            {
                Console.WriteLine(responseAccount);
                getAccountError = true;
            }

            shouldRender = true;
        }

        public class RespOperations
        {
            public Operations? data { get; set; }
        }

        public class Operations
        {
            public IList<Operation>? operations { get; set; }
        }

        public class RespAccount
        {
            public TheAccount? data { get; set; }
        }

        public class TheAccount
        {
            public Account? account { get; set; }
        }

        public class Detail
        {
            public int? id { get; set; }
            public string? label { get; set; }
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

    }
}