using DiscordAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Models;
public class CommandModel : ICommandObject
{
	public string Name { get; set; }
	public string Description { get; set; }
	public List<CommandOption> Options { get; set; } = new();
}


