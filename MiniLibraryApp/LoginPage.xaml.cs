using Microsoft.Maui.Storage;
using MiniLibraryApp.Services;

namespace MiniLibraryApp;

public partial class LoginPage : ContentPage
{
    private AuthService _authService = new AuthService();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var userId = await _authService.LoginUser(emailEntry.Text, passwordEntry.Text);
        if (!string.IsNullOrEmpty(userId))
        {
            Preferences.Default.Set("UserId", userId);
            Application.Current.Windows[0].Page = new NavigationPage(new BooksPage());
        }
        else
        {
            await DisplayAlert("Erreur", "Email ou mot de passe incorrect", "OK");
        }
    }

    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}