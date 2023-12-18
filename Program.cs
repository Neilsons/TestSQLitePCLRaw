using Test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDapperDBContext(options => {
    options.Configuration = "Data Source=AppAuthAssist.db";
    options.DataBaseAddress = "AppAuthAssist.db";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
