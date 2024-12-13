using System.Text.RegularExpressions;

namespace PodcastUpdater;

public class PodcastEpisode(int number, string name)
{
    private const string youtubeBaseUrl = "https://www.youtube.com/watch?v=";

    private string? _youtubeVideoID;

    public int Number { get; set; } = number;

    public string Name { get; set; } = name;

    public string Description { get; set; } = "";

    public DateTime PublicationDate { get; set; }

    public Uri? GithubFileUrl { get; set; }

    public FileInfo? LocalMp3File { get; set; }

    public FileInfo? LocalOggFile { get; set; }

    public string GetYoutubeVideoUrl()
    {
        if (_youtubeVideoID is null)
            throw new InvalidOperationException("YouTube video ID not defined.");

        return $"{youtubeBaseUrl}{_youtubeVideoID}";
    }

    public void SetYoutubeVideoID(string youtubeVideoID)
    {
        if (string.IsNullOrWhiteSpace(youtubeVideoID) || !Regex.IsMatch(youtubeVideoID, @"^[A-Za-z0-9_-]{11}$"))
            throw new ArgumentNullException(nameof(youtubeVideoID), $"The specified YouTube video ID is not valid: {youtubeVideoID}");

        _youtubeVideoID = youtubeVideoID;
    }
}
