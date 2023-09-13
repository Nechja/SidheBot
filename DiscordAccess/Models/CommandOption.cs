using Discord;

namespace DiscordAccess.Models;

public class CommandOption
{
	public string Name { get; set; }
	public string Description { get; set; }
	public ApplicationCommandOptionType Type { get; set; }
	public bool IsRequired { get; set; }
	public List<CommandOptionChoice> Choices { get; set; } = new List<CommandOptionChoice>();

	public CommandOption(string name, string description, ApplicationCommandOptionType type, bool isRequired)
	{
		Name = name;
		Description = description;
		Type = type;
		IsRequired = isRequired;
	}

	public SlashCommandOptionBuilder ConvertToSlashCommandOption()
	{
		var builder = new SlashCommandOptionBuilder();
		builder.WithName(Name);
		builder.WithDescription(Description);
		builder.WithType((Discord.ApplicationCommandOptionType)Type);
		builder.IsRequired = IsRequired;
		foreach (var choice in Choices)
		{
			builder.AddChoice(choice.Name, choice.Value);
		}

		return builder;
	}

}
