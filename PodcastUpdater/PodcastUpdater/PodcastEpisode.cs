using System.Text.RegularExpressions;

namespace PodcastUpdater;

public class PodcastEpisode(int number, string name)
{
    private const string youtubeBaseUrl = "https://www.youtube.com/watch?v=";

    private string? _youtubeVideoID;

    private Uri? _patreonPostUrl;

    private Uri? _tipeeePostUrl;

    private Uri? _githubFileUrl;

    private FileInfo? _localMp3File;

    private FileInfo? _localOggFile;

    public int Number { get; set; } = number;

    public string Name { get; set; } = name;

    public string Description { get; set; } = "";

    public DateTime PublicationDate{ get; set; }

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

    public string GetPatreonPostUrl()
    {
        return _patreonPostUrl?.ToString()
            ?? throw new InvalidOperationException("Patreon post URL has not been set.");
    }

    public void SetPatreonPostUrl(string patreonPostUrl)
    {
        throw new NotImplementedException();
    }

    public string GetTipeeePostUrl()
    {
        return _tipeeePostUrl?.ToString()
            ?? throw new InvalidOperationException("Tipeee post URL has not been set.");
    }

    public void SetTipeeePostUrl(string tipeeePostUrl)
    {
        throw new NotImplementedException();
    }

    public string GetGitHubFileUrl()
    {
        return _githubFileUrl?.ToString()
            ?? throw new InvalidOperationException("GitHub file URL has not been set.");
    }

    public void SetGitHubFileUrl(string gitHubFileUrl)
    {
        if (string.IsNullOrWhiteSpace(gitHubFileUrl))
            throw new ArgumentNullException(nameof(gitHubFileUrl), "GitHub file URL cannot be null or empty.");

        _githubFileUrl = new Uri(gitHubFileUrl);
    }

    public string GetLocalMp3FilePath()
    {
        return _localMp3File?.FullName
            ?? throw new InvalidOperationException("Local MP3 file path has not been set.");
    }

    public void SetLocalMp3File(string mp3FilePath)
    {
        if (string.IsNullOrWhiteSpace(mp3FilePath))
            throw new ArgumentNullException(nameof(mp3FilePath), "MP3 file path cannot be null or empty.");

        var fileInfo = new FileInfo(mp3FilePath);

        if (!fileInfo.Exists)
            throw new FileNotFoundException("The specified MP3 file does not exist.", mp3FilePath);

        if (!fileInfo.Name.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("The file must be an MP3 file.", nameof(mp3FilePath));

        _localMp3File = fileInfo;
    }

    public string GetLocalOggFilePath()
    {
        return _localOggFile?.FullName
            ?? throw new InvalidOperationException("Local OGG file path has not been set.");
    }

    public void SetLocalOggFile(string oggFilePath)
    {
        if (string.IsNullOrWhiteSpace(oggFilePath))
            throw new ArgumentNullException(nameof(oggFilePath), "OGG file path cannot be null or empty.");

        var fileInfo = new FileInfo(oggFilePath);

        if (!fileInfo.Exists)
            throw new FileNotFoundException("The specified OGG file does not exist.", oggFilePath);

        if (!fileInfo.Name.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("The file must be an OGG file.", nameof(oggFilePath));

        _localOggFile = fileInfo;
    }
}
