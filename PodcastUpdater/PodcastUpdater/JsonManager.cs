using Newtonsoft.Json;

namespace PodcastUpdater;

internal class JsonManager
{
    public JsonManager(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath) || jsonFilePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            throw new ArgumentException($"'{nameof(jsonFilePath)}' is not a valid file path.", nameof(jsonFilePath));

        JsonFilePath = jsonFilePath;
    }

    public string JsonFilePath { get; }

    public Podcast ReadJsonFile()
    {
        if (!File.Exists(JsonFilePath))
            throw new FileNotFoundException("The JSON file was not found.", JsonFilePath);

        var json = File.ReadAllText(JsonFilePath);
        return ReadJson(json);
    }

    public Podcast ReadJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new ArgumentException("The JSON content cannot be null or whitespace.", nameof(json));

        return JsonConvert.DeserializeObject<Podcast>(json) ?? throw new InvalidOperationException("Failed to deserialize the JSON content.");
    }

    public void WriteJsonFile(Podcast podcast)
    {
        if (podcast is null)
            throw new ArgumentNullException(nameof(podcast), "The podcast cannot be null.");

        var json = GenerateJson(podcast);
        File.WriteAllText(JsonFilePath, json);
    }

    public string GenerateJson(Podcast podcast)
    {
        if (podcast is null)
            throw new ArgumentNullException(nameof(podcast), "The podcast cannot be null.");

        return JsonConvert.SerializeObject(podcast, Formatting.Indented);
    }
}
