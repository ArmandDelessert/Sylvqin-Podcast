namespace PodcastUpdater;

public static class PodcastSettings
{
    public static string GitFolderPath => @"C:\Dev\Armand\ArmandDelessert.github.io\RssFeed\Sylvqin-Podcast";

    public static string JsonFileRelativePath => "Podcast.json";

    public static string JsonFilePath => Path.Combine(GitFolderPath, JsonFileRelativePath);

    public static string ReadmeFileRelativePath => "ReadMe.md";

    public static string ReadmeFilePath => Path.Combine(GitFolderPath, ReadmeFileRelativePath);

    public static string RssFeedFileRelativePath => "Podcast.xml";

    public static string RssFeedFilePath => Path.Combine(GitFolderPath, RssFeedFileRelativePath);

    public static string AudioMp3FilesFolderRelativePath => "Files";

    public static string AudioMp3FilesFolderPath  => Path.Combine(GitFolderPath, AudioMp3FilesFolderRelativePath);

    //private static string OnedriveBasePath => Environment.GetEnvironmentVariable("OneDriveConsumer") ?? throw new InvalidOperationException("The personal OneDrive folder is not defined.");

    public static string YoutubePlaylistId => "PLU0vpugYYFUrtBnjxPAZFRKKG89RqgKUm";
}
