using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Representation of a card from the main deck.
*/
public class MainCard : Card {
    

    // Use this for initialization
    protected new void Start () {
        base.Start();
        rear_sprite = Resources.Load<Sprite>("CardBacks/DeckB") as Sprite;
    }

    // Update is called once per frame
    protected new void Update () {
        base.Update();
    }
    
}
