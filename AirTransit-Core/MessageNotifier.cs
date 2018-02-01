using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirTransit_Core
{
    public class MessageNotifier
    {
        private readonly Action<IEnumerable<string>> _notifyAction;
        
        public MessageNotifier(Action<IEnumerable<String>> notifyAction)
        {
            this._notifyAction = notifyAction;
        }

        public void NotifyObservers(IEnumerable<String> messages)
        {
            this._notifyAction(messages);
        }
    }
}
