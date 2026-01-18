using MiniLibraryApp.Models;
using MiniLibraryApp.Services;

namespace MiniLibraryApp.Pages;

public partial class BooksPage : ContentPage
{
    private readonly FirebaseService _firebase = new();

    public BooksPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        booksView.ItemsSource = (await _firebase.GetBooksAsync()).Values.ToList();
    }
}
