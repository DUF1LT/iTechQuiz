using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(iTechArt.iTechQuiz.WebApp.Areas.Identity.IdentityHostingStartup))]
namespace iTechArt.iTechQuiz.WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}