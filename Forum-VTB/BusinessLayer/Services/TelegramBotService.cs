using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BusinessLayer.Services
{
    public class TelegramBotService
    {
        private readonly IConfiguration _configuration;
        private TelegramBotClient _botClient;

        public TelegramBotService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TelegramBotClient> GetBot()
        {
            if (_botClient != null)
            {
                return _botClient;
            }

            _botClient = new TelegramBotClient("6468967405:AAHa_SXmCGZSZxCI1tNYzgFOu2Ve8zWqBNI");
            var hook = $"https://puella.serveo.net/api/message";
            await _botClient.SetWebhookAsync(hook);
            return _botClient;
        }
    }
}
