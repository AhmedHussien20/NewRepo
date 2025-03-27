using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ApiClient : IDisposable
{
    private readonly HttpClient _client;

    public ApiClient(string baseUrl)
    {
        _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetAsync(string endpoint, string authToken = null)
    {
        AddBearerTokenHeader(authToken);

        HttpResponseMessage response = await _client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode(); // Throw an exception if the request is not successful

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostAsync(string endpoint, string data, string authToken = null)
    {
        AddAuthTokenHeader(authToken);

        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode(); // Throw an exception if the request is not successful

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostAsyncBearer(string endpoint, string data, string authToken = null)
    {
        AddBearerTokenHeader(authToken);

        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode(); // Throw an exception if the request is not successful

        return await response.Content.ReadAsStringAsync();
    }

    private void AddAuthTokenHeader(string authToken)
    {
        if (!string.IsNullOrEmpty(authToken))
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
        }
    }

    private void AddBearerTokenHeader(string authToken)
    {
        if (!string.IsNullOrEmpty(authToken))
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
