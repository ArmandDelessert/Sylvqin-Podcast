using System.Text;

namespace PodcastUpdater;

public class ReadmeManager
{
    private const string EpisodesSection = "## Épisodes";

    private readonly FileInfo _readmeFile;

    public ReadmeManager(string readmeFilePath)
    {
        ArgumentNullException.ThrowIfNull(readmeFilePath);

        _readmeFile = new(readmeFilePath);
    }

    public void UpdateReadmeFile(PodcastEpisode newEpisode)
    {
        if (!_readmeFile.Exists)
            throw new FileNotFoundException("ReadMe file not found.", _readmeFile.FullName);

        var readmeContent = File.ReadAllText(_readmeFile.FullName);

        // Trouver la section des épisodes
        int episodesStart = readmeContent.IndexOf(EpisodesSection);
        if (episodesStart == -1)
            throw new InvalidOperationException("Episodes section not found in ReadMe file.");

        // Trouver le début de la prochaine section
        int nextSectionStart = readmeContent.IndexOf("## ", episodesStart + EpisodesSection.Length);

        // Générer le nouveau contenu de l'épisode
        string newEpisodeContent = GenerateEpisodeMarkdown(newEpisode);

        // Insérer le nouvel épisode
        string updatedContent = readmeContent.Insert(
            nextSectionStart == -1 ? readmeContent.Length : nextSectionStart,
            "\n" + newEpisodeContent + "\n"
        );

        // Écrire le contenu mis à jour
        File.WriteAllText(_readmeFile.FullName, updatedContent);
    }

    private static string GenerateEpisodeMarkdown(PodcastEpisode episode)
    {
        if (episode is null)
            throw new ArgumentNullException(nameof(episode), "Episode cannot be null.");

        var markdownBuilder = new StringBuilder();
        markdownBuilder.AppendLine($"### Épisode {episode.Number} - {episode.Name}");
        markdownBuilder.AppendLine();

        // Format de date similaire à l'existant
        markdownBuilder.AppendLine($"Publication : {episode.PublicationDate:dddd d MMMM yyyy HH:mm:ss} UTC+2");
        markdownBuilder.AppendLine();

        // Liens standards basés sur le format existant
        markdownBuilder.AppendLine($"- [Patreon]({episode.GetPatreonPostUrl()})");
        markdownBuilder.AppendLine($"- [Tipeee]({episode.GetTipeeePostUrl()})");
        markdownBuilder.AppendLine($"- [YouTube]({episode.GetYoutubeVideoUrl()})");
        //markdownBuilder.AppendLine($"- [Google Drive](https://drive.google.com/file/d/123456789abcdefghijklmnopqrstuvwx/view) (format MP3)");
        markdownBuilder.AppendLine($"- [GitHub](Files/Épisode_{episode.Number}.mp3) (format MP3)");

        return markdownBuilder.ToString();
    }
}
