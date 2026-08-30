var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Soundcloud_Clone.API.Repositories.ISongRepository, Soundcloud_Clone.API.Repositories.InMemorySongRepository>();
builder.Services.AddScoped<Soundcloud_Clone.API.Services.ISongService, Soundcloud_Clone.API.Services.SongService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
