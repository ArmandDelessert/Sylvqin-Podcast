namespace PodcastUpdater;

internal class Podcast
{
    public Podcast(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException($"'{nameof(description)}' cannot be null or whitespace.", nameof(description));

        Title = title;
        Description = description;
    }

    public string Title { get; }

    public string Description { get; }

    // Cover

    public int EpisodeCount => Episodes.Count;

    public IList<PodcastEpisode> Episodes { get; }

    public void AddEpisode(PodcastEpisode episode) => Episodes.Add(episode);
}
