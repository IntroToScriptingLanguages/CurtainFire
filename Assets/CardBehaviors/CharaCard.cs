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
        rear_sprite = Resources.Load<Sprite>("CardBacks/CharactersB") as Sprite;
    }

    // Update is called once per frame
    protected new void Update () {
        base.Update();
    }

    
}
