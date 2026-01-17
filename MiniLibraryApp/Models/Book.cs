namespace MiniLibraryApp.Models;

public class Book
{
    public string Id { get; set; }  // Généré par Firebase
    public string Title { get; set; }
    public string Author { get; set; }
    public string Status { get; set; } = "À lire"; // ou "Lu"
}
