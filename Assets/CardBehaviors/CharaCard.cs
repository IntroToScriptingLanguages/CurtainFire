using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Representation of a character card.
    Note that character cards are represented as square disks on peoples' UIs, NOT physical cards.
    As UI elements, characters MUST be initialized in LateStart
*/
public class CharaCard : Card {
    

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
            CardCreator.setArt(this.gameObject, "CardBacks/CharactersB");
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
