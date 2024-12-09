# Podcast Updater

``` Mermaid
classDiagram
    class YouTubeService {
        +GetVideoDetails(videoUrl : string) : VideoDetails
        +AddToPlaylist(videoUrl : string, playlistId : string) : void
    }

    class AudioDownloader {
        +DownloadAudio(videoUrl : string, outputPath : string) : void
        +GetFileSize(filePath : string) : long
    }

    class GitHubService {
        +UpdateReadMe(episodeDetails : string) : void
        +UpdateRssFeed(xmlFilePath : string, episode : RssEpisode) : void
        +UploadFile(filePath : string, commitMessage : string) : void
    }

    class RssEpisode {
        +Title : string
        +Description : string
        +PublicationDate : DateTime
        +FileSize : long
        +Mp3Url : string
    }

    class RssFeedManager {
        +AddEpisode(episode : RssEpisode) : void
        +SaveRssFeed(filePath : string) : void
    }

    class PodcastAutomationWorkflow {
        +ProcessNewEpisode(videoUrl : string) : void
    }

    YouTubeService --> PodcastAutomationWorkflow
    AudioDownloader --> PodcastAutomationWorkflow
    GitHubService --> PodcastAutomationWorkflow
    RssFeedManager --> PodcastAutomationWorkflow
    RssEpisode --> RssFeedManager
```

``` Mermaid
classDiagram
    class Podcast {
        +Name : string
        +Episodes : List~PodcastEpisode~
        +AddEpisode(episode : PodcastEpisode) : void
    }

    class PodcastEpisode {
        +Number : int
        +Title : string
        +YouTubeUrl : string
        +PublicationDate : DateTime
        +FileSize : long
        +Mp3Path : string
    }

    class YouTubeVideo {
        +Id : string
        +Title : string
        +Url : string
        +PublicationDate : DateTime
    }

    class YouTubeService {
        +GetPlaylistVideos(playlistUrl : string) : List~YouTubeVideo~
    }

    class FileManager {
        +ReadMarkdown(filePath : string) : Podcast
        +WriteMarkdown(filePath : string, podcast : Podcast) : void
        +SaveMp3(videoUrl : string, outputPath : string) : void
        +GetFileSize(filePath : string) : long
    }

    class RssFeedManager {
        +UpdateFeed(podcast : Podcast, outputPath : string) : void
    }

    class GitHubService {
        +PushChanges(commitMessage : string, files : List~string~) : void
    }

    class Workflow {
        +Run(podcastMarkdownPath : string, rssFeedPath : string, playlistUrl : string) : void
    }

    Podcast "1" --> "many" PodcastEpisode
    YouTubeService --> Workflow
    FileManager --> Workflow
    RssFeedManager --> Workflow
    GitHubService --> Workflow
    YouTubeVideo --> YouTubeService
```
