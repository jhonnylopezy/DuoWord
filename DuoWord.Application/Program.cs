
using FastEndpoints;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();

var endpoint = builder.Build();
endpoint.UseFastEndpoints();
endpoint.Run();