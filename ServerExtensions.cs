using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ListChallengeApi.Models;
using ListChallengeApi.Contracts;
using ListChallengeApi.Repository;

namespace ListChallengeApi
{
	public static class ServerExtensions
	{
		public static void ConfigureMySql(this IServiceCollection services, IConfiguration config)
		{
			var connectionString = config["mysqlconnection:connectionString"];

			services.AddDbContext<RepositoryContext>(options => {
					options.UseMySql(connectionString);
			});
		}
		public static void ConfigureRepository(this IServiceCollection services)
		{
				services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
		}
		public static void ConfigureCors(this IServiceCollection services)
		{
			
		}
	}
}