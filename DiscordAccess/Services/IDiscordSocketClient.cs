namespace DiscordAccess.Services;

public interface IDiscordSocketClient
{
	public Task LogInAsync(string token);
	public Task StartAsync();

	public void SetGuildId(ulong guildId);
} 
