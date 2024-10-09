using AutoPounch_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace AutoPounch_V3
{
    internal partial class Program
    {
        static async Task Pounch(HttpClient httpClient,int parameterValue)
        {
            //




            //定義Header {Content-Type:application/x-www-form-urlencoded; charset=UTF-8}       
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            //定義網址
            string Params = $"https://adm_acc.dyu.edu.tw/budget/prj_epfee/kernel/kernel_prj_carddata_edit.php?page=NDgy";

            // 定義要傳遞的表單資料
            String jsonString = JsonConvert.SerializeObject(
                   new Dictionary<string, string>()
                   {
                       { "type", $"{parameterValue}" }
                   }
               );
            // 使用StringContent來將資料轉換為content形式
            var content = new StringContent(jsonString, Encoding.UTF8);

            using HttpResponseMessage response = await httpClient.PostAsync(new Uri(Params), content);
            string responseContent = await response.Content.ReadAsStringAsync();
            
            ResultMsg resultMsg = ReadContent(responseContent);
            if(resultMsg.result != 1 )
            {
                Console.ForegroundColor = ConsoleColor.Red; // 設定文字顏色為紅色
                Console.WriteLine($"【簽到失敗】：{resultMsg.msg}");
                Console.ResetColor(); // 恢復預設文字顏色
            }
            else if(resultMsg.result == 0 )
            {
                Console.ForegroundColor = ConsoleColor.Green; // 設定文字顏色為紅色
                Console.WriteLine($"【簽到成功】：{resultMsg.msg}");
                Console.ResetColor(); // 恢復預設文字顏色
            }


        }
    }
}
