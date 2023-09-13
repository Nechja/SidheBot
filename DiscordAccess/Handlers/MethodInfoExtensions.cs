using System.Reflection;

namespace DiscordAccess.Handlers;

public static class MethodInfoExtensions
{
	public static async Task<object> InvokeAsync(this MethodInfo methodInfo, object obj, params object[] parameters)
	{
		dynamic awaitable = methodInfo.Invoke(obj, parameters);
		if (methodInfo.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null)
		{
			await awaitable;
		}
		return awaitable;
	}
}