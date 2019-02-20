using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TatekanController : MonoBehaviour, IPointerClickHandler {
    public int InitialScore = 250;
    public int ScorePerFrame = 1;

    public float health = 120f;

    private ScoreController1P sc1;
    private float damage_per_frame = 0f;
    private bool is_removed = false;

    private InputController2P ic2;

    public void OnPointerClick(PointerEventData eventData)
    {
        ic2.CommenceRemoval(this);
    }

    void Start()
    {
        var sm = GameObject.Find("ScoreManager1P");
        sc1 = sm.GetComponent<ScoreController1P>();
        sc1.AddScore(InitialScore);

        var im2 = GameObject.Find("2PInputManager");
        ic2 = im2.GetComponent<InputController2P>();
    }

    void Update()
    {
        health -= damage_per_frame;
        if(health <= 0f)
        {
            is_removed = true;
        }
        if (is_removed)
        {
            Destroy(gameObject);
        }
        else
        {
            sc1.AddScore(ScorePerFrame);
        }
    }

    public void SetDamagePerFrame(float d)
    {
        damage_per_frame = d;
    }
}
