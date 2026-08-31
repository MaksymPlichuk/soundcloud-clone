namespace Soundcloud_Clone.API.Infrastracture
{
    public static class StaticFilesSettings
    {
        public static string ImageStoragePath => "Media/Images"; //головна папка для фото
        public static string SongCoverPath => "Songcovers";
        public static string WebSongCoverPath => "/images/songcovers";

        public static string AlbumPath => "Albums"; //папка у Image
        public static string WebAlbumPath => "/images/albums";

        public static string SongStoragePath => "Media/Song"; //окрема папка для Video
        public static string WebSongPath => "/songs";
    }
}
