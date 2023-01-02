﻿namespace Mimbly.Api.Extensions;

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Mimbly.Application;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Common.Mappings;
using Mimbly.CoreServices.Authorization;
using Mimbly.CoreServices.PuppeteerServices;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Persistence.Repositories;
using PuppeteerSharp;
using Microsoft.AspNetCore.Authorization;
using Mimbly.CoreServices.Authorization;
using Mimbly.Api.AAD;
using Mimbly.Api.AAD.Helpers;
using Mimbly.Api.AAD.Mappings;

public static class PuppeteerExtensions
{
    public static async Task PreparePuppeteerAsync(this IServiceCollection service,
        IWebHostEnvironment hostingEnvironment)
    {
        // Downloads & Installs a chromium browser.
        var downloadPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "./puppeteer");
        var browserOptions = new BrowserFetcherOptions { Path = downloadPath };
        var browserFetcher = new BrowserFetcher(browserOptions);
        ExecutablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);
        await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
    }

    public static string? ExecutablePath { get; private set; }
}

public static class ServiceExtensions
{
    public static void ConfigureDataAccessManager(this IServiceCollection services) =>
          services.AddScoped<ISqlDataAccess, SqlDataAccess>();

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMimboxRepository, MimboxRepository>();
        services.AddScoped<IMimboxLocationRepository, MimboxLocationRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
        services.AddScoped<IMimboxErrorLogRepository, MimboxErrorLogRepository>();
        services.AddScoped<IEventLogRepository, EventLogRepository>();
    }

    public static void ConfigureCors(this IServiceCollection services, string allowedOrigins, IConfiguration config) =>
    services.AddCors(opts => opts.AddPolicy(allowedOrigins, policy =>
    {
        var corsUrl = config.GetValue<string>("CorsUrl");
        policy.WithOrigins(corsUrl);
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    }));

    public static void ConfigureAppDbContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnectionString"), b => b.MigrationsAssembly("Mimbly.Api")));

    public static void ConfigureNugetPackages(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddAutoMapper(typeof(AADMappingProfile).Assembly);
        services.AddMediatR(typeof(ApplicationMediatREntrypoint).Assembly);
    }

    public static void ConfigureAuthentication(this IServiceCollection services, IConfigurationRoot configurationBuilder)
    {
        var azureAd = configurationBuilder.GetSection("AzureAd");
        Console.WriteLine("AZUREAD OBJECT SHOULD BE HERE ->>>" + azureAd.ToString());

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(azureAd);
    }

    public static void ConfigurePuppeteer(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddControllersWithViews();
        services.AddScoped<ITemplateService, ViewTemplateService>();
        services.PreparePuppeteerAsync(environment).GetAwaiter().GetResult();
        services.Configure<RazorViewEngineOptions>(opt =>
        {
            opt.ViewLocationExpanders.Add(new ViewLocationExpander());
            opt.ViewLocationFormats.Add("/DocumentTemplates/{0}.cshtml");
        });
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<SwaggerExtension>();
    }

    public static void ConfigureAuthAttribute(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, GroupsPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, GroupsHandler>();
    }

    public static void ConfigureAccountService(this IServiceCollection services)
    {
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IGraphService, GraphService>();
        services.AddSingleton<IGraphHelper, GraphHelper>();
    }
}