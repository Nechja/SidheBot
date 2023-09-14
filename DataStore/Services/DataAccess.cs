using DataStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Services;
public class DataAccess
{
	IDatabase _database;
	public DataAccess(IDatabase database)
	{
		_database = database;
	}

	public void AddCommand(CommandModel command)
	{
		_database.AddCommand(command);
	}

	public void DeleteCommand(CommandModel command) { }

	public void UpdateCommand(CommandModel command) { }

	public List<CommandModel> GetCommands()
	{
		return _database.GetCommands();
	}
}

public interface IDatabase
{
	public void AddCommand(CommandModel command);
	public List<CommandModel> GetCommands();
}

public class FlatFile: IDatabase
{
	private string commandCollection = "database.json";
	private string messageCollection = "messages.json";
	private string postCollection = "posts.json";
	private string emojiCollection = "emoji.json";
	private string ranksCollection = "ranks.json";

	public void AddCommand(CommandModel command)
	{
		List<CommandModel> commands = GetCommands();
		commands.Add(command);
		File.WriteAllText(commandCollection, JsonConvert.SerializeObject(commands));
	}

	public List<CommandModel> GetCommands()
	{
		var commands = JsonConvert.DeserializeObject<List<CommandModel>>(File.ReadAllText(commandCollection));
		return commands ?? new List<CommandModel>();
	}

	public void DeleteCommand(CommandModel command)
	{
		List<CommandModel> commands = GetCommands();
		commands.Remove(command);
		File.WriteAllText(commandCollection, JsonConvert.SerializeObject(commands));
	}
}