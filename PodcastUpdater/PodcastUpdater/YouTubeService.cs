using System.Diagnostics;
using Newtonsoft.Json;

namespace PodcastUpdater;

internal class YoutubeService
{
    private const string Command = "yt-dlp";
    private const string OptionFlatPlaylist = "--flat-playlist";
    private const string OptionDumpJson = "--dump-json";

    private readonly Uri youtubePlaylistBaseUrl = new("https://www.youtube.com/playlist?list=");

    public YoutubeService(string youtubePlaylistId)
    {
        if (string.IsNullOrWhiteSpace(youtubePlaylistId) && youtubePlaylistId.Length != 34)
            throw new ArgumentException($"'{nameof(youtubePlaylistId)}' cannot be null or whitespace.", nameof(youtubePlaylistId));

        _youtubePlaylistUrl = new(youtubePlaylistBaseUrl, youtubePlaylistId);
    }

    public Uri YoutubePlaylistUrl => _youtubePlaylistUrl ?? throw new InvalidOperationException("YouTube playlist ID not set.");

    private readonly Uri? _youtubePlaylistUrl;

    public List<YoutubeVideo> GetEpisodesFromPlaylist()
    {
        string playlistJson = GetPlaylistWithYtdlp();
        List<YoutubeVideo> episodes = [];

        using (StringReader reader = new(playlistJson))
        {
            string? line;
            while ((line = reader.ReadLine()) is not null)
            {
                var video = JsonConvert.DeserializeObject<YoutubeVideo>(line);
                if (video is not null)
                {
                    episodes.Add(video);
                }
            }
        }

        return episodes;
    }

    private string GetPlaylistWithYtdlp()
    {
        ProcessStartInfo processStartInfo = new()
        {
            FileName = Command,
            Arguments = $"{OptionFlatPlaylist} {OptionDumpJson} {YoutubePlaylistUrl}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process process = new()
        {
            StartInfo = processStartInfo
        };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException($"yt-dlp command failed with error: {error}");
        }

        return output;
    }
}
