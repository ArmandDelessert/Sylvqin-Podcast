using System.Text.RegularExpressions;

namespace PodcastUpdater;

public class PodcastEpisode
{
    private readonly Uri youtubeVideoBaseUrl = new("https://www.youtube.com/watch?v=");
    private readonly Uri patreonBaseUrl = new("https://www.patreon.com/posts/");
    private readonly Uri tipeeeBaseUrl = new("https://fr.tipeee.com/sylvqin/news/");

    private Uri? _githubFileUrl;

    private FileInfo? _localMp3File;

    private FileInfo? _localOggFile;

    public PodcastEpisode(int number, string name, string description, DateTime publicationDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException($"'{nameof(description)}' cannot be null or whitespace.", nameof(description));

        if (publicationDate == DateTime.MinValue)
            throw new ArgumentException($"The value of '{nameof(publicationDate)}' is not valid.", nameof(publicationDate));

        Number = number;
        Name = name;
        Description = description;
        PublicationDate = publicationDate;
    }

    public int Number { get; }

    public string Name { get; }

    public string Description { get; }

    public DateTime PublicationDate{ get; }

    public Uri YoutubeVideoUrl => _youtubeVideoUrl ?? throw new InvalidOperationException("YouTube video ID not set.");

    private Uri? _youtubeVideoUrl;

    public Uri PatreonPostUrl => _patreonPostUrl ?? throw new InvalidOperationException("Patreon post ID not set.");

    private Uri? _patreonPostUrl;

    public Uri TipeeePostUrl => _tipeeePostUrl ?? throw new InvalidOperationException("Tipeee post ID not set.");

    private Uri? _tipeeePostUrl;

    public void SetYoutubeVideoId(string youtubeVideoId)
    {
        if (string.IsNullOrWhiteSpace(youtubeVideoId) || !Regex.IsMatch(youtubeVideoId, @"^[A-Za-z0-9_-]{11}$"))
            throw new ArgumentNullException(nameof(youtubeVideoId), $"The specified YouTube video ID is not valid: {youtubeVideoId}");

        _youtubeVideoUrl = new(youtubeVideoBaseUrl, youtubeVideoId);
    }

    public void SetPatreonPostId(string patreonPostId)
    {
        _patreonPostUrl = new(patreonBaseUrl, patreonPostId);
    }

    public void SetTipeeePostId(string tipeeePostId)
    {
        _tipeeePostUrl = new(tipeeeBaseUrl, tipeeePostId);
    }

    public string GetGithubFileUrl()
    {
        return _githubFileUrl?.ToString()
            ?? throw new InvalidOperationException("GitHub file URL has not been set.");
    }

    public void SetGithubFileUrl(string githubFileUrl)
    {
        if (string.IsNullOrWhiteSpace(githubFileUrl))
            throw new ArgumentNullException(nameof(githubFileUrl), "GitHub file URL cannot be null or empty.");

        _githubFileUrl = new Uri(githubFileUrl);
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
