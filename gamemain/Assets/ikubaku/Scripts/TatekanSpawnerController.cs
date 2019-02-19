using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TatekanSpawnerController : MonoBehaviour {
    public GameObject TatekanBig;
    public GameObject[] SpawnTargets;
    public TatekanController TatekanMController;

    private int spawn_idx = 0;
    private bool is_fire1_pressed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!is_fire1_pressed && Input.GetAxis("Fire1") > 0f)
        {
            is_fire1_pressed = true;
            if (!TatekanMController.IsFull())
            {
                GameObject new_tatekan = Instantiate(TatekanBig);
                new_tatekan.transform.position = SpawnTargets[spawn_idx].transform.position;
                spawn_idx++;
                if (spawn_idx >= SpawnTargets.Length)
                {
                    spawn_idx = 0;
                }
                TatekanMController.IncreaseTatekan();
            }
        }
        if(Input.GetAxis("Fire1") <= 0f)
        {
            is_fire1_pressed = false;
        }
	}
}
