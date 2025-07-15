using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Yana.Client.Services;
using Yana.Client.Shared.Services;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Yana.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the Yana.Client.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // Configure HttpClient with platform-specific base addresses and handlers
            builder.Services.AddSingleton(sp =>
            {
                var logger = sp.GetService<ILogger<HttpClient>>();
                
                HttpClient httpClient;

#if DEBUG 
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    // Create a custom handler for Android that bypasses SSL validation for development
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                    
                    httpClient = new HttpClient(handler);
                    var baseAddress = "http://10.0.2.2:5063/";
                    httpClient.BaseAddress = new Uri(baseAddress);
                    logger?.LogInformation($"Android: Setting HttpClient BaseAddress to {baseAddress} with custom SSL handler");
                }
                else if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    httpClient = new HttpClient();
                    var baseAddress = "https://localhost:7071/";
                    httpClient.BaseAddress = new Uri(baseAddress);
                    logger?.LogInformation($"Windows: Setting HttpClient BaseAddress to {baseAddress}");
                }
                else
                {
                    // for macos and ios dev default to localhost 
                    httpClient = new HttpClient();
                    var baseAddress = "https://localhost:7071/";
                    httpClient.BaseAddress = new Uri(baseAddress);
                    logger?.LogInformation($"iOS/macOS: Setting HttpClient BaseAddress to {baseAddress}");
                }
#else
                httpClient = new HttpClient();
                var baseAddress = "https://your-deployed-yana-api.azurewebsites.net/"; // REPLACE WITH YOUR ACTUAL DEPLOYED URL LATER
                httpClient.BaseAddress = new Uri(baseAddress);
                logger?.LogInformation($"Production: Setting HttpClient BaseAddress to {baseAddress}");
#endif
                
                // Set timeout for debugging connectivity issues
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                
                // Add default headers
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Yana-MAUI-Client");
                
                return httpClient;
            });

            // Register the NotesService for dependency injection
            builder.Services.AddScoped<INotesService, NotesService>();

            return builder.Build();
        }
    }
}
