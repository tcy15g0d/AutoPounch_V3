using System.Text;
using Newtonsoft.Json;

namespace AutoPounch_V3
{
    internal partial class Program
    {
        static async Task SendTelegram(string botToken, string chatId, string message)
        {
            if (string.IsNullOrWhiteSpace(botToken) || string.IsNullOrWhiteSpace(chatId))
                return;

            try
            {
                using HttpClient client = new HttpClient();
                string url = $"https://api.telegram.org/bot{botToken}/sendMessage";

                string jsonBody = JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    { "chat_id", chatId },
                    { "text", message }
                });

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"【Telegram 通知失敗】：{response.StatusCode}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"【Telegram 通知失敗】：{ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
