using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour {
    public Sprite SpriteOnActive;
    public Sprite SpriteOnInactive;

    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeToActive()
    {
        sr.sprite = SpriteOnActive;
    }
    public void ChangeToInactive()
    {
        sr.sprite = SpriteOnInactive;
    }
}
