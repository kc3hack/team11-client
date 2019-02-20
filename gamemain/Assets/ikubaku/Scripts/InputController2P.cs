using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController2P : MonoBehaviour {
    public ButtonController[] Buttons;
    public float[] CooldownCounts;

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

    public void CommenceRemoval(TatekanController sender)
    {
        if (n_personnel > 0)
        {
            // ダメージのセット
            sender.SetDamagePerFrame(n_personnel);
            // 選択解除とクールダウン開始
            Buttons[selected_idx].ChangeToOnCooldown();
            cnt_cooldown[selected_idx] = CooldownCounts[selected_idx];
            selected_idx = -1;
            n_personnel = 0;
        }
    }
}
