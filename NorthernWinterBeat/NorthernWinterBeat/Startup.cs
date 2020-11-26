using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;

namespace NorthernWinterBeat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin", "AdministratorOnly");
                options.Conventions.AuthorizeFolder("/ParticipantRazor", "ParticipantOnly");
                options.Conventions.AuthorizeFolder("/VenueRazor", "VenueOnly");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("ADMIN"));
                options.AddPolicy("ParticipantOnly", policy => policy.RequireRole("PARTICIPANT"));
                options.AddPolicy("VenueOnly", policy => policy.RequireRole("VENUE"));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            

            services.AddScoped<IDataAccess, EFDataAccess>();
            services.AddScoped<IFestivalManager, FestivalManager>(); 
            services.AddScoped<IAuthorizationManager, AuthorizationManager>();
            services.AddSingleton<IBlobStorageManager>(new BlobStorageManager("artists", Configuration.GetConnectionString("nwbBlobStorage")));

            services.AddDbContext<NorthernWinterBeatConcertContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("NorthernWinterBeatConcertContext")));

            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration["ConnectionStrings:nwbBlobStorage:blob"], preferMsi: true);
                builder.AddQueueServiceClient(Configuration["ConnectionStrings:nwbBlobStorage:queue"], preferMsi: true);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
    internal static class StartupExtensions
    {
        public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
            {
                return builder.AddBlobServiceClient(serviceUri);
            }
            else
            {
                return builder.AddBlobServiceClient(serviceUriOrConnectionString);
            }
        }
        public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
            {
                return builder.AddQueueServiceClient(serviceUri);
            }
            else
            {
                return builder.AddQueueServiceClient(serviceUriOrConnectionString);
            }
        }
    }
}
