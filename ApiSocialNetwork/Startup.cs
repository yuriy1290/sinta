using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Prometheus;

namespace ApiSocialNetwork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Этот метод вызывается во время выполнения для добавления сервисов в контейнер.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<Models.SocialNetworkContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("con")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Инициализация Firebase
            var firebaseCredentials = GoogleCredential.FromFile("Push/socialnetwork-40154-firebase-adminsdk-tpfbq-5e902076ce.json");
            FirebaseApp.Create(new AppOptions
            {
                Credential = firebaseCredentials,
            });

            services.AddSingleton(FirebaseMessaging.DefaultInstance);
            services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));
        }

        // Этот метод вызывается во время выполнения для настройки HTTP-запросов.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics(); // Эндпоинт для Prometheus метрик
            });

            app.UseHttpMetrics(); // Метрики Prometheus для HTTP запросов
        }
    }
}