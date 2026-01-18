
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniLibraryApp.Services;

public class AuthService
{
    private readonly string apiKey = "AIzaSyAw4dJtXF4vNJaoFe_a9fVzAvoAOIKC2oU";
    private readonly HttpClient _client = new HttpClient();

    public async Task<string> RegisterUser(string email, string password)
    {
        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";
        var response = await _client.PostAsJsonAsync(url, new { email, password, returnSecureToken = true });
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("localId").GetString() ?? string.Empty;
        }
        return string.Empty;
    }

    public async Task<string> LoginUser(string email, string password)
    {
        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";
        var response = await _client.PostAsJsonAsync(url, new { email, password, returnSecureToken = true });
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("localId").GetString() ?? string.Empty;
        }
        return string.Empty;
    }
}

