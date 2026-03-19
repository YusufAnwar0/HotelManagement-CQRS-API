using Application.Helpers;
using Infrastructure.Helpers;
using Presentation.Helpers;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// 1. Register Layers Dependencies
// --------------------
builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// --------------------
// 2. Build the App
// --------------------
var app = builder.Build();

await app.Services.InitializeDatabaseAsync();

app.UseExceptionHandler();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();