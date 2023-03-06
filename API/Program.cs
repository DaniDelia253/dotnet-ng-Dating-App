using API.Extentions;

//as this gets more full, I will extract some of the logic here into extension methods
//extention methods allow me to extend a class in some way (could be a built-in class or one I wrote)
//in this class, I want to extend this Services class (of type IServiceCollection)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));

//add auth middelware after CORS adn before mapcontrollers
app.UseAuthentication();
//^do you have a valid token?
app.UseAuthorization();
//^you have a valid token, but what are you allowed to do?

app.MapControllers();

app.Run();
