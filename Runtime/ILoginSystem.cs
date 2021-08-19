namespace OnlineService {
    public interface ILoginSystem
    {
        //functions for LogIn/Out & Register
        void CreateAccountByEmail(string email,string password);
        void LoginByEmail(string email, string password);
        void LogInByFacebook();
        void LogInByGoogle();
        void Logout();
        
        //functions for Get PlayerData
        void GetUserInventory();
        void GetPlayerProfile(string playfabId);
        void SaveAvatarData();
        void LoadAvatarData();


        //Get veriables
        string GetPlayfabId();

        //function for Push notfication
        void RegisterForPush();
    }
}
