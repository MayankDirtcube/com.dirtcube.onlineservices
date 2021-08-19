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
    
    public class PlayfabLoginSystem : ILoginSystem
    {
        string userPlayfabId;
        public string pushToken;

        public void CreateAccountByEmail(string email, string password)
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

        public void RegisterForPush()
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

        public void GetPlayerProfile(string playfabId)
        {
            var request = new GetPlayerProfileRequest
            {
                PlayFabId = playfabId
            };
            PlayFabClientAPI.GetPlayerProfile(request, OnGetPlayerProfile, Common.OnError);
        }
        void OnGetPlayerProfile(GetPlayerProfileResult result)
        {
            
            
        }

        public void GetUserInventory()
        {
            var request = new GetUserInventoryRequest { };
            PlayFabClientAPI.GetUserInventory(request, OnGetUserInventory, Common.OnError);

        }
        void OnGetUserInventory(GetUserInventoryResult result)
        {
            //Currency.text = "VC : " + result.VirtualCurrency["PC"].ToString();
        }

        public void LoginByEmail(string email, string password)
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
            userPlayfabId = result.PlayFabId;
            //SceneManager.LoadScene(1);
        }

        public void Logout()
        {
            PlayFabClientAPI.ForgetAllCredentials();
            Debug.Log("logout successfully");
        }

        public void SaveAvatarData()
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

        public void LoadAvatarData()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnLoadAvatarData, Common.OnError);
        }
        void OnLoadAvatarData(GetUserDataResult result)
        {

        }

        public void LogInByFacebook()
        {
            throw new System.NotImplementedException();
        }

        public void LogInByGoogle()
        {
            throw new System.NotImplementedException();
        }

        public string GetPlayfabId()
        {
            return userPlayfabId;
        }
    }
}

