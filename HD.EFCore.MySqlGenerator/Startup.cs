using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HD.EFCore.MySqlGenerator
{
    public class Startup
    {
        TestOperationReporter _reporter;

        public Startup(IConfiguration configuration)
        {
            _reporter = new TestOperationReporter();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //ef core 代码生成的依赖注入
            services
                .AddSingleton<IOperationReporter>(_reporter)
                .AddScaffolding(_reporter)
                .AddLogging();
            new Microsoft.EntityFrameworkCore.Design.Internal.MySqlDesignTimeServices().ConfigureDesignTimeServices(services);
            services.AddSingleton(typeof(IFileService), sp => new FileSystemFileService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
