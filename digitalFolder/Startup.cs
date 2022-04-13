using DigitalFolder.Data;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace digitalFolder
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
            services.AddControllers();

            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseNpgsql(Configuration["ConnectionStrings:DigitalFolder"]));

            services.AddIdentity<IdentityUser<int>,IdentityRole<int>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "digitalFolder", Version = "v1" });
            });

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 8;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Checar ess método 
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<SignOutService, SignOutService>();
            services.AddScoped<SignInService, SignInService>();
            services.AddScoped<SignUpService, SignUpService>();
            services.AddScoped<WalletService, WalletService>();
            services.AddScoped<TransactionService, TransactionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // app.UseSwagger();
               // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "digitalFolder v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
