using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace firehopper
{
    public

    class firebaseManager
    {
        /// <summary>
        /// This is a method that does the GET request asynchronously to Firebase.
        /// </summary>
        public static async Task<string> getAsync(string _apiKey, string _databaseURL, string _databaseNode)
        {
            HttpClient httpClient = setHttpClient(_apiKey, _databaseURL);
            var res = await httpClient.GetAsync(_databaseURL + _databaseNode + ".json");
            string responseBodyAsText = await res.Content.ReadAsStringAsync();

            return responseBodyAsText;
        }


        /// <summary>
        /// This is a method that does the PUT request asynchronously to Firebase.
        /// </summary>
        public static async Task<string> putAsync(string _apiKey, string _databaseURL, string _databaseNode, string _keyValuePair)
        {
            HttpClient httpClient = setHttpClient(_apiKey, _databaseURL);
            HttpContent httpContent = new StringContent(_keyValuePair);
            var res = await httpClient.PutAsync(_databaseURL + _databaseNode + ".json", httpContent);

            return res.StatusCode.ToString();
        }


        /// <summary>
        /// This is a method that does the POST request asynchronously to Firebase.
        /// </summary>
        public static async Task<string> postAsync(string _apiKey, string _databaseURL, string _databaseNode, string _keyValuePair)
        {
            HttpClient httpClient = setHttpClient(_apiKey, _databaseURL);
            HttpContent httpContent = new StringContent(_keyValuePair);
            var res = await httpClient.PostAsync(_databaseURL + _databaseNode + ".json", httpContent);

            return res.StatusCode.ToString();
        }


        /// <summary>
        /// This is a method that does the PATCH request asynchronously to Firebase.
        /// </summary>
        public static async Task<string> patchAsync(string _apiKey, string _databaseURL, string _databaseNode, string _keyValuePair)
        {
            HttpClient httpClient = setHttpClient(_apiKey, _databaseURL);
            HttpContent httpContent = new StringContent(_keyValuePair);
            HttpRequestMessage req = new HttpRequestMessage(new HttpMethod("PATCH"), _databaseURL + _databaseNode + ".json");
            req.Content = httpContent;
            var res = await httpClient.SendAsync(req);
            //var responseBodyAsText = await res.Content.ReadAsStringAsync();

            return res.StatusCode.ToString();
        }


        /// <summary>
        /// This is a method that does the DELETE request asynchronously to Firebase.
        /// </summary>
        public static async Task<string> deleteAsync(string _apiKey, string _databaseURL, string _databaseNode)
        {
            HttpClient httpClient = setHttpClient(_apiKey, _databaseURL);
            var res = await httpClient.DeleteAsync(_databaseURL + _databaseNode + ".json");

            return res.StatusCode.ToString();
        }

        public static HttpClient setHttpClient(string _apiKey, string _databaseURL)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_databaseURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", "Bearer " + _apiKey);

            return httpClient;
        }
    }
}
