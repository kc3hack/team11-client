using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TatekanController : MonoBehaviour, IPointerClickHandler {
    public float health = 120f;

    private float damage_per_frame = 0f;
    private bool is_removed = false;

    private InputController2P ic2;
    private ScoreController sc;

    public void OnPointerClick(PointerEventData eventData)
    {
        ic2.CommenceRemoval(this);
    }

    void Start()
    {
        var im2 = GameObject.Find("2PInputManager");
        ic2 = im2.GetComponent<InputController2P>();

        var sm = GameObject.Find("ScoreManager");
        sc = sm.GetComponent<ScoreController>();
    }

    void Update()
    {
        // 撤去ダメージ
        health -= damage_per_frame;
        if(health <= 0f)
        {
            is_removed = true;
        }

        if (is_removed)
        {
            sc.DecreaseTatekan();
            Destroy(gameObject);
        }
    }

    public void SetDamagePerFrame(float d)
    {
        damage_per_frame = d;
    }
}
