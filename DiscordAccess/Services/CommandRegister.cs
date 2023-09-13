using Discord;
using Discord.WebSocket;
using DiscordAccess.Models;

namespace DiscordAccess.Services;

public class CommandRegister
{
	SocketGuild _guild;
	public CommandRegister(SocketGuild guild)
	{
		_guild = guild;
	}

	public async Task RegisterCommand(CommandObject command)
	{
		var guildCommand = new SlashCommandBuilder();
		var options = command.Options.Select(x => x.ConvertToSlashCommandOption()).ToArray();

		guildCommand.WithName(command.Name);
		guildCommand.WithDescription(command.Description);
		guildCommand.AddOptions(options);
		await _guild.CreateApplicationCommandAsync(guildCommand.Build());
	}

}