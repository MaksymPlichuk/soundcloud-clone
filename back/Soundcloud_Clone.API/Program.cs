using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL;
using Soundcloud_Clone.DAL.Enitites.Identity;
using Soundcloud_Clone.DAL.Initializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
builder.Services.AddControllers();

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SeedAsync();
app.Run();
