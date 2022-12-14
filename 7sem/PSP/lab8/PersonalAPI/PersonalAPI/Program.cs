using PersonalAPI.Models;
using PersonalAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var personalSonnectionString = builder.Configuration.GetConnectionString("Personal");
builder.Services.AddSingleton(new PersonalServiceOptions { ConnectionString = personalSonnectionString });
builder.Services.AddTransient<IPersonalService, PersonalService>();
var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/personal/", async (IPersonalService personalService) =>
{
    return await personalService.GetAsync();
})
.WithOpenApi();

app.MapGet("/personal/{personId}", async (int personId, IPersonalService personalService) =>
{
    var person = await personalService.GetAsync(personId);
    if(person == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(person);
})
.WithOpenApi();

app.MapDelete("/personal/{personId}", async (int personId, IPersonalService personalService) =>
{
    await personalService.DeleteAsync(personId);
})
.WithOpenApi();

app.MapPost("/personal/", async (Personal personal, IPersonalService personalService) =>
{
    return await personalService.CreateAsync(personal);
})
.WithOpenApi();

app.MapPut("/personal/", async (Personal personal, IPersonalService personalService) =>
{
    await personalService.UpdateAsync(personal);
})
.WithOpenApi();

app.Run();
