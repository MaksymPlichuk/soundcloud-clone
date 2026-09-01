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
    string? connectionString = builder.Configuration.GetConnectionString("LocalDB");
    opt.UseNpgsql(connectionString);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<SongRepository>();

builder.Services.AddSingleton<MapperProfile>();

builder.Services.AddScoped<IImageService, ImageService>();


var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseStaticMedia(builder.Environment);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SeedAsync();
app.Run();
