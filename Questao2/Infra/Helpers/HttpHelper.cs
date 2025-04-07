using Newtonsoft.Json;
using Questao2.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Questao2.Infra.Helpers
{
    class HttpHelper : IHttpHelper
    {
        private IHttpClientFactory _httpClientFactory { get; set; }
        private HttpClient client { get; set; }
        public HttpHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            client = _httpClientFactory.CreateClient("goalClient");
        }

        public async Task<ApiResponse> ApiCallAsync(string team, int year, int teamNumber, int page)
        {
            try
            {
                string url = $"{client.BaseAddress}?year={year}&team{teamNumber}={team}&page={page}";


                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        ApiResponse resp = JsonConvert.DeserializeObject<ApiResponse>(responseBody)!;
                        return resp;
                    }
                }

                return await Task.FromResult(new ApiResponse());

            } catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
