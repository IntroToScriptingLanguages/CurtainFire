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
        rear_sprite = Resources.Load<Sprite>("CardBacks/RolesB") as Sprite;
    }

    // Update is called once per frame
    protected new void Update () {
		base.Update();
    }
}
