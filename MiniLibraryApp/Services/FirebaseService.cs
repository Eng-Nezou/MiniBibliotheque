using System.Net.Http.Json;
using MiniLibraryApp.Models;

namespace MiniLibraryApp.Services;

public class FirebaseService
{
    private readonly HttpClient _client = new HttpClient();
    private readonly string _baseUrl = "https://minilibraryapp-default-rtdb.firebaseio.com/";
    private readonly string _userId;

    public FirebaseService(string userId)
    {
        _userId = userId;

    }

    public async Task AddBookAsync(Book book)
    {
        await _client.PostAsJsonAsync($"{_baseUrl}/{_userId}.json", book);
    }

    public async Task<Dictionary<string, Book>> GetBooksAsync()
    {
        var response = await _client.GetFromJsonAsync<Dictionary<string, Book>>($"{_baseUrl}/{_userId}.json");
        return response ?? new Dictionary<string, Book>();
    }

    public async Task DeleteBookAsync(string id)
    {
        await _client.DeleteAsync($"{_baseUrl}/{_userId}/{id}.json");
    }
}
