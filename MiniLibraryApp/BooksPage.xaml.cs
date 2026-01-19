using Microsoft.Maui.Storage;
using MiniLibraryApp.Models;
using MiniLibraryApp.Services;

namespace MiniLibraryApp;

public partial class BooksPage : ContentPage
{
    private readonly FirebaseService _firebase;

    public BooksPage()
    {
        InitializeComponent();

        string userId = Preferences.Default.Get("UserId", "");
        _firebase = new FirebaseService(userId);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadBooks();
    }

    private async Task LoadBooks()
    {
        var data = await _firebase.GetBooksAsync();

        var books = data.Select(item =>
        {
            item.Value.Id = item.Key;
            return item.Value;
        }).ToList();

        booksView.ItemsSource = books;
    }


    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddBookPage());
    }

    private async void OnDeleteBookClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var bookId = button.CommandParameter.ToString();

        bool confirm = await DisplayAlert(
            "Confirmation",
            "Supprimer ce livre ?",
            "Oui",
            "Non");

        if (!confirm) return;

        await _firebase.DeleteBookAsync(bookId);
        await LoadBooks();
    }

    private async void OnRefreshClicked(object sender, EventArgs e) 
            {
        await LoadBooks();
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
         Preferences.Default.Remove("UserId");
        Application.Current.Windows[0].Page = new NavigationPage(new LoginPage());
    }
}
