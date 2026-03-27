using Newtonsoft.Json.Linq;

namespace AutoPounch_V3
{
    internal partial class Program
    {
        static async Task StartBotListener(string botToken)
        {
            Console.WriteLine("【Bot 監聽中】請對 Bot 傳送 /chatid 取得你的 Chat ID");
            Console.WriteLine("所有人取得後按 Ctrl+C 停止");
            Console.WriteLine();

            using HttpClient client = new HttpClient();
            long offset = 0;

            using CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    string url = $"https://api.telegram.org/bot{botToken}/getUpdates?offset={offset}&timeout=30";
                    using HttpResponseMessage response = await client.GetAsync(url, cts.Token);
                    string body = await response.Content.ReadAsStringAsync(cts.Token);

                    JObject json = JObject.Parse(body);
                    if (json["ok"]?.Value<bool>() != true) continue;

                    JArray? updates = json["result"] as JArray;
                    if (updates == null || !updates.Any()) continue;

                    foreach (JObject update in updates.Cast<JObject>())
                    {
                        offset = update["update_id"]!.Value<long>() + 1;

                        JObject? message = update["message"] as JObject;
                        if (message == null) continue;

                        string? text = message["text"]?.Value<string>();
                        long chatId = message["chat"]!["id"]!.Value<long>();
                        string? username = message["from"]?["username"]?.Value<string>()
                            ?? message["from"]?["first_name"]?.Value<string>()
                            ?? "unknown";

                        if (text == "/start" || text?.StartsWith("/start@") == true)
                        {
                            Console.WriteLine($"收到 /start，來自 @{username}，Chat ID：{chatId}");
                            await SendTelegram(botToken, chatId.ToString(), $"歡迎使用 AutoPounch Bot！\n\n傳送 /chatid 可取得你的 Chat ID，設定完成後即可收到打卡通知。");
                        }
                        else if (text == "/chatid" || text?.StartsWith("/chatid@") == true)
                        {
                            Console.WriteLine($"收到 /chatid，來自 @{username}，Chat ID：{chatId}");
                            await SendTelegram(botToken, chatId.ToString(), $"你的 Chat ID 是：{chatId}\n\n請將此 ID 填入 info.json 的 telegram 欄位。");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"【輪詢錯誤】：{ex.Message}，3秒後重試...");
                    Console.ResetColor();
                    await Task.Delay(3000).ContinueWith(_ => { });
                }
            }

            Console.WriteLine("【Bot 監聽已停止】");
        }
    }
}
