using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPounch_V3.Models
{
    internal class User
    {
        public string? id { get; set; }
        public string? pw { get; set; }
        public string? line { get; set; }

        public User() { }
        public User(string id, string pwd)
        {
            this.id = id;
            this.pw = pwd;
            this.line = "";
        }
        public User(string id, string pwd, string line)
        {
            this.id = id;
            this.pw = pwd;
            this.line = line;
        }

    }
    internal class Users
    {
        public List<User>? users { get; set; }
    }
}
