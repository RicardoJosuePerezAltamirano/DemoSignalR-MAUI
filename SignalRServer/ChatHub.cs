using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("MessageReceived", message);
        }
        public Task SendPrivateMessage(string group, string message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }
        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
