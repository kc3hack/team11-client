using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController1P : MonoBehaviour {
    public UnityEngine.UI.Text Score1PText;

    private int score = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int s)
    {
        score += s;
        Score1PText.text = "Score1P: " + score.ToString();
    }
}
