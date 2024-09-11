using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared_Layer.ApiServices.Authentication;
using Shared_Layer.ApiServices.User;
using Shared_Layer.Authentication;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //Add services IUserService,... from Shared-Layer
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7036/") });//api https address

           

            await builder.Build().RunAsync();
        }
    }
}
