using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Forum_VTB.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        private readonly TelegramBotClient _botClient;
        private const string BotToken = "6468967405:AAHa_SXmCGZSZxCI1tNYzgFOu2Ve8zWqBNI";
        private const string WebhookUrl = "http://10.55.1.8:5335/api/message/telegram";

        public TelegramBotController(TelegramBotService botService)
        {
            _botClient = new TelegramBotClient(BotToken);
        }

        [HttpPost("telegram")]
        public async Task<IActionResult> Webhook([FromBody] object entity)
        {
            var update = JsonConvert.DeserializeObject<Update>(entity.ToString());
            try
            {
                // При первом вызове метода Webhook, устанавливаем вебхук
                WebhookInfo webhookInfo = await _botClient.GetWebhookInfoAsync();
                if (string.IsNullOrEmpty(webhookInfo.Url))
                {
                    await _botClient.DeleteWebhookAsync();
                    await _botClient.SetWebhookAsync(WebhookUrl);
                }

                // Обработка входящего обновления
                // Пример: отправка ответного сообщения
                if (update.Message != null && !string.IsNullOrWhiteSpace(update.Message.Text))
                {
                    string message = "Вы сказали: " + update.Message.Text;
                    await _botClient.SendTextMessageAsync(update.Message.Chat.Id, message);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // Обработка ошибок при обработке входящего обновления
                // В реальных приложениях рекомендуется добавить логирование ошибок
                return BadRequest("Ошибка при обработке входящего обновления: " + ex.Message);
            }
        }

        //[HttpPost("telegram")]
        //public async Task<ActionResult> Update([FromBody] Update entity)
        //{
        //    var update = JsonConvert.DeserializeObject<Update>(entity.ToString());
        //    var chat = update.Message?.Chat;

        //    if (chat is null)
        //    {
        //        return Ok();
        //    }

        //    await _botClient.SendTextMessageAsync(chat.Id, "Всё работает", parseMode: ParseMode.Markdown);

        //    return Ok();
        //}
    }
}
