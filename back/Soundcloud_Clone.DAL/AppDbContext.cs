using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;

namespace Soundcloud_Clone.DAL;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<PlaylistEntity> Playlist { get; set; }
    public DbSet<SongEntity> Musics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CommentEntity>(e =>
        {
            e.HasKey(e => e.Id);
            e.Property(e=>e.CommentText).HasMaxLength(500);
            
            e.HasOne(e=>e.Author).WithMany(a=>a.Comments).HasForeignKey(e => e.AuthorId);
            e.HasOne(e=>e.Song).WithMany(m=>m.Comments).HasForeignKey(e => e.SongId);
        });
        modelBuilder.Entity<SongEntity>(e =>
        {
            e.HasKey(e => e.Id);
            e.Property(e=>e.Name).HasMaxLength(50);
            e.Property(e => e.Length).HasDefaultValue(0);
            
            e.HasMany(e=>e.Playlists).WithMany(p=>p.Songs).UsingEntity("SongPlaylists");
        });
        modelBuilder.Entity<PlaylistEntity>(e =>
        {
            e.HasKey(e => e.Id);
            e.Property(e => e.Name).HasMaxLength(50);
            e.Property(e=>e.Description).HasMaxLength(500);
            
            e.HasOne(e=>e.Author).WithMany(a => a.Playlists).HasForeignKey(e => e.AuthorId);
        });
        //todo identity
    }
}