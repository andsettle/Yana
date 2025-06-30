using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Yana.Client.Services;
using Yana.Client.Shared.Services;

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
            builder.Services.AddSingleton(sp =>
            {
                HttpClient httpClient = new HttpClient();

#if DEBUG 
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    httpClient.BaseAddress = new Uri("https://10.0.2.2:7071/");
                }
                else if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7071/");
                }
                else
                {
                    // for macos and ios dev default to localhost 
                    httpClient.BaseAddress = new Uri("https://localhost:7071");
                }
#else
            httpClient.BaseAddress = new Uri("https://your-deployed-yana-api.azurewebsites.net/"); // REPLACE WITH YOUR ACTUAL DEPLOYED URL LATER
#endif
                return httpClient;
            });

            return builder.Build();
        }
    }
}
