using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirTransit_Core
{
    public class Core
    {
        private readonly Task _getMessagesTask;
        private bool Running = false;

        public Core(Action<IEnumerable<String>> notifyAction)
        {
            this._getMessagesTask = new Task(() =>
            {
                while (Running)
                    GetMessages(notifyAction);
            });
        }


        public void Stop()
        {
            Running = false;
            this._getMessagesTask.Wait();
        }

        public void GetMessages(Action<IEnumerable<String>> notifyAction)
        {
            var messages = new string[] { "yolo", "1234" };
            notifyAction(messages);
        }
    }
}
