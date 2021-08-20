using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace OnlineService
{
    public class LoadSceneFromServer : Singleton<LoadSceneFromServer>
    {
        [SerializeField]
        string AssetBundleURL;
        [SerializeField]
        string SceneName;

        AssetBundle assetbundle;

        
        void Start()
        {
            PlayerPrefs.DeleteAll();
            Scene scene = SceneManager.GetActiveScene();
            
        }


        //Function To load scene/assetbundles from server
        public void LoadAssetBundleFromServer()
        {
            StartCoroutine(SceneLoadFromServer());
        }

        IEnumerator SceneLoadFromServer()
        {
            UnityWebRequest webRequest;
#if UNITY_ANDROID
            using ( webRequest = UnityWebRequestAssetBundle.GetAssetBundle(AssetBundleURL))
#endif
            {
                yield return webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    yield return null;
                }
                if (!string.IsNullOrEmpty(webRequest.error))
                {
                    Debug.Log(webRequest.error);
                    yield return null;
                }
                else
                {
                    Debug.Log("Download success");

                }
                assetbundle = DownloadHandlerAssetBundle.GetContent(webRequest);
            }
 
            string[] sceneList = assetbundle.GetAllScenePaths();

            foreach(string _sceneName in sceneList)
            {
                if (Path.GetFileNameWithoutExtension(_sceneName) == SceneName)
                {
                    LoadScene(_sceneName);
                }
            }
            
        }

        public void LoadScene(string name)
        {
           
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            Debug.Log("The scene name is--" + name);
            assetbundle.UnloadAsync(false);
        }
    }

   

    public enum AssetBundleDownloadType
    {
        local,
        server
    }
}

