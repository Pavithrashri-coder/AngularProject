using API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddApiVersioning();
builder.Services.AddOptionsPattern(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>

{
    options.AddDefaultPolicy(

        builder =>

        {

            builder

            .AllowAnyOrigin()

            .AllowAnyHeader()

            .AllowAnyMethod();

        });

});

// Configure Serilog


Log.Logger = new LoggerConfiguration()

.ReadFrom.Configuration(builder.Configuration)

.Enrich.FromLogContext()

//.Enrich.WithCorrelationIdHeader("CorrelationId")

.Enrich.WithProperty("Product", "PAL")

.Enrich.WithProperty("Application", "PURCHASE")

.CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
