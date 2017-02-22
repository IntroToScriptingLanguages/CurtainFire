using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Representation of a card from the incident deck.
*/
public class IncidentCard : Card {
    

    // Use this for initialization
    protected new void Start () {
        base.Start();
        rear_sprite = Resources.Load<Sprite>("CardBacks/IncidentsB") as Sprite;
    }

    // Update is called once per frame
    protected new void Update () {
        base.Update();
    }
}
