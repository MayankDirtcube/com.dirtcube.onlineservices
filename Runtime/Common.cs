using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;

namespace OnlineService
{
    public class Common
    {
        public static void OnError(PlayFabError error)
        {
            Debug.Log(error.ErrorMessage);
        }

        public static void OnErrorCreateAccountByEmail(PlayFabError error)
        {
            Debug.Log("OnErrorCreateAccountByEmail " + error.ErrorMessage);
        }

       public static void OnErrorLoginByEmail(PlayFabError error)
        {
            Debug.Log("OnErrorLoginByEmail " + error.ErrorMessage);
        }

        public static void OnErrorLogInByFacebook(PlayFabError error)
        {
            Debug.Log("OnErrorLogInByFacebook " + error.ErrorMessage);
        }

        public static void OnErrorLogInByGoogle(PlayFabError error)
        {
            Debug.Log("OnErrorLogInByGoogle " + error.ErrorMessage);
        }

        public static void OnErrorLogout(PlayFabError error)
        {
            Debug.Log("OnErrorLogout " + error.ErrorMessage);
        }
    }
}
