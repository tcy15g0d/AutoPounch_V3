using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoPounch_V3.Models
{
    //internal class ResultMsgRootobject
    //{
    //    public ResultMsg[]? resultmsg { get; set; }
    //}
    internal class ResultMsg
    {
        public int? result { get; set; }
        public string? msg { get; set; }
        public string[]? data { get; set; }

    }
}
