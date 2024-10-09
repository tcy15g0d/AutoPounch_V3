using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoPounch_V3.Models;

namespace AutoPounch_V3
{
    static partial class Program
    {
        //讀取存放帳密的info.json檔
        static async Task<Users> ReadInfo(string path)
        {
            return ReadJson(path);
        }
        static Users ReadJson(string path)
        {
            Users users;
            using (StreamReader sr = new StreamReader(path))
            {
                var json = sr.ReadToEnd();
                users = JsonSerializer.Deserialize<Users>(json);

            }
            return users;
        }
    }
}
