using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TatekanController : MonoBehaviour {
    public int InitialScore = 250;
    public int ScorePerFrame = 1;

    private ScoreController1P sc1;
    void Start()
    {
        var sm = GameObject.Find("ScoreManager1P");
        sc1 = sm.GetComponent<ScoreController1P>();
        sc1.AddScore(InitialScore);
    }

    void Update()
    {
        sc1.AddScore(ScorePerFrame);
    }
}
