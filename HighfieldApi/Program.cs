using RecruitmentApiClient;
using UserDataProcessor;

var builder = WebApplication.CreateBuilder(args);

 builder.Services.AddCors(options =>
 {
    options.AddPolicy("AllowAnyOriginPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
  });

// Add services to the container.
builder.Logging.AddConsole();
ConfigureServices(builder.Services);

var app = builder.Build();

app.UseCors("AllowAnyOriginPolicy");

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
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddApiClient();
    services.AddUserDataProcessor();

    services.AddMemoryCache();
    
}