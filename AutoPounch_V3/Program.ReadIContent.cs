using AutoPounch_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace AutoPounch_V3
{
    internal partial class Program
    {
        public static ResultMsg ReadContent(string content)
        {
            ResultMsg resultMsg = new ResultMsg();
            resultMsg = JsonSerializer.Deserialize<ResultMsg>(content);
            return resultMsg;
        }
    }
}
