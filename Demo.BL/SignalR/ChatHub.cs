using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMsg(string fromUser , string message)
        {
            await Clients.All.SendAsync("RecieveMessage" , fromUser , message);
        }
    }
}
