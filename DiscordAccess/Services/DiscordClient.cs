using Discord;
using Discord.WebSocket;
using DiscordAccess.Commands;
using DiscordAccess.Handlers;
using DiscordAccess.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Reflection.Metadata;

namespace DiscordAccess.Services;

public class DiscordClient : IDiscordSocketClient
{
	private DiscordSocketClient _client;
	private readonly ulong _channelId = 997287599691538525;
	private ulong _guildId;
	private readonly ILogger<DiscordClient> _logger;
	private bool _disposed;
	private bool _isConnected;
	private CommandHandler commander = new CommandHandler();

	public DiscordClient(ILogger<DiscordClient> logger)
	{
		_client = new DiscordSocketClient();
		_logger = logger;
	}

	public void SetGuildId(ulong guildId) => _guildId = guildId;

	public async Task LogInAsync(string token)
	{
		await _client.LoginAsync(TokenType.Bot, token);
		_client.Ready += ReadyAsync;
		_client.Log += LogAsync;
		_client.Disconnected += DisconnectedAsync;
		_client.SlashCommandExecuted += SlashCommandExecutedAsync;
	}

	private async Task SlashCommandExecutedAsync(SocketSlashCommand command)
	{
		var commandName = command.CommandName;
		try
		{
			await commander.ExecuteCommand(commandName, new BasicCommands(), command);
			_logger.LogInformation($"Command {commandName} executed by {command.User.Username}");
			await command.RespondAsync("Command Executed");
		}
		catch (Exception e)
		{
			_logger.LogError(e.Message);
		}

		
		
		
	}

	private Task DisconnectedAsync(Exception exception)
	{
		_isConnected = false;
		_logger.LogWarning("Client has dropped.");
		return Task.CompletedTask;
	}

	private Task LogAsync(LogMessage message)
	{
		_logger.LogInformation(message.Message);
		return Task.CompletedTask;
	}

	private async Task ReadyAsync()
	{
		_isConnected = true;
		_logger.LogInformation($"{_client.CurrentUser} is connected");
		await SendTestMessage("I am online!");
		var guild = _client.GetGuild(_guildId);
		var guildCommand = new SlashCommandBuilder();
		

		try
		{
			CommandRegister commandRegister = new CommandRegister(guild);
			var _botCommands = new BasicCommands();
			commander.RegisterCommands(_botCommands);
			var commands = commander.GetCommands();
			;
			foreach (var item in commands)
            {

				var commandAttribute = (CommandAttribute)item.Value.GetCustomAttributes(typeof(CommandAttribute), false).First();
				var commandobject = new CommandObject
				{
					Name = commandAttribute.Name,
					Description = commandAttribute.Description
				};
				commandRegister.RegisterCommand(commandobject);
			}
            
		}
		catch (Exception e)
		{
			_logger.LogError(e.Message);
		}

	}

	public async Task StartAsync()
	{
		await _client.StartAsync();
		
	}

	private async Task SendTestMessage(string message)
	{
		var channel = _client.GetChannel(997287599691538525) as IMessageChannel;
		await channel.SendMessageAsync(message);
	}




}
