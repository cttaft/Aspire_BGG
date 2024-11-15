using Frontend;
using Frontend.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.Extensions.AI;
using OllamaSharp;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddOllamaSharpChatClient("ollama");
builder.AddRedisDistributedCache(connectionName: "cache");
builder.Services.AddHttpClient<BggApiClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://backend");
});


builder.Services.AddFluentUIComponents();
builder.Services.AddRazorComponents().AddInteractiveServerComponents(); 




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
    .AddInteractiveServerRenderMode();

app.Run();