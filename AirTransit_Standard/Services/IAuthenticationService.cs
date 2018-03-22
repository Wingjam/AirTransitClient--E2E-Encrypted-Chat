namespace AirTransit_Core.Services
{
    interface IAuthenticationService
    {
        bool CheckIfKeysExist();
        bool SignUp();
    }
}