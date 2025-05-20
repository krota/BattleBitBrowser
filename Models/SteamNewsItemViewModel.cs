namespace BattleBitBrowser.Models;

public class SteamNewsItemViewModel
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? Author { get; set; }
    public string? SanitizedContents { get; set; }
    public DateTime Date { get; set; }
}