using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour {
    public int ReadyDuration = 3;
    public int GameDuration = 180;

    public UnityEngine.UI.Text CountText;
    public UnityEngine.UI.Text TimeText;

    private float cnt_game;
    private bool is_started = false;

    string GetTimeString()
    {
        if(cnt_game <= 0f)
        {
            return "TIME: 0:00:00";
        }
        var minute = Mathf.FloorToInt(cnt_game / 60f);
        var second = Mathf.FloorToInt(cnt_game % 60f);
        var subsec = Mathf.FloorToInt((cnt_game * 100) % 100);
        var s = string.Format("TIME: {0,1:D1}:{1,2:D2}:{2,2:D2}", minute, second, subsec);
        return s;
    }

	// Use this for initialization
	void Start () {
        cnt_game = GameDuration;
        TimeText.text = GetTimeString();
        StartCoroutine(StartCountDown());
        Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (is_started)
        {
            cnt_game -= Time.deltaTime;
        }
        TimeText.text = GetTimeString();
        if(cnt_game <= 0f)
        {
            CountText.text = "Time's Up!";
            StartCoroutine(StartGoToRankings());
            Time.timeScale = 0f;
        }
	}

    IEnumerator StartCountDown()
    {
        for(int c=ReadyDuration; c>0; c--)
        {
            CountText.text = c.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        Time.timeScale = 1f;
        is_started = true;
        CountText.text = "Go!";
        StartCoroutine(StartHideCount());
    }

    IEnumerator StartHideCount()
    {
        yield return new WaitForSeconds(1f);

        CountText.text = "";
    }

    IEnumerator StartGoToRankings()
    {
        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene("Rankings");
    }

    public bool IsStarted()
    {
        return is_started;
    }
}
