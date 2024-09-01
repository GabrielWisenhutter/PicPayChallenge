using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PicPay.Domain.Extensions
{
    public class Notification
    {
        public Notification()
        {
            Messages = new List<string>();
        }
        [JsonIgnore]
        public List<string> Messages { get; set; }

        [JsonIgnore]
        public bool IsValid => Messages.Count == 0;

        public void AddNotification(string message)
        {
            Messages.Add(message);
        }
    }
}
