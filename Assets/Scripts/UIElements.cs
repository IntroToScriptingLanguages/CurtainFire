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
        int handSize = player.handSize;

        float cardOffset = handSize * 40.0f;
        float upOffset = handSize * 0.01f;


        //Setup facing
        if (player_num == 1 || player_num == 2)
        {
            c.rotateCard(Facing.Down);
        }
        else if (player_num == 3 || player_num == 4)
        {
            c.rotateCard(Facing.Right);
        }
        else if (player_num == 5 || player_num == 6)
        {
            c.rotateCard(Facing.Up);
        }
        else if (player_num == 7 || player_num == 8)
        {
            c.rotateCard(Facing.Left);
        }

        //Setup proper movement target
        Camera cam = camera.GetComponent<Camera>();
        target_t = cam.ScreenToWorldPoint(new Vector3(300.0f + cardOffset, 110.0f, 15.0f - upOffset));

        //Handles movement and visibility
        c.move(target_t, 30);
        c.StartCoroutine(c.setVisibleAfterDelay(player_num, 0.5f));
    }

    //Resets the appearance of the hand
    public void resetHand()
    {
        List<Card> hand = player.hand;
        int handSize = player.handSize;

        Camera cam = camera.GetComponent<Camera>();

        for (int i=0; i<handSize; i++)
        {
            float cardOffset = i * 40.0f;
            float upOffset = i * 0.01f;

            Card c = hand[i];
            Vector3 target_t = cam.ScreenToWorldPoint(new Vector3(300.0f + cardOffset, 110.0f, 15.0f - upOffset));

            c.move(target_t, 80);
        }
    }
}
