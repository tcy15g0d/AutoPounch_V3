using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AutoPounch_V3
{
    internal partial class Program
    {
        static async Task LoginAdm(HttpClient httpClient, string url)
        {
            //定義網址
            string Params = url;

            // 發送 打卡系統 登入請求
            using HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));

            if (!response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red; // 設定文字顏色為紅色
                Console.WriteLine($"【HTTP請求失敗】：{response.StatusCode}");
                Console.ResetColor(); // 恢復預設文字顏色
            }
            string responseContent = await response.Content.ReadAsStringAsync();
        }
    }
}
