using FastEndpoints;
using XYZ.Services.Dapper;
using FastEndpoints.Security;
using XYZ.Services.Auth;
using XYZ.Services.MongoDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();
builder.Services.AddJWTBearerAuth("TokenSigningKeyrwerwerqweqwtereqwewtyewewqrert");
builder.Services.AddSingleton<IDapper, DapperM>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IMongo, Mongo>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});

app.Run();