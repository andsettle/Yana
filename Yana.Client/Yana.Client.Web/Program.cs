using Yana.Client.Shared.Services;
using Yana.Client.Web.Components;
using Yana.Client.Web;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp =>
{
    HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7071/") }; // <--- REPLACE YOUR_API_PORT

    // If you need to include credentials (cookies, auth headers) with cross-origin requests,
    // you might use something like this, though for basic GET it's not strictly necessary.
    // var handler = new HttpClientHandler { AllowAutoRedirect = false }; // Or more advanced setups
    // httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:YOUR_API_PORT/") };

    return httpClient;
});

builder.Services.AddScoped<INotesService, NotesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Yana.Client.Shared._Imports).Assembly);

app.Run();
