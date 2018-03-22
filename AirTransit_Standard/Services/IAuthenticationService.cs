using System;
using AirTransit_Standard.Models;

namespace AirTransit_Standard.Services
{
    interface IAuthenticationService
    {
        bool CheckIfKeysExist();
        bool SignUp();
    }
}