using System;
using System.Collections;
using UnityEngine;

namespace Assets.ikubaku.Scripts
{
    public class TatekanDownloader : MonoBehaviour
    {
        public IEnumerator Download(Uri imageUri)
        {
            var www = new WWW(imageUri.AbsoluteUri);

            while (!www.isDone)
            {
//                Debug.Log(Mathf.CeilToInt(www.progress * 100));
                yield return null;
            }

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
            }
            else
            {
                TatekanStore.TatekanImages.Add(www.textureNonReadable);
            }
        }
    }
}