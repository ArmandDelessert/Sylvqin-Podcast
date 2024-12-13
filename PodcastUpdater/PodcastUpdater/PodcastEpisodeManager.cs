namespace PodcastUpdater;

public class PodcastEpisodeManager
{
    private readonly AudioDownloadService _audioDownloadService;
    private readonly GitHubService _gitHubService;
    private readonly RSSFeedService _rssFeedService;
    private readonly YouTubeService _youTubeService;

    public PodcastEpisodeManager(
        AudioDownloadService audioDownloadService,
        GitHubService gitHubService,
        RSSFeedService rssFeedService,
        YouTubeService youTubeService)
    {
        _audioDownloadService = audioDownloadService ?? throw new ArgumentNullException(nameof(audioDownloadService));
        _gitHubService = gitHubService ?? throw new ArgumentNullException(nameof(gitHubService));
        _rssFeedService = rssFeedService ?? throw new ArgumentNullException(nameof(rssFeedService));
        _youTubeService = youTubeService ?? throw new ArgumentNullException(nameof(youTubeService));
    }

    public void

    public void ProcessNewEpisode(string videoUrl)
    {
        // Méthode principale qui coordonne toutes les étapes du processus
        var episode = ValidateAndPrepareEpisode(videoUrl);

        _youTubeService.AddVideoToPlaylist(episode.PlaylistId, episode.VideoId);
        _gitHubService.UpdateReadme(episode);

        var mp3FilePath = _audioDownloadService.DownloadAudioFromYouTube(videoUrl);
        episode.Mp3FilePath = _audioDownloadService.ConvertToMp3(mp3FilePath);
        episode.Mp3FileSize = _audioDownloadService.GetMp3FileSize(episode.Mp3FilePath);

        _rssFeedService.UpdateRSSFeed(episode);
        _gitHubService.AddFileToRepository(episode.Mp3FilePath);
        _gitHubService.CommitAndPushFile(episode.Mp3FilePath, $"Ajout de l'épisode {episode.Title}");
    }

    private PodcastEpisode ValidateAndPrepareEpisode(string videoUrl)
    {
        // Validation et préparation des informations de l'épisode
    }
}
