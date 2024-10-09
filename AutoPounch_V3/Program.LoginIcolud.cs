using AutoPounch_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoPounch_V3
{
    static partial class Program
    {
        //登入icloud，不知道為甚麼從這邊進去的打卡系統才會正常運作
        static async Task LoginIcloud(HttpClient httpClient, User user)
        {
            try
            {
                //定義網址
                string Params = $"login.php?data=";

                // 定義要傳遞的表單資料
                var formData = new Dictionary<string, string>
            {
                { "acc", $"{user.id}" },
                { "pwd", $"{user.pw}" }
            };
                // 使用FormUrlEncodedContent來將資料轉換為表單形式
                var content = new FormUrlEncodedContent(formData);

                // 發送 icloud 登入請求
                using HttpResponseMessage response = await httpClient.PostAsync(Params, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    //檢查是否登入成功
                    if (responseContent.Contains("<script>window.location.replace(\"index.php\");</script>"))
                    {
                        string pattern = @"window\.location\.replace\(""(.*?)""\);";
                        Match match = Regex.Match(responseContent, pattern);
                        string url = match.Groups[1].Value;

                        //需要導向 icloud 的首頁，否則在取得個人打卡專屬登入網址時會有問題
                        //Params = $"index.php";
                        Params = match.Groups[1].Value;
                        using HttpResponseMessage response_index = await httpClient.GetAsync(Params);

                        Console.WriteLine($"----------【{user.id}】----------");
                        Console.ForegroundColor = ConsoleColor.Green; // 設定文字顏色為綠色
                        Console.WriteLine("【登入成功】");
                        Console.ResetColor(); // 恢復預設文字顏色	
                    }
                    else
                    {
                        Console.WriteLine($"----------【{user.id}】----------");
                        Console.ForegroundColor = ConsoleColor.Red; // 設定文字顏色為紅色
                        Console.Write("【登入失敗】：請檢查設定檔是否正確");
                        Console.ResetColor(); // 恢復預設文字顏色						
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // 設定文字顏色為紅色
                    Console.WriteLine($"【HTTP請求失敗】：{response.StatusCode}");
                    Console.ResetColor(); // 恢復預設文字顏色	
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
