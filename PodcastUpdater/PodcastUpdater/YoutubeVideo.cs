namespace PodcastUpdater;

internal class YoutubeVideo(string name, string VideoId)
{
    public string Name { get; } = name;

    public string VideoId { get; } = VideoId;
}
