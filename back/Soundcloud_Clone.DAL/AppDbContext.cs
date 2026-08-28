using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;

namespace Soundcloud_Clone.DAL;

public class AppDbContext : IdentityDbContext<UserEntity, AppRole, string,
        AppUserClaim, AppUserRole, AppUserLogin,
        AppRoleClaim, AppUserToken>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<AlbumEntity> Playlist { get; set; }
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
            
            e.HasMany(e=>e.Albums).WithMany(p=>p.Songs).UsingEntity("SongAlbums");
        });
        modelBuilder.Entity<AlbumEntity>(e =>
        {
            e.HasKey(e => e.Id);
            e.Property(e => e.Name).HasMaxLength(50);
            e.Property(e=>e.Description).HasMaxLength(500);
            
            e.HasOne(e=>e.Author).WithMany(a => a.Albums).HasForeignKey(e => e.AuthorId);
        });


        modelBuilder.Entity<UserEntity>(b =>
        {
            // Each User can have many UserClaims
            b.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            b.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            b.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<AppRole>(b =>
        {
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        });
    }
}