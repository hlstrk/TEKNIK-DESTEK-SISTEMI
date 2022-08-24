using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeknikServis.MvcUI
{
    public class ChatHub : Hub
    {
        public void Send(string username, string message)
        {
            Clients.All.sendMessage(username, message);
        }
    }
}