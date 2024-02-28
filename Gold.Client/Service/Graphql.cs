using Microsoft.VisualBasic;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Gold.Service
{
    public class GraphQlService
    {
        // PROD
        private string host = "https://api.gold.happykiller.net/graphql";
        // DEV
        // private string host = "http://localhost:3000/graphql";
        private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjb2RlIjoiZmFybyIsImlkIjoxLCJpYXQiOjE3MDkxMDQ2NDgsImV4cCI6MTcwOTEzMzQ0OH0.5LLh1OhK4QJosvKbAAGfm0ct7g8k1C_XEs6hkXOAUME";
        HttpClient _client;
        string responseString;

        public virtual async Task<string> Execute(string qry)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Uri uri = new Uri(string.Format(host, string.Empty));

            var query = new
            {
                query = qry,
                variables = new { }
            };
            StringContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString;
        }
    }
}
