using LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(n =>
{
    var _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDB");
    Directory.CreateDirectory(_folderPath);

    var _filePath = Path.Combine(_folderPath, builder.Configuration.GetSection("DbFileName").Value!);
    return new LiteDatabase(_filePath);
});

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
