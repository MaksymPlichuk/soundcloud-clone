using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;
using System.Runtime.CompilerServices;

namespace Soundcloud_Clone.DAL.Initializer;

public static class Seeder
{
    public static async Task SeedAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        await context.Database.MigrateAsync();


        if (!await context.Songs.AnyAsync())
        {
            var listener = new UserEntity
            {
                UserName = "listener1",
                Email = "listener@gmail.com",
            };
            await userManager.CreateAsync(listener, "qwerty");

            var song = new SongEntity
            {
                Name = "Never Gonna Give You Up",
                Length = 213.5,
                Comments = new List<CommentEntity>
                {
                    new CommentEntity
                    {
                        TimeCode = 45.2,
                        CommentText = "Best song ever!",
                        Author = listener
                    }
                }
            };

            var album = new AlbumEntity
            {
                Name = "Mini Album",
                Description = "Test description",
                Songs = new List<SongEntity> { song }
            };

            var artist = new UserEntity
            {
                UserName = "artist1",
                Email = "artist@gmail.com",
                Albums = new List<AlbumEntity> { album },
                Songs = new List<SongEntity> { song }
            };

            await userManager.CreateAsync(artist, "qwerty");
        }
    }
}