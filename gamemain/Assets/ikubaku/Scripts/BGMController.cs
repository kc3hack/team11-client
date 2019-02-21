using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {
    private AudioSource aud;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartBGM()
    {
        aud.Play();
    }
    void StopBGM()
    {
        aud.Stop();
    }
}
