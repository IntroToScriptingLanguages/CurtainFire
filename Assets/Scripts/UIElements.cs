using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for UI elements such as menu, status bar, hand and current incident
//Attach to camera at run-time, so owner can be specified.
//NOTE: The hand and current incident at the moment
public class UIElements  {

    public Player player;
    public GameObject camera;
    public GameObject canvas;
    public int player_num;

	public UIElements(Player owner)
    {
        player = owner;
        player_num = player.player_num;
        camera = player.playerCamera;
        canvas = player.canvas;
    }

    //Shows that a card in hand has been added in that player's
    public void addHand(Card c)
    {
        Transform camera_t = camera.GetComponent<Transform>();
        Vector3 target_t;

        if (player_num == 1 || player_num == 2)
        {
            target_t = camera_t.position;
        }
        else
        {
            target_t = camera_t.position;
        }

        c.move(target_t, 30);
        c.StartCoroutine(c.setVisibleAfterDelay(player_num, 3.2f));
    }
}
