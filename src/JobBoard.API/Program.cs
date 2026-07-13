using JobBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// controllers support
builder.Services.AddControllers();

// PostgreSQL + EF Core
builder.Services.AddDbContext<JobBoardDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// visable endpoint for swagger
builder.Services.AddEndpointsApiExplorer();
// generate swagger documentation
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // generate.json specification for swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// connetion to the controllers
app.MapControllers();

app.Run();