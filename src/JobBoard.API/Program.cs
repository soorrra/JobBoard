var builder = WebApplication.CreateBuilder(args);

// controllers support
builder.Services.AddControllers();

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