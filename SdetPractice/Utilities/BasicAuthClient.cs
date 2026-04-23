using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace SdetPractice.Utilities
{
    /// <summary>HTTP client helper for testing Basic Auth endpoints at the protocol level.</summary>
    public static class BasicAuthClient
    {
        /// <summary>Sends a GET request to the given URL and returns the HTTP status code. Pass credentials to include a Basic Auth header.</summary>
        public static async Task<HttpStatusCode> GetStatusCodeAsync(string url, string? username = null, string? password = null)
        {
            using var client = new HttpClient();

            if (username != null && password != null)
            {
                var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
            }

            var response = await client.GetAsync(url);
            return response.StatusCode;
        }

    }
}
