using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class FlashMessage
    {
        public string Message { get; protected set; }
        public int Life { get; set; }

        public FlashMessage(string message, int life)
        {
            Message = message;
            Life = life;
        }
    }
}
