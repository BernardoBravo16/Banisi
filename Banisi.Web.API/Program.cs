using Banisi.Web.API.Shared.ErrorHandling.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Reflection;
using Banisi.Web.API.Shared.Filters.OpenApi;
using Banisi.Web.API.Shared.Initialize;
using Azure.Identity;
using Banisi.Persistence;
using Banisi.Common.Configuration;
using Amazon.CognitoIdentityProvider;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

var BanisiOrigins = "_BanisiOrigins";

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<OpenAPIHeadersFilter>();
});

builder.Services.Configure<GeneralSettings>(builder.Configuration.GetSection(GeneralSettings.SectionName));

builder.Services.AddAWSService<IAmazonCognitoIdentityProvider>();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();


builder.Services.AddDbContext<DatabaseContext>(
    options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(BanisiOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
});

builder.Services
    .AddPersistence()
    .AddInfrastructure()
    .AddApplication();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(BanisiOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthz");

app.Run();
