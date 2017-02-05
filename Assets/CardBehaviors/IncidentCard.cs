using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncidentCard : Card {
    

    // Use this for initialization
    protected new void Start () {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update () {
        base.Update();
    }

    public override void flip()
    {
        if (!flipped)
        {
            //Flip back
            CardCreator creator = GameObject.Find("Manager").GetComponent<CardCreator>();
            CardCreator.setArt(this.gameObject, "CardBacks/IncidentsB");
            flipped = true;
        }
        else
        {
            //Flip front
            flipped = false;
            Debug.Log("flip back");
        }
    }
}
