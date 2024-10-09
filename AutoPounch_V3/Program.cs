using AutoPounch_V3.Models;
using System.Net;


namespace AutoPounch_V3
{
    internal partial class Program
    {
        public static async Task Main(string[] args)
        {
            //用來判斷要簽到、簽退的參數[1:簽到、2:簽退]
            int parameterValue = 1;

            if (args.Length < 1)
            {
                Console.WriteLine("在執行前，請先輸入一個參數 (1:簽到、2:簽退)。");
               
                return;
            }

            if (int.TryParse(args[0], out parameterValue))
            {
                // 在这里根据参数值 (parameterValue) 执行相应的操作
                if (parameterValue != 1 && parameterValue != 2)
                {
                    Console.WriteLine("參數無效，請輸入 1 或 2 。");
                    
                    return;
                }
            }
            else
            {
                Console.WriteLine("參數無效，請輸入 1 或 2 。");
                
                return;
            }


            //將帳密 json 讀入 Users Model
            Models.Users users = await ReadInfo("./info.json");

            foreach (var user in users.users)
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    CookieContainer = new CookieContainer()
                };
                using (HttpClient sharedClient = new HttpClient(handler) { BaseAddress = new Uri("https://icloud.dyu.edu.tw/") })
                {
                    await LoginIcloud(sharedClient, user);
                    string url = await GetAdmLoginUrl(sharedClient);

                    if (url != "")
                    {                    
                        await LoginAdm(sharedClient, url);

                        //string rscontent = "{\"result\":1,\"msg\":\"\\u7c3d\\u5230\\u6210\\u529f\\uff01\",\"data\":[]}";
                        //ResultMsg resultMsg = ReadContent(rscontent);
                        await Pounch(sharedClient, parameterValue);

                        Console.WriteLine();
                    }
                }
            }
            return ;
        }
    }
}