namespace AggregationApi.Models.SpotifyDTO
{
    public class SpotifyTrackDTO
    {
      
        public string Artist { get; set; }
        public string Album { get; set; }
    }

    public class SpotifyApiResponse
    {
        public AlbumsInfo Albums { get; set; }
    }

    public class AlbumsInfo
    {
        public List<AlbumItem> Items { get; set; }
    }

    public class AlbumItem
    {
        public string Name { get; set; }
        public List<Artist> Artists { get; set; }
    }

    public class Artist { public string Name { get; set; } }
}
