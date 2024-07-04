using Microsoft.EntityFrameworkCore;
using StoryTrails.API.Filters;
using StoryTrails.Application;

using StoryTrails.Domain.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<Repository>(options => options.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=StoryTrailsDatabase;User Id=postgres;Password=postgres"));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddApplication();


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

app.Run();
