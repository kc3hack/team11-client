using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerClickHandler {
    public int NPersonnel = 1;
    public Sprite SpriteNormal;
    public Sprite SpriteSelected;
    public Sprite SpriteDisabled;

    private int index = 0;

    private SpriteRenderer sr;

    private InputController2P ic2;
    private bool is_selected = false;
    private bool is_on_cooldown = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        ic2.SelectNPersonnel(NPersonnel, this);
    }

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();

        var im2 = GameObject.Find("2PInputManager");
        ic2 = im2.GetComponent<InputController2P>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        if(is_on_cooldown)
        {
            sr.sprite = SpriteDisabled;
        } else if(is_selected)
        {
            sr.sprite = SpriteSelected;
        } else
        {
            sr.sprite = SpriteNormal;
        }
    }

    public void ChangeToSelected()
    {
        if (!is_on_cooldown)
        {
            is_selected = true;
        }

        //UpdateAppearance();
    }

    public void ChangeToNotSelected()
    {
        is_selected = false;

        //UpdateAppearance();
    }

    public void ChangeToOnCooldown()
    {
        is_on_cooldown = true;
        is_selected = false;

        //UpdateAppearance();
    }

    public void ChangeToCooleddown()
    {
        is_on_cooldown = false;
        is_selected = false;

        //UpdateAppearance();
    }

    public void SetIndex(int i)
    {
        index = i;
    }
    public int GetIndex()
    {
        return index;
    }
}
