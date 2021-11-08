using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;
using XFin.API.DAL.Repositories;

namespace XFin.API
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
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddControllers(setupAction => setupAction.ReturnHttpNotAcceptable = true);

            services.AddDbContextPool<XFinDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("XFinDb"));
            });

            services.AddScoped<IAccountHolderRepository, AccountHolderRepository>();
            services.AddScoped<IInternalBankAccountRepository, InternalBankAccountRepository>();
            services.AddScoped<IInternalBankAccountSettingsRepository, InternalBankAccountSettingsRepository>();
            services.AddScoped<IExternalBankAccountRepository, ExternalBankAccountRepository>();
            services.AddScoped<IInternalTransactionRepository, InternalTransactionRepository>();
            services.AddScoped<IExternalTransactionRepository, ExternalTransactionRepository>();
            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
            services.AddScoped<IExternalPartyRepository, ExternalPartyRepository>();
            services.AddScoped<ITransactionService, TransactionsService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
