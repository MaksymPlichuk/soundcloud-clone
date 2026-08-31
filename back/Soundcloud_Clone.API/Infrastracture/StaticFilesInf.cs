using Microsoft.Extensions.FileProviders;

namespace Soundcloud_Clone.API.Infrastracture
{
    public static class StaticFilesInf
    {
        public static IApplicationBuilder UseStaticMedia(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var items = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>(StaticFilesSettings.SongCoverPath,StaticFilesSettings.WebSongCoverPath),
                new KeyValuePair<string, string>(StaticFilesSettings.AlbumPath,StaticFilesSettings.WebAlbumPath),
            };

            string storagePath = Path.Combine(env.ContentRootPath, StaticFilesSettings.ImageStoragePath);

            if (!Directory.Exists(storagePath)) { Directory.CreateDirectory(storagePath); }

            foreach (var item in items)
            {
                string path = Path.Combine(storagePath, item.Key);
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(path),
                    RequestPath = item.Value,
                });
            }

            string songStoragePath = Path.Combine(env.ContentRootPath, StaticFilesSettings.SongStoragePath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(songStoragePath),
                RequestPath = StaticFilesSettings.WebSongPath,
            });

            return app;
        }
    }
}
