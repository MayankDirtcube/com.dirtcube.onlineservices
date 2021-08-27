using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab;

namespace OnlineService
{
    public class PlayfabLoginSystem : ALoginSystem
    {
        string userPlayfabId;
        public PlayerProfileModel PlayerProfile;

        public override void CreateAccountByEmail(string email, string password)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = email,
                Password = password,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnCreateAccountByEmail, Common.OnErrorCreateAccountByEmail);
        }

        void OnCreateAccountByEmail(RegisterPlayFabUserResult result)
        {
            Debug.Log("Account successfuly Created ");
        }

        public override void RegisterForPush()
        {
            pushToken = PlayerPrefs.GetString("Pushtoken");
#if UNITY_ANDROID
            var request = new AndroidDevicePushNotificationRegistrationRequest
            {
                DeviceToken = pushToken,
                SendPushNotificationConfirmation = true,
                ConfirmationMessage = "Push notifications registered successfully"
            };
            PlayFabClientAPI.AndroidDevicePushNotificationRegistration(request, OnPfAndroidReg, Common.OnError);
#endif
        }

        private void OnPfAndroidReg(AndroidDevicePushNotificationRegistrationResult result)
        {
            Debug.Log("PlayFab: Push Registration Successful");
        }

        public override void GetPlayerProfile()
        {
            var request = new GetPlayerProfileRequest
            {
                PlayFabId = PlayerId
            };
            PlayFabClientAPI.GetPlayerProfile(request, OnGetPlayerProfile, Common.OnError);
        }
        void OnGetPlayerProfile(GetPlayerProfileResult result)
        {

            PlayerProfile = result.PlayerProfile;
        }

        public override void GetUserInventory()
        {
            var request = new GetUserInventoryRequest { };
            PlayFabClientAPI.GetUserInventory(request, OnGetUserInventory, Common.OnError);

        }
        void OnGetUserInventory(GetUserInventoryResult result)
        {
            //Currency.text = "VC : " + result.VirtualCurrency["PC"].ToString();
        }

        public override void LoginByEmail(string email, string password)
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password,
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginByEmail, Common.OnErrorLoginByEmail);
        }
        void OnLoginByEmail(PlayFab.ClientModels.LoginResult result)
        {
            Debug.Log("Login Sucessfully");
            RegisterForPush();
            PlayerId = result.PlayFabId;
            GetPlayerProfile();
        }

        public override void Logout()
        {
            PlayFabClientAPI.ForgetAllCredentials();
            Debug.Log("logout successfully");
        }

        public override void SaveAvatarData()
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    {
                        "Avatar",""//JsonConvert.SerializeObject(avatarInfo)
                    }
                }
            };
            PlayFabClientAPI.UpdateUserData(request, OnSaveAvatarData, Common.OnError);
        }
        void OnSaveAvatarData(UpdateUserDataResult result)
        {
            Debug.Log("avatar save online");
        }

        public override void LoadAvatarData()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnLoadAvatarData, Common.OnError);
        }
        void OnLoadAvatarData(GetUserDataResult result)
        {

        }

        public override void LogInByFacebook()
        {
            throw new System.NotImplementedException();
        }

        public override void LogInByGoogle()
        {
            throw new System.NotImplementedException();
        }

        public string GetPlayfabId()
        {
            return userPlayfabId;
        }
    }
}

