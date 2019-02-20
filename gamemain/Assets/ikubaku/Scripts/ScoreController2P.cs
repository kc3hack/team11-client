using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController2P : MonoBehaviour {
    public UnityEngine.UI.Text ScoreText2P;

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
        ScoreText2P.text = "Score2P: " + score.ToString();
    }
}
