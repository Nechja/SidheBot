namespace DiscordAccess.Models;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CommandAttribute : Attribute
{
	public string Name { get; }
	public string Description { get; }
	public CommandOption Options { get; }

	public CommandAttribute(string name, string description)
	{
		Name = name;
		Description = description;
	}
}
