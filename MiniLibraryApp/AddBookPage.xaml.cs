namespace MiniLibraryApp;

public partial class AddBookPage : ContentPage
{
	public AddBookPage()
	{
		InitializeComponent();
	}
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void OnAddBookClicked(object sender,EventArgs e)
    {
        string title = titleEntry.Text;
        string author = authorEntry.Text;
        string imageUrl = imageEntry.Text;
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
        {
            await DisplayAlert("Erreur", "Le titre et l'auteur sont obligatoires.", "OK");
            return;
        }
        string userId = Microsoft.Maui.Storage.Preferences.Default.Get("UserId", "");
        var firebaseService = new Services.FirebaseService(userId);
        var newBook = new Models.Book
        {
            Title = title,
            Author = author,
            ImageUrl = imageUrl ?? string.Empty
        };
        await firebaseService.AddBookAsync(newBook);
        await DisplayAlert("Succès", "Livre ajouté avec succès.", "OK");
        await Navigation.PopAsync();
    }

    private async void OnGoToBooksClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BooksPage());
    }


}