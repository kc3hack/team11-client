using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TatekanController : MonoBehaviour {
    public int NTatekanMax = 5;
    private int n_tatekans = 0;

	public void IncreaseTatekan()
    {
        if(n_tatekans < NTatekanMax) n_tatekans++;
    }

    public void DecreaseTatekan()
    {
        if(n_tatekans > 0) n_tatekans--;
    }

    public bool IsFull()
    {
        return n_tatekans >= NTatekanMax;
    }
}
