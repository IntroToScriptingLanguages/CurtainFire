using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehavior : MonoBehaviour {
    public bool zoomedin //Check if already zoomed in
    {
        get; set;
    }
    public bool zooming //Check if is in process of zooming in
    {
        get; set;
    }

	// Use this for initialization
	void Start () {
        zoomedin = false;
        zooming = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static GlobalBehavior GetInstance()
    {
        GlobalBehavior[] cameraBehaviors = GameObject.FindObjectsOfType<GlobalBehavior>();
        if (cameraBehaviors.Length > 0)
        {
            return cameraBehaviors[0];
        }
        else
        {
            return null;
        }
    }
}
