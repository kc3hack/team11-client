using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {
    public GameObject TargetObject;
    public float InitialSpeed = 5f;
    public float Attractiveness = 1f;
    public float Resistance = 2.5f;
    public float RemainDuration = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool is_removed = false;
    private float time_to_live = -1f;
	// Use this for initialization
	void Start () {
        time_to_live = RemainDuration;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        var r = Random.Range(0f, Mathf.PI * 2);
        rb.velocity = new Vector2(Mathf.Cos(r), Mathf.Sin(r)) * InitialSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (!is_removed && TargetObject != null)
        {
            rb.AddForce((TargetObject.transform.position - transform.position) * Vector2.Distance(TargetObject.transform.position, transform.position) * Attractiveness);
            if (Vector2.SqrMagnitude(rb.velocity) > 5f)
            {
                rb.AddForce(-rb.velocity * Resistance);
            }
        }

        if(is_removed)
        {
            time_to_live -= Time.deltaTime;
            sr.color = Color.Lerp(new Color(255, 255, 255, 0), Color.white, time_to_live / RemainDuration);
            if(time_to_live <= 0f)
            {
                Destroy(gameObject);
            }
        }
	}

    public void SetTarget(GameObject t)
    {
        TargetObject = t;
    }

    public void SetToRemoved()
    {
        is_removed = true;
    }
}
