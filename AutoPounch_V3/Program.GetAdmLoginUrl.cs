using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutoPounch_V3
{
    internal partial class Program
    {
        //取得個人打卡專屬登入網址
        static async Task<string> GetAdmLoginUrl(HttpClient httpClient)
        {
            string Params = $"dm_system/dm_system.php";
            using HttpResponseMessage response = await httpClient.GetAsync(Params);

            // Handle success
            if (response.IsSuccessStatusCode)
            {

                //取得 response content
                var jsonResponse = await response.Content.ReadAsStringAsync();

                //解析 JSON 資料
                JObject jsonObject = JObject.Parse(jsonResponse);

                // 取得資料數組
                JArray dataArray = (JArray)jsonObject["data"];

                if (dataArray.Count() == 0)
                {
                    return "";
                }

                // 提取 sys_url 值
                string sysUrl = dataArray[0]["sys_url"].ToString();

                //Console.WriteLine("sys_url 值为：" + sysUrl);

                return sysUrl;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // 設定文字顏色為紅色
                Console.WriteLine($"【HTTP請求失敗】：{response.StatusCode}，取得個人打卡專屬登入網址失敗");
                Console.ResetColor(); // 恢復預設文字顏色	
            }

            return "";
        }
    }
}
