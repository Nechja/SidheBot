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
