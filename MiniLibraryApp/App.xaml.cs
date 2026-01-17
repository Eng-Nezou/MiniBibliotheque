namespace MiniLibraryApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // La page principale de l'application
        MainPage = new NavigationPage(new MainPage());
    }
}
