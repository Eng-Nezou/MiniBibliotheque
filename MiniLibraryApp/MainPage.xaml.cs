using Microsoft.Maui.Storage;
using MiniLibraryApp.Models;
using MiniLibraryApp.Services;

namespace MiniLibraryApp;

public partial class MainPage : ContentPage
{
    private FirebaseService _firebaseService;
    private Dictionary<string, Book> _books = new();
    private string _userId;

    public MainPage(string userId)
    {
        InitializeComponent();
        _userId = userId;
        _firebaseService = new FirebaseService(_userId);
        LoadBooks();
    }

    private async void LoadBooks()
    {
        _books = await _firebaseService.GetBooksAsync();
        booksView.ItemsSource = _books.Select(b => new Book
        {
            Id = b.Key,
            Title = b.Value.Title,
            Author = b.Value.Author,
            Status = b.Value.Status,
            ImageUrl = b.Value.ImageUrl
        }).ToList();
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(titleEntry.Text) && !string.IsNullOrWhiteSpace(authorEntry.Text))
        {
            var book = new Book
            {
                Title = titleEntry.Text,
                Author = authorEntry.Text,
                ImageUrl = imageEntry.Text?.Trim() ?? string.Empty
            };
            await _firebaseService.AddBookAsync(book);
            titleEntry.Text = "";
            authorEntry.Text = "";
            imageEntry.Text = "";
            LoadBooks();
        }
    }

    private async void OnDeleteBookClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Book book)
        {
            await _firebaseService.DeleteBookAsync(book.Id);
            LoadBooks();
        }
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        Preferences.Default.Remove("UserId");
        Application.Current.Windows[0].Page = new NavigationPage(new LoginPage());
    }
}
