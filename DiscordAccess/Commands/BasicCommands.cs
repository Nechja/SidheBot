using DiscordAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordAccess.Models;
using Discord.WebSocket;

namespace DiscordAccess.Commands;
public class BasicCommands
{
	[Command("greet", "Greets a user")]
	public async Task Greet(SocketSlashCommand command)
	{
		var channel = command.Channel;
		await channel.SendMessageAsync($"Hello {command.User.Username}!");
	}
}
