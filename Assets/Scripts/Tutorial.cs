using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public GameObject screen;
    bool show = true;

    // Use this for initialization
	void Update () {
        if (show)
        {
            screen.SetActive(true);
        } else
        {
            screen.SetActive(false);
        }
	}

    public void Dismiss()
    {
        show = false;
    }
}
