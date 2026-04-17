using MyResilientAPI.Services;
using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace MyResilientAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddHttpClient("my-client")
            .AddStandardResilienceHandler();
        builder.Services.AddScoped<ResilienceService>();
        builder.Services.AddResiliencePipeline("my-strategy", pipelineBuilder =>
        {
            pipelineBuilder
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    Delay = new TimeSpan(2000)
                })
                .AddTimeout(new TimeoutStrategyOptions
                {
                    Timeout = new TimeSpan(5000)
                });
        });

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
