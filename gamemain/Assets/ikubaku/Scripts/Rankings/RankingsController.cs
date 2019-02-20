using Assets.ikubaku.Scripts.Rankings;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class RankingsController : MonoBehaviour {
    public string ServerURL;
    public UnityEngine.UI.Text[] StudentNameTexts;
    public UnityEngine.UI.Text[] StudentScoreTexts;
    public UnityEngine.UI.Text[] AdminNameTexts;
    public UnityEngine.UI.Text[] AdminScoreTexts;

    private bool is_upload_ok = false;
    private RankingsResponse res_s;
    private RankingsResponse res_a;
	// Use this for initialization
	void Start () {
        res_s = new RankingsResponse();
        res_a = new RankingsResponse();
        StartCoroutine(UploadScoreForStudent());
        StartCoroutine(UploadScoreForAdmin());
        StartCoroutine(AcquireScoreForStudent());
        StartCoroutine(AcquireScoreForAdmin());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator UploadScoreForStudent()
    {
        var q = new UnityWebRequest(ServerURL + "?user_type=student");
        q.method = UnityWebRequest.kHttpVerbPOST;
        // stub
        var stub_entry = new ScoreEntry() { username = "ikubaku", value = 1200, user_type = "student" };
        var qs = JsonConvert.SerializeObject(stub_entry);
        var uh = new UploadHandlerRaw(Encoding.ASCII.GetBytes(qs));
        uh.contentType = "application/json";
        q.uploadHandler = uh;

        yield return q.SendWebRequest();

        if(q.isNetworkError || q.isHttpError)
        {
            Debug.LogError(q.error);
        } else
        {
            Debug.Log(q.responseCode);
            is_upload_ok = true;
        }
    }
    IEnumerator UploadScoreForAdmin()
    {
        var q = new UnityWebRequest(ServerURL + "?user_type=admin");
        q.method = UnityWebRequest.kHttpVerbPOST;
        // stub
        var stub_entry = new ScoreEntry() { username = "ikubaku", value = 1200, user_type = "admin" };
        var qs = JsonConvert.SerializeObject(stub_entry);
        var uh = new UploadHandlerRaw(Encoding.ASCII.GetBytes(qs));
        uh.contentType = "application/json";
        q.uploadHandler = uh;

        yield return q.SendWebRequest();

        if (q.isNetworkError || q.isHttpError)
        {
            Debug.LogError(q.error);
        }
        else
        {
            Debug.Log(q.responseCode);
            is_upload_ok = true;
        }
    }

    IEnumerator AcquireScoreForStudent()
    {
        var q = new UnityWebRequest(ServerURL);
        q.method = UnityWebRequest.kHttpVerbGET;
        q.downloadHandler = new DownloadHandlerBuffer();

        yield return q.SendWebRequest();

        if(q.isNetworkError || q.isHttpError)
        {
            Debug.LogError(q.error);
        } else
        {
            Debug.Log(q.downloadHandler.text);
            JsonConvert.PopulateObject(q.downloadHandler.text, res_s);
            for(int i=0; i<res_s.data.Length; i++)
            {
                if (i >= StudentScoreTexts.Length) break;

                var se = res_s.data[i];
                StudentNameTexts[i].text = (i + 1).ToString() + ": " + se.username;
                StudentScoreTexts[i].text = se.value.ToString();
            }
        }
    }
    IEnumerator AcquireScoreForAdmin()
    {
        var q = new UnityWebRequest(ServerURL);
        q.method = UnityWebRequest.kHttpVerbGET;
        q.downloadHandler = new DownloadHandlerBuffer();

        yield return q.SendWebRequest();

        if (q.isNetworkError || q.isHttpError)
        {
            Debug.LogError(q.error);
        }
        else
        {
            Debug.Log(q.downloadHandler.text);
            JsonConvert.PopulateObject(q.downloadHandler.text, res_a);
            for (int i = 0; i < res_a.data.Length; i++)
            {
                if (i >= AdminScoreTexts.Length) break;

                var se = res_a.data[i];
                AdminNameTexts[i].text = (i + 1).ToString() + ": " + se.username;
                AdminScoreTexts[i].text = se.value.ToString();
            }
        }
    }
}
