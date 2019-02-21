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

    public UnityEngine.UI.Text StudentNameTextLocal;
    public UnityEngine.UI.Text StudentScoreTextLocal;
    public UnityEngine.UI.Text AdminNameTextLocal;
    public UnityEngine.UI.Text AdminScoreTextLocal;

    private bool is_local_score_shown = false;

    // 0: not ready, >0: number of successful operations
    private int upload_status = 0;
    private int acquire_status = 0;
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
        if (!is_local_score_shown && upload_status >= 2 && acquire_status >= 2)
        {
            StudentNameTextLocal.text = "YOU: " + StaticVars.LocalStudentName;
            StudentScoreTextLocal.text = StaticVars.LocalStudentScore.ToString();
            AdminNameTextLocal.text = "YOU: " + StaticVars.LocalAdminName;
            AdminScoreTextLocal.text = StaticVars.LocalAdminScore.ToString();
        }
    }

    IEnumerator UploadScoreForStudent()
    {
        var q = new UnityWebRequest(ServerURL + "?user_type=student");
        q.method = UnityWebRequest.kHttpVerbPOST;
        var stub_entry = new ScoreEntry() { username = StaticVars.LocalStudentName, value = StaticVars.LocalStudentScore, user_type = "student" };
        var qs = JsonConvert.SerializeObject(stub_entry);
        var uh = new UploadHandlerRaw(Encoding.ASCII.GetBytes(qs));
        uh.contentType = "application/json";
        q.uploadHandler = uh;

        yield return q.SendWebRequest();

        if(q.isNetworkError || q.isHttpError)
        {
            Debug.LogError(q.error);
            StudentNameTexts[0].text = "通信エラー発生(送信)";
        } else
        {
            Debug.Log(q.responseCode);
            upload_status++;
        }
    }
    IEnumerator UploadScoreForAdmin()
    {
        var q = new UnityWebRequest(ServerURL + "?user_type=admin");
        q.method = UnityWebRequest.kHttpVerbPOST;
        var stub_entry = new ScoreEntry() { username = StaticVars.LocalAdminName, value = StaticVars.LocalAdminScore, user_type = "admin" };
        var qs = JsonConvert.SerializeObject(stub_entry);
        var uh = new UploadHandlerRaw(Encoding.ASCII.GetBytes(qs));
        uh.contentType = "application/json";
        q.uploadHandler = uh;

        yield return q.SendWebRequest();

        if (q.isNetworkError || q.isHttpError)
        {
            Debug.LogError(q.error);
            AdminNameTexts[0].text = "通信エラー発生(送信)";
        }
        else
        {
            Debug.Log(q.responseCode);
            upload_status++;
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
            StudentNameTexts[0].text = "通信エラー発生(受信)";
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
            acquire_status++;
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
            AdminNameTexts[0].text = "通信エラー発生(受信)";
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
            acquire_status++;
        }
    }
}
