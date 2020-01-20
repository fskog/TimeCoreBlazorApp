using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using TimeCore.Services;

namespace TimeCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CounterService, CounterService>();
            services.AddSingleton<TimerService, TimerService>();
            services.AddSingleton<TimeLogService, TimeLogService>();
            services.AddSingleton<CategoryService, CategoryService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
