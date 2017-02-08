using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Representation of a player in the game.
    Also handles the human player's UI and canvas
*/
public class Player {

    public static List<Player> list = new List<Player>();
    public static int num_players = 8;

    public int player_num;
    public bool zoomedIn;
    public bool zooming;
    bool human; //If player is human or AI
    public GameObject playerCamera;
    public GameObject canvas;
    public List<Card> hand;
    public UIElements uiElems;

    public int life;
    public int handSize;

    //Character information
    public string chara_name;
    public Sprite chara_sprite;
    
    //Initializes the player
    public Player(int pn, bool hum, int starting_life = 4)
    {
        player_num = pn;
        human = hum;
        zoomedIn = false;
        zooming = false;

        if (human)
        {
            playerCamera = GameObject.Find("Player" + player_num + "Camera");
            canvas = GameObject.Find("Player" + player_num + "UI");
            uiElems = new UIElements(this);
        }
        else
        {
            uiElems = null;
        }

        life = 4;
        handSize = 0;
        hand = new List<Card>();
    }

    public bool isHuman()
    {
        return human;
    }

    //Zooms in, by displaying the given card
    public void ui_zoomIn(Card c)
    {
        if (isHuman() && !zoomedIn)
        {
            float zoom_scale = 1.3f;

            Sprite s = c.sprite;

            //Create new image object
            GameObject newImage = new GameObject();
            newImage.name = "ZoomedCard";

            RectTransform newImage_t = newImage.AddComponent<RectTransform>();
            newImage.AddComponent<CanvasRenderer>();
            Image image = newImage.AddComponent<Image>();
            
            //Set size of image
            if (c is CharaCard)
            {
                newImage_t.sizeDelta = new Vector2(350 * zoom_scale, 496 * zoom_scale);
            }
            else
            {
                newImage_t.sizeDelta = new Vector2(248 * zoom_scale, 350 * zoom_scale);
            }

            //Attach image to object
            image.sprite = s;

            if (c is CharaCard)
            {
                image.rectTransform.Rotate(new Vector3(0, 0, 90));
            }

            //Attach new image object to parent
            newImage.transform.SetParent(canvas.transform, false);

            zoomedIn = true;
        }
    }

    //Zooms out
    public void ui_zoomOut()
    {
        if (isHuman() && zoomedIn)
        {
            GameObject zoomedCard = GameObject.Find("ZoomedCard");

            if (zoomedCard != null)
            {
                Object.Destroy(zoomedCard);
            }

            zoomedIn = false;
        }
    }

    //Adds a card to hand
    public void addHand(Card c)
    {
        if (c != null)
        {
            uiElems.addHand(c);
            hand.Add(c);
            handSize++;
        }
    }

    //Removes a card from hand
    public void removeHand(Card c)
    {
        if (handSize != 0 && c != null && hand.Contains(c))
        {
            hand.Remove(c);
            handSize--;
            c.StartCoroutine(c.setVisibleAfterDelay(0, 0.5f));
        }
    }

    //Check if hand is empty
    public bool handEmpty()
    {
        return handSize == 0;
    }
}
