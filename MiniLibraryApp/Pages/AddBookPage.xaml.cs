using Java.Util;
using MiniLibraryApp.Models;
using MiniLibraryApp.Services;

namespace MiniLibraryApp.Pages;

public partial class AddBookPage : ContentPage
{
    private readonly FirebaseService _firebase = new();

    public AddBookPage()
    {
        InitializeComponent();
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        var book = new Book
        {
            Title = titleEntry.Text,
            Author = authorEntry.Text,
            ImageUrl = imageEntry.Text
        };

        await _firebase.AddBookAsync(book);
        await DisplayAlert("Succès", "Livre ajouté", "OK");

        titleEntry.Text = "";
        authorEntry.Text = "";
        imageEntry.Text = "";
    }
}
