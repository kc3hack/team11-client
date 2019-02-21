using System.Collections;
using Assets.ikubaku.Scripts.TatekanModels;
using Assets.ikubaku.Scripts.Title;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ikubaku.Scripts
{
    public class LoadSceneManager : MonoBehaviour
    {
        public string ServerURL;
        public Slider slider;

        private IEnumerator Start()
        {
            slider = GameObject.Find("Slider").GetComponent<Slider>();
            yield return StartCoroutine(AcquireTatekanImages());
            SceneManager.LoadScene("GameMain");
        }

        IEnumerator AcquireTatekanImages()
        {
            var unityWebRequest = new UnityWebRequest(ServerURL);
            unityWebRequest.method = UnityWebRequest.kHttpVerbGET;
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                Debug.LogError(unityWebRequest.error);
            }
            else
            {
                var tatekanResponse = new TatekanResponse();
                JsonConvert.PopulateObject(unityWebRequest.downloadHandler.text, tatekanResponse);

                foreach (var tatekanEntity in tatekanResponse.data)
                {
                    var tatekanDownloader = GetComponent<TatekanDownloader>();
                    yield return StartCoroutine(tatekanDownloader.Download(tatekanEntity.uri));
                    slider.value += (float) 1 / tatekanResponse.data.Length;
                }
            }
        }
    }
}