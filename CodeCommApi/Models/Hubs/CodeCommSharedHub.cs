using Microsoft.AspNetCore.SignalR;

namespace CodeCommApi;

public class CodeCommSharedHub:Hub
{
      public override Task OnConnectedAsync()
    {
        Clients.Caller.SendAsync("ReceiveConnectionId", Context.ConnectionId);
        return base.OnConnectedAsync();
    }
}
