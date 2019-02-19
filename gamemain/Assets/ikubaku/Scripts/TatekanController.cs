using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TatekanController : MonoBehaviour, IPointerClickHandler {
    public int InitialScore = 250;
    public int ScorePerFrame = 1;

    private ScoreController1P sc1;
    private bool is_removed = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        is_removed = true;
    }

    void Start()
    {
        var sm = GameObject.Find("ScoreManager1P");
        sc1 = sm.GetComponent<ScoreController1P>();
        sc1.AddScore(InitialScore);
    }

    void Update()
    {
        if (is_removed)
        {
            Destroy(gameObject);
        }
        else
        {
            sc1.AddScore(ScorePerFrame);
        }
    }
}
