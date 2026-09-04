using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.API.Infrastracture;
using Soundcloud_Clone.API.Repositories;
using Soundcloud_Clone.API.Services;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.BLL.Services;
using Soundcloud_Clone.DAL;
using Soundcloud_Clone.DAL.Enitites.Identity;
using Soundcloud_Clone.DAL.Initializer;
using Soundcloud_Clone.DAL.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();


builder.Services.AddIdentity<UserEntity, AppRole>(opt =>
{
    opt.User.RequireUniqueEmail = false;
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    string connectionString = BuildConnectionString(builder.Configuration);
    opt.UseNpgsql(connectionString, npgsql => npgsql.EnableRetryOnFailure());
});


string CORSPolicy = "AllowAll";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(CORSPolicy, cfg =>
    {
        cfg.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<SongRepository>();

builder.Services.AddSingleton<MapperProfile>();

builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

app.UseCors(CORSPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseStaticMedia(builder.Environment);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

await app.SeedAsync();
app.Run();

static string BuildConnectionString(IConfiguration configuration)
{
    string? host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
    if (!string.IsNullOrWhiteSpace(host))
    {
        string port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
        string database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "soundcloud_clone";
        string username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
        string password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres";
        return $"Host={host};Port={port};Database={database};Username={username};Password={password};";
    }
    string? configured = configuration.GetConnectionString("LocalDB");
    if (string.IsNullOrWhiteSpace(configured))
    {
        throw new InvalidOperationException(
            "Connection string is not configured. Set ConnectionStrings:LocalDB in appsettings.json " +
            "or provide POSTGRES_HOST/POSTGRES_DB/POSTGRES_USER/POSTGRES_PASSWORD environment variables.");
    }
    return configured;
}