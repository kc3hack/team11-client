using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController2P : MonoBehaviour {
    public ButtonController[] Buttons;
    public float[] CooldownCounts;

    public UnityEngine.UI.Text debug_text;

    private int n_personnel = 0;
    private int selected_idx = -1;
    private float[] cnt_cooldown;
	// Use this for initialization
	void Start () {
        cnt_cooldown = new float[CooldownCounts.Length];
        for(int i=0; i<Buttons.Length; i++)
        {
            Buttons[i].SetIndex(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        debug_text.text = n_personnel.ToString();
		for(int i=0; i<cnt_cooldown.Length; i++)
        {
            if(cnt_cooldown[i] > 0f)
            {
                cnt_cooldown[i] -= Time.deltaTime;
                if(cnt_cooldown[i] <= 0f)
                {
                    Buttons[i].ChangeToCooleddown();
                }
            }
        }
	}

    public void SelectNPersonnel(int n, ButtonController sender)
    {
        var idx = sender.GetIndex();
        if(cnt_cooldown[idx] <= 0f)
        {
            if(selected_idx != -1)
            {
                Buttons[selected_idx].ChangeToNotSelected();
            }
            Buttons[idx].ChangeToSelected();
            selected_idx = idx;

            n_personnel = n;
        }
    }
}
