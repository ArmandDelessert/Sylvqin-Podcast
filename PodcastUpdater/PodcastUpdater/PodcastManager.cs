namespace PodcastUpdater;

internal class PodcastManager(
    JsonManager jsonManager,
    ReadmeManager readmeManager,
    AudioDownloadService audioDownloadService,
    GitService gitService,
    RssFeedService rssFeedService,
    YoutubeService youtubeService)
{
    private readonly JsonManager _jsonManager = jsonManager ?? throw new ArgumentNullException(nameof(jsonManager));
    private readonly ReadmeManager _readmeManager = readmeManager ?? throw new ArgumentNullException(nameof(readmeManager));
    private readonly AudioDownloadService _audioDownloadService = audioDownloadService ?? throw new ArgumentNullException(nameof(audioDownloadService));
    private readonly GitService _gitService = gitService ?? throw new ArgumentNullException(nameof(gitService));
    private readonly RssFeedService _rssFeedService = rssFeedService ?? throw new ArgumentNullException(nameof(rssFeedService));
    private readonly YoutubeService _youtubeService = youtubeService ?? throw new ArgumentNullException(nameof(youtubeService));

    private Podcast? _podcast;

    public void AddNewPodcastEpisode(string youtubeVideoId, string patreonPostId, string tipeeePostId, string? googleDriveFileId)
    {
        GetPodcastFromJson();

        if (_podcast is null)
            throw new InvalidOperationException(); // TODO : Faire mieux ?

        // TODO : Récupérer la vidéo depuis YouTube.

        PodcastEpisode newEpisode = new(_podcast.EpisodeCount, );
        newEpisode.SetYoutubeVideoId(youtubeVideoId);
        newEpisode.SetPatreonPostId(patreonPostId);
        newEpisode.SetTipeeePostId(tipeeePostId);
        _podcast.AddEpisode(newEpisode);

        UpdateJson();

        UpdatePodcast();
    }

    public void UpdatePodcast()
    {

    }

    private void GetPodcastFromJson()
    {
        _podcast = _jsonManager.ReadJsonFile();
    }

    private List<YoutubeVideo> GetPodcastFromYoutube()
    {
        return _youtubeService.GetEpisodesFromPlaylist();
    }

    private void UpdateJson()
    {
        if (_podcast is null)
            throw new InvalidOperationException();

        _jsonManager.WriteJsonFile(_podcast);
    }

    private void UpdateReadme()
    {
        _readmeManager.WriteReadme();
    }

    private void UpdateRssFeed()
    {
        _rssFeedService.UpdateRssFeed();
    }

    private void DownloadAudioFile()
    {
        _audioDownloadService.DownloadAudioFromYoutube();
    }

    private void PublishThroughGit()
    {
        _gitService.PublishChanges();
    }
}
