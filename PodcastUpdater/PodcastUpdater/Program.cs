namespace PodcastUpdater;

internal static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("PodcastUpdater");

        JsonManager jsonManager = new(PodcastSettings.JsonFilePath);
        ReadmeManager readmeManager = new(PodcastSettings.ReadmeFilePath);
        AudioDownloadService audioDownloadService = new();
        GitService gitService = new();
        RssFeedService rssFeedService = new();
        YoutubeService youtubeService = new(PodcastSettings.YoutubePlaylistId);
        PodcastManager manager = new(jsonManager, readmeManager, audioDownloadService, gitService, rssFeedService, youtubeService);
    }
}
