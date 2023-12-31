﻿using DiscordAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAccess.Handlers;
public class CommandHandler
{
	private readonly Dictionary<string, MethodInfo> _commands = new Dictionary<string, MethodInfo>();

	public void RegisterCommands(object instance)
	{
		var methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
			.Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0);

		foreach (var method in methods)
		{
			var attribute = (CommandAttribute)method.GetCustomAttributes(typeof(CommandAttribute), false).First();
			_commands.Add(attribute.Name, method);
		}
	}

	public Dictionary<string, MethodInfo> GetCommands() => _commands;

	public async Task ExecuteCommand(string commandName, object instance, params object[] parameters)
	{
		if (_commands.TryGetValue(commandName, out var method))
		{
			var result = method.InvokeAsync(instance, parameters); 
			await result;
		}
		else
		{
			Console.WriteLine($"Command '{commandName}' not found");
		}
	}


}
