using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consumindoIA.Domain
{
    public class ChatResponse
    {
        public string model { get; set; } = "";
        public List<Choice> choices { get; set; } = new();
    }
}