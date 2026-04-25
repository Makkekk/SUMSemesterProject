using LajmiBlazorWebApp.Components;
using LajmiBlazorWebApp.Client.Services;

namespace LajmiBlazorWebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        
        // delt service pr. bruger (1 kurv pr bruger)
        builder.Services.AddScoped<CartService>();
        
        builder.Services.AddScoped<ProductService>();
        
        builder.Services.AddScoped<IOrderService, OrderService>();
        
        builder.Services.AddScoped<CalculateDiscountService>();

        builder.Services.AddScoped<DiscountAgreementService>();

        builder.Services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5055")
            });
        //Service for bruger log-in gemmes i session. vi bruger singelton fordi scoped vil oprette en ny session pr. http-request
        builder.Services.AddSingleton<UserSessionService>();

        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}