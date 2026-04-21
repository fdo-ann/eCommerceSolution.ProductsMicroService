using BusinessLogicLayer;
using DataAccessLayer;
using ProductsMicroService.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Add data access layer and BLL
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();

var app = builder.Build();
app.UseExceptionHandlingMiddleware();

//Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
