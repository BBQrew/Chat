using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Hubs;

public class ChatHubs : Hub
{
    private static Dictionary<string, string> Users = new Dictionary<string, string>();

    //Login: verbindung zum Server
    public override async Task OnConnectedAsync()
    {
    https://localhost:7268/
        string username = Context.GetHttpContext().Request.Query["username"];
        Users.Add(Context.ConnectionId, username);
        await AddMessageToChat(string.Empty, $"{username} joined the party!");
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
        await AddMessageToChat(string.Empty, $"{username} left!");
    }
    public async Task AddMessageToChat(string user, string message)
    {
        await Clients.All.SendAsync("GetThatMessageDude", user, message);
    }
}