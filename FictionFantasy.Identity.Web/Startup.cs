﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static IdentityServer4.Models.IdentityResources;

namespace FictionFantasy.Identity.Web
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
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(new List<Client>
                {
                    new Client
                    {
                        ClientId = "ff-web",
                        ClientName = "Fiction Fantasy Web",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowedScopes = new List<string> { "ff-api" },
                        AccessTokenLifetime = (int)TimeSpan.FromDays(1).TotalSeconds
                    }
                }).AddInMemoryPersistedGrants()
                .AddInMemoryApiResources(new List<ApiResource>
                {
                    
                })
                .AddInMemoryIdentityResources(new List<IdentityResource>
                {
                    new OpenId(),
                    new Profile()
                }).AddTestUsers(new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "d0965302-30c3-11e9-b210-d663bd873d93",
                        Username = "jamesdev#1",
                        Password = "nottraining"
                    },
                    new TestUser
                    {
                        SubjectId = "d09656fe-30c3-11e9-b210-d663bd873d93",
                        Username = "leothegod",
                        Password = "sassmaster"
                    },
                    new TestUser
                    {
                        SubjectId = "d0965870-30c3-11e9-b210-d663bd873d93",
                        Username = "tim",
                        Password = "jamesbestfriend"
                    },
                    new TestUser
                    {
                        SubjectId = "d0965be0-30c3-11e9-b210-d663bd873d93",
                        Username = "moshe",
                        Password = "leosbro"
                    }
                });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentityServer();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
