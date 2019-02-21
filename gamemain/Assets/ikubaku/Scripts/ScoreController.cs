using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    public int ScorePerExtraTatekan = 120;
    public int ScoreThreshold = 3;

    public UnityEngine.UI.Text Score1PText;
    public UnityEngine.UI.Text Score2PText;

    public TimeController TimeManager;

    private int n_tatekans = 0;
    private float score_1p = 0f;
    private float score_2p = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // このフレームのスコアを計算
        if (TimeManager.IsStarted())
        {
            var d = n_tatekans - ScoreThreshold;
            if (d < 0)
            {
                score_2p += ScorePerExtraTatekan * -d * Time.deltaTime;
            }
            else if (d > 0)
            {
                score_1p += ScorePerExtraTatekan * d * Time.deltaTime;
            }
        }

        // 表示
        Score1PText.text = "Score1P: " + Mathf.FloorToInt(score_1p).ToString();
        Score2PText.text = "Score2P: " + Mathf.FloorToInt(score_2p).ToString();
	}

    public void IncreaseTatekan()
    {
        n_tatekans++;
    }
    public void DecreaseTatekan()
    {
        if(n_tatekans > 0)
        {
            n_tatekans--;
        }
    }

    public int GetScore1P()
    {
        return Mathf.FloorToInt(score_1p);
    }
    public int GetScore2P()
    {
        return Mathf.FloorToInt(score_2p);
    }
}
