
using UnityEngine;

namespace OnlineService
{
    public abstract class ALoginSystem
    {
        public string PlayerId;
        public string pushToken;
        public IChatSystem chat;

        //functions for LogIn/Out & Register
        public abstract void CreateAccountByEmail(string emil,string password);
        public abstract void LoginByEmail(string email, string password);
        public abstract void LogInByFacebook();
        public abstract void LogInByGoogle();
        public abstract void Logout();

        //functions for Get PlayerData
        public abstract void GetUserInventory();
        public abstract void GetPlayerProfile();
        public abstract void SaveAvatarData();
        public abstract void LoadAvatarData();

        //function for Push notfication
        public abstract void RegisterForPush();


    }
}