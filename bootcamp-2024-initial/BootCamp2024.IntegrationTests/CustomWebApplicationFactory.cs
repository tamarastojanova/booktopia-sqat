using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using BootCamp2024.Service.Interface;
using BootCamp2024.Service.Implementation;

namespace BootCamp2024.IntegrationTests
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseUrls("http://localhost:5001");
			builder.ConfigureServices(services =>
			{
				var descriptor = services.SingleOrDefault(
					d => d.ServiceType == typeof(IAuthorsService));

				if (descriptor != null)
				{
					services.Remove(descriptor);
				}

				services.AddSingleton<IAuthorsService, AuthorsService>();

				var descriptorBooks = services.SingleOrDefault(
					d => d.ServiceType == typeof(IBooksService));

				if (descriptorBooks != null)
				{
					services.Remove(descriptorBooks);
				}

				services.AddSingleton<IBooksService, BooksService>();
			});
		}

		public HttpClient CreateClientWithBaseAddress()
		{
			var client = CreateClient();
			client.BaseAddress = new Uri("http://localhost:47643/authors/");
			return client;
		}
	}
}
