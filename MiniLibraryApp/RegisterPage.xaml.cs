using Microsoft.Maui.Storage;
using MiniLibraryApp.Services;

namespace MiniLibraryApp;

public partial class RegisterPage : ContentPage
{
    private AuthService _authService = new AuthService();

    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (passwordEntry.Text != confirmEntry.Text)
        {
            await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas", "OK");
            return;
        }

        var userId = await _authService.RegisterUser(emailEntry.Text, passwordEntry.Text);
        if (!string.IsNullOrEmpty(userId))
        {
            Preferences.Default.Set("UserId", userId);
            Application.Current.Windows[0].Page = new NavigationPage(new MainPage(userId));
        }
        else
        {
            await DisplayAlert("Erreur", "Impossible de cr�er le compte", "OK");
        }
    }

    private async void OnGoToLoginTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}