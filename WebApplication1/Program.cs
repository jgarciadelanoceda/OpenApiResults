using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

app.MapOpenApi();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapPost("/api/multipleFromForm", ([FromForm] Person person, [FromForm] Address address) =>
{
	return $"{person.LastName} {address.City}";
});

app.MapPost("/api/FromForm", ([FromForm] Person person) =>
{
	return person.LastName;
});

app.MapPost("/api/FromFile", (IFormFile file) =>
{
	return file.Name;
});

app.MapPost("api/FromFileCollection", (IFormFileCollection collection) =>
{
	return $"{collection.Count} {string.Join(',', collection.Select(s => s.FileName))}";
});

app.MapControllers();

app.Run();

[ApiController]
[Route("api/[controller]")]
public class EnumController : ControllerBase
{
	[HttpGet]
	public IResult Get(LogLevel? logLevel = LogLevel.Error) => Results.Ok(logLevel);
}

public record class Person(string FirstName, string LastName);

public record class Address(string Street, string City, string State, string ZipCode);
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
