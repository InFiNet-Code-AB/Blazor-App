using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared_Layer.ApiServices;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7036/") });//api https address

            //Add services IUserService,... from Shared-Layer
            builder.Services.AddScoped<IUserServices, UserServices>();

            await builder.Build().RunAsync();
        }
    }
}
