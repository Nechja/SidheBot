using Discord;

namespace DiscordAccess.Models;
public interface ICommandObject
{
	public string Description { get; set; }
	public string Name { get; set; }
	public List<CommandOption> Options { get; set; } 
}

