using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Core
{
    interface ICore
    {
        String SendMessage(String phoneNumber, String message);
        String GetMessages();
        String DeleteMessages();
    }
}
