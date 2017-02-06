using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Representation of a role card.
*/
public class RoleCard : Card {

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
            CardCreator.setArt(this.gameObject, "CardBacks/RolesB");
            flipped = true;
        }
        else
        {
            //Flip front
            CardCreator creator = GameObject.Find("Manager").GetComponent<CardCreator>();
            CardCreator.setArt(this.gameObject, this.sprite);
            flipped = false;
        }
    }
}
