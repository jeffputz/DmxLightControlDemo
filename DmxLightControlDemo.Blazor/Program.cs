using DmxLightControlDemo.Blazor;
using DmxLightControlDemo.Core;
using DmxLightControlDemo.Core.NetworkInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var services = builder.Services;
services.AddSingleton<IConfig, Config>();
services.AddSingleton<INetworkInterface, SacnNetworkInterface>();

// This is what runs the background service to make the lights do things.
builder.Services.AddSingleton<MainHostedService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<MainHostedService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
