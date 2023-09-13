using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace DiscordAccess.Services;
public class DiscordConnection
{
	private IDiscordSocketClient _client;
	private string _token;
	private readonly ILogger<DiscordConnection> _logger;
	public DiscordConnection(IDiscordSocketClient client, string token, ulong guildId, ILogger<DiscordConnection> logger)
	{
		_client = client;
		_token = token;
		_logger = logger;
		_client.SetGuildId(guildId);
		_client.LogInAsync(_token);
		_client.StartAsync();

	}
}
