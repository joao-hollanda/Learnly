using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consumindoIA.Domain
{
    public class ChatRequest
    {
        public string model { get; set; } = "";
        public List<Message> messages { get; set; } = new();
        public double temperature { get; set; }
    }
}