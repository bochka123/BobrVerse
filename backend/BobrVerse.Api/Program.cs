using BobrVerse.Api.Extensions;
using BobrVerse.Api.HostedServices;
using BobrVerse.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBobrVerseServices(builder.Configuration);
builder.Services.AddBobrVerseAzureBlobStorage(builder.Configuration);
builder.Services.AddHostedService<DailyLogsService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();
app.MapHub<QuestsHub>("/hubs/quests", options =>
{
    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
}).RequireCors("SignalRCorsPolicy");

app.SeedDatabase();

await app.RunAsync();