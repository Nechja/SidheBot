
using DataStore.Models;
using DataStore.Services;
using DiscordAccess.Models;
using DiscordAccess.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class Program
{
	public static async Task Main(string[] args)
	{
		var configuration = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.Build() ?? throw new NullReferenceException("No connection String Found");


		var services = new ServiceCollection();

		services.AddLogging(config =>
		{
			config.AddConsole();
			config.AddDebug();
			config.SetMinimumLevel(LogLevel.Information);
		});

		services.AddSingleton<IDiscordSocketClient, DiscordClient>();
		services.AddSingleton<DiscordConnection>(serviceProvider =>
			new DiscordConnection(
		   serviceProvider.GetRequiredService<IDiscordSocketClient>(),
		   configuration["BotConfig:Token"],
		   ulong.Parse(configuration["BotConfig:GuildId"]),
		   serviceProvider.GetRequiredService<ILogger<DiscordConnection>>()
	   ));

		services.AddSingleton<IDatabase, FlatFile>();
		services.AddSingleton<DataAccess>();


	

		var serviceProvider = services.BuildServiceProvider();

		var discordConnection = serviceProvider.GetService<DiscordConnection>();
		var db = serviceProvider.GetService<DataAccess>();



		await Task.Delay(-1);
	}
}
