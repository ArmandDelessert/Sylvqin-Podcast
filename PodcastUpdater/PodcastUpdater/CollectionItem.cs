namespace PodcastUpdater;

public class CollectionItem(int number, string name) : INamedItem, INumberedItem
{
    public int Number { get; set; } = number;

    public string Name { get; set; } = name;
}