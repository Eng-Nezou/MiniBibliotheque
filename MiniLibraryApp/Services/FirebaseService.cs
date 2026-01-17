using System.Net.Http.Json;
using MiniLibraryApp.Models;

namespace MiniLibraryApp.Services;

public class FirebaseService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl = "https://minilibraryapp-default-rtdb.firebaseio.com/";

    public FirebaseService()
    {
        _client = new HttpClient();
    }

    public async Task AddBookAsync(Book book)
    {
        await _client.PostAsJsonAsync($"{_baseUrl}.json", book);
    }

    public async Task<Dictionary<string, Book>> GetBooksAsync()
    {
        var response = await _client.GetFromJsonAsync<Dictionary<string, Book>>($"{_baseUrl}.json");
        return response ?? new Dictionary<string, Book>();
    }

    public async Task DeleteBookAsync(string id)
    {
        await _client.DeleteAsync($"{_baseUrl}/{id}.json");
    }
}
