using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAccess.Models;
public class CommandObject : ICommandObject
{
	public string Name { get; set; }
	public string Description { get; set; }
	public List<CommandOption> Options { get; set; } = new List<CommandOption>();

}

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
