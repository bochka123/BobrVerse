using BobrVerse.Api.Models.Settings;
using BobrVerse.Auth;
using BobrVerse.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value.Auth);
builder.Services.AddDbContext<BobrVerseContext>(options => options.UseSqlServer("name=ConnectionStrings:BobrVerseDb"));
builder.Services.AddAuth();
builder.Services.AddAuthDbContext<BobrVerseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureAuth();

app.UseAuthorization();

app.MapControllers();

app.Run();
