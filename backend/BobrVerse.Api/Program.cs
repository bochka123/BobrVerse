using BobrVerse.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBobrVerseServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.SeedDatabase();

app.Run();
