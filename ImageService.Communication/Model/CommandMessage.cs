/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ImageService.Infrastructure.Enums;

namespace ImageService.Communication.Model
{
    public class CommandMessage
    {
        //General members
        public bool Status { get; set; }
        public CommandEnum Type { get; set; }
        public string Message { get; set; }
        public string[] Args { get; set; }

        //Config specific members
        public string OutputDir { get; set; }
        public string LogSource { get; set; }
        public string LogName { get; set; }
        public int ThumbSize { get; set; }
        public string[] Handlers { get; set; }

        //Log member
        public List<LogMessage> LogMessages { get; set; }

        /// <summary>
        /// The function converts a JSON message to a string
        /// </summary>
        /// <returns>a string message</returns>
        public string ToJSONString()
        {
            JObject jsonMessage = new JObject();
            jsonMessage["Status"] = Status;
            jsonMessage["Type"] = (int)Type;
            jsonMessage["Message"] = Message;
            jsonMessage["Args"] = JsonConvert.SerializeObject(Args);

            jsonMessage["OutputDir"] = OutputDir;
            jsonMessage["LogSource"] = LogSource;
            jsonMessage["LogName"] = LogName;
            jsonMessage["ThumbSize"] = ThumbSize;
            jsonMessage["Handlers"] = JsonConvert.SerializeObject(Handlers);

            jsonMessage["LogMessages"] = JsonConvert.SerializeObject(LogMessages);

            return jsonMessage.ToString();
        }

        /// <summary>
        /// The function converts a string message to a JSON message
        /// </summary>
        /// <param name="str">the string message</param>
        /// <returns>a JSON message</returns>
        public static CommandMessage FromJSONString(string str)
        {
            if (str == null)
            {
                return new CommandMessage
                {
                    Status = false,
                    Message = "Error while parsing JSON"
                };
            }
            JObject jsonMessage = JObject.Parse(str);
            return new CommandMessage
            {
                Status = (bool)jsonMessage["Status"],
                Type = (CommandEnum)((int)jsonMessage["Type"]),
                Message = (string)jsonMessage["Message"],
                Args = JsonConvert.DeserializeObject<string[]>((string)jsonMessage["Args"]),

                OutputDir = (string)jsonMessage["OutputDir"],
                LogSource = (string)jsonMessage["LogSource"],
                LogName = (string)jsonMessage["LogName"],
                ThumbSize = (int)jsonMessage["ThumbSize"],
                Handlers = JsonConvert.DeserializeObject<string[]>((string)jsonMessage["Handlers"]),

                LogMessages = JsonConvert.DeserializeObject<List<LogMessage>>((string)jsonMessage["LogMessages"])
            };
        }
    }
}
