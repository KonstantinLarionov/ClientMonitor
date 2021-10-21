using ClientMonitor.Application.Abstractions;
using Telegram.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ClientMonitor.Infrastructure.Notifications.Adaptors
{
    public class TelegramAdaptor : INotification
    {
        readonly string Token = "2007180917:AAGZJCmAeZRnR8gx5QPryo9FvtKj36xJbuY";

        TelegramBotClient Client;

        public TelegramAdaptor()
        {
            Client = new TelegramBotClient(Token);
        }

        public void SendMassage(string to, string massage)
        {
            Client.SendTextMessageAsync(to, massage);
        }
    }
}
