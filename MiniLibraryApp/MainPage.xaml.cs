using MiniLibraryApp.Models;
using MiniLibraryApp.Services;

namespace MiniLibraryApp;

public partial class MainPage : ContentPage
{
    private FirebaseService _firebaseService;
    private Dictionary<string, Book> _books = new();

    public MainPage()
    {
        InitializeComponent(); // unique et correct
        _firebaseService = new FirebaseService();
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
            Status = b.Value.Status
        }).ToList();
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(titleEntry.Text) && !string.IsNullOrWhiteSpace(authorEntry.Text))
        {
            var book = new Book
            {
                Title = titleEntry.Text,
                Author = authorEntry.Text
            };
            await _firebaseService.AddBookAsync(book);
            titleEntry.Text = "";
            authorEntry.Text = "";
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
}
