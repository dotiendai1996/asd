using DeviceAbriDoor.Model.Authorization;
using System;

namespace DeviceAbriDoor.RestSharp
{
    public class AuthenlicateHelper
    {
        private static AuthenlicateHelper instance = new AuthenlicateHelper();
        public static AuthenlicateHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthenlicateHelper();
                }
                return instance;
            }
        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public AuthenticateResultModel User { get; set; }
        public AuthenlicateHelper() { }

        public void Login(string userName, string password)
        {
            var data = WebApiHelper.Instance.Post("api/TokenAuth/Authenticate", new
            {
                userNameOrEmailAddress = userName,
                password = password,
                rememberClient = false,
                singleSignIn = false
            }).Result.ToObjectModel<AuthenticateResultModel>();

            //User = StorageManager.Instance.AdminSession = data;
            UserName = userName;
            Token = data.AccessToken;
            SystemToken();
        }

        public void SystemToken()
        {
            WebApiHelper.Instance.SetToken(Token);
        }

        public bool IsAuthenlication()
        {
            return !Token.IsNullOrWhiteSpace();
        }

    }
}
