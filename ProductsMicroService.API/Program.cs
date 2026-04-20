using BusinessLogicLayer;
using DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Add data access layer and BLL
builder.Services.AddDataAccessLayer();
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
