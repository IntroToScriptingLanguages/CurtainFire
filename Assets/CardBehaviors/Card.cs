using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Direction a card is facing
public enum Facing { Up, Left, Down, Right };

//Zones of the game board
public enum Zone { None, BoardTop, BoardBottom, Hand, Stack, MainDeck, DiscardDeck, Incident, IncidentDeck, IncidentDiscardDeck, IncidentCollectDeck }

/*
    Representation of a card, attached to the actual card object.
*/
public abstract class Card : MonoBehaviour {

    public string cardName; //Card name

    static int cur_id = 0;
    public int cardID;
    int testInt;
    Facing facing; //Changes card direction
    public bool flipped; //If true, back is facing top
    public bool enableInput;
    public Zone loc; //NOTE: Only used with Card.moveZone
    public Sprite sprite; //Stores the sprite of the card
    public Sprite rear_sprite; //Stores the back sprite of the card

    public Transform card_t;
    
    //Player controller, 0 is no one
    public int controller;

    //For card movement
    float movementSpeed;
    Vector3 movementTarget;

    // Use this for initialization
    protected void Start () {
        testInt = 0;
        facing = Facing.Up;
        controller = 0;
        flipped = false;
        enableInput = true;

        cardName = "";
        cardID = cur_id;
        cur_id++;

        card_t = this.GetComponent<Transform>();

        loc = Zone.None;
    }


    IEnumerator ZoomIn(Player player)
    {

        if (player != null && Input.GetMouseButtonDown(1) && !player.zoomedIn)
        {
            if (player.zooming)
            {
                yield break;
            }

            player.zooming = true;

            yield return new WaitForSeconds(0.1f);

            player.ui_zoomIn(this);

            Debug.Log("zoomed in");

            player.zooming = false;
        }
    }

    IEnumerator ZoomOut(Player player)
    {
        if (player != null && player.zoomedIn && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Return))
            )
        {
            if (player.zooming)
            {
                yield break;
            }

            player.zooming = true;

            yield return new WaitForSeconds(0.1f);

            // Code to execute after the delay
            player.ui_zoomOut();
            Debug.Log("zoomed out");

            player.zooming = false;
        }
    }

    //Changes the facing of the card
    public void rotateCard(Facing target)
    {
        if (facing.Equals(target))
        {
            return;
        }

        Transform card_t = this.gameObject.GetComponent<Transform>();

        if (card_t != null)
        {
            int cur_int = (int)facing;
            int tar_int = (int)target;

            int diff = (tar_int - cur_int) % 4;

            facing = target;

            card_t.Rotate(new Vector3(0, 0, 1), 90 * diff);
        }
    }

   

    //Called when you left-click a card when not Zoomed
    protected void Select(Player selector)
    {
        //For testing LZ LZ LZ
        if (!(this is CharaCard))
        {
            if (this is IncidentCard)
            {
                this.moveZone(Zone.Hand, Utilities.random.Next(1, 9));
                
            }
            else
            {
                this.moveZone(Zone.Hand, Utilities.random.Next(1, 9));
            }
        }
        //End testing
    }

    //The ultimate move function, moves anywhere you want
    //Use this over any other move function, as it's the only one that updates MoveTarget
    public void moveZone(Zone target, int new_controller = 1)
    {
        Zone card_loc = loc;

        //Removes card from original location
        switch (card_loc)
        {
            case Zone.Hand:
                Player oldPlayer = Player.list[this.controller - 1];
                oldPlayer.removeHand(this);
                break;
            case Zone.BoardBottom:
                PlayerField.removeFromBoard(this, controller);
                break;
            case Zone.BoardTop:
                PlayerField.removeFromBoard(this, controller);
                break;
            case Zone.Stack:
                Deck stack = GameObject.Find("Player"+controller+"Stack").GetComponent<Deck>();
                stack.remove(this);
                break;
            case Zone.Incident:
                break;
            case Zone.MainDeck:
                Deck mdeck = GameObject.Find("MainDeck").GetComponent<Deck>();
                mdeck.remove(this);
                break;
            case Zone.DiscardDeck:
                Deck ddeck = GameObject.Find("DiscardDeck").GetComponent<Deck>();
                ddeck.remove(this);
                break;
            case Zone.IncidentDeck:
                Deck ideck = GameObject.Find("IncidentDeck").GetComponent<Deck>();
                ideck.remove(this);
                break;
            case Zone.IncidentDiscardDeck:
                Deck icdeck = GameObject.Find("IncidentCollectDeck").GetComponent<Deck>();
                icdeck.remove(this);
                break;
            case Zone.IncidentCollectDeck:
                Deck iddeck = GameObject.Find("IncidentDiscardDeck").GetComponent<Deck>();
                iddeck.remove(this);
                break;
            default:
                break;
        }

        rotateCard(Facing.Up);

        //Moves card to new location, also handles movement animation
        switch (target)
        {
            case Zone.Hand:
                Player new_player = Player.list[new_controller - 1];
                new_player.addHand(this);
                controller = new_controller;
                break;
            case Zone.BoardBottom:
                PlayerField.move(this, new_controller, false);
                controller = new_controller;
                break;
            case Zone.BoardTop:
                PlayerField.move(this, new_controller, true);
                controller = new_controller;
                break;
            case Zone.Stack:
                Deck stack = GameObject.Find("Player"+new_controller+"Stack").GetComponent<Deck>();
                stack.add(this);
                controller = new_controller;
                break;
            case Zone.Incident:
                break;
            case Zone.MainDeck:
                Deck mdeck = GameObject.Find("MainDeck").GetComponent<Deck>();
                mdeck.add(this);
                controller = 0;
                break;
            case Zone.DiscardDeck:
                Deck ddeck = GameObject.Find("DiscardDeck").GetComponent<Deck>();
                ddeck.add(this);
                controller = 0;
                break;
            case Zone.IncidentDeck:
                Deck ideck = GameObject.Find("IncidentDeck").GetComponent<Deck>();
                ideck.add(this);
                controller = 0;
                break;
            case Zone.IncidentDiscardDeck:
                Deck icdeck = GameObject.Find("IncidentCollectDeck").GetComponent<Deck>();
                icdeck.add(this);
                controller = 0;
                break;
            case Zone.IncidentCollectDeck:
                Deck iddeck = GameObject.Find("IncidentDiscardDeck").GetComponent<Deck>();
                iddeck.add(this);
                controller = 0;
                break;
            default:
                controller = 0;
                break;
        }

        loc = target;
    }

    //Orders a card to move to a designated location
    //
    public void move(Vector3 target, float speed)
    {
        this.movementTarget = target;
        this.movementSpeed = speed * Time.deltaTime;
    }

    //Sets a card to only be visible to player x.  A number not within 1 to 8 inclusive makes it visible to everyone.
    public void setVisible(int x)
    {
        if (x >= 1 && x <= 8)
            this.gameObject.layer = 7 + x;
        else
            this.gameObject.layer = 0;

        if (flipped)
        {
            flip();
        }
    }

    // Update is called once per frame
    protected void Update() {

        for (int i = 0; i < Player.num_players; i++)
        {
            if (Player.list[i].zoomedIn)
            StartCoroutine(ZoomOut(Player.list[i]));
        }


        //Set movement
        if (movementSpeed > 0)
        {
            if (movementTarget != null)
            {
                enableInput = false;
                Transform transform = this.gameObject.GetComponent<Transform>();

                if (transform.position.Equals(movementTarget))
                {
                    movementSpeed = 0;
                    enableInput = true;
                }
                else
                {
                    //Debug.Log("Test int: "+(testInt+1)+", current: ("+transform.position.x+", "+transform.position.z+"), target: ("
                    //    + movementTarget.x + ", " + movementTarget.z + ")");
                    transform.position = Vector3.MoveTowards(transform.position, movementTarget, movementSpeed);
                }
                
            }
            else
            {
                movementSpeed = 0;
                enableInput = true;
            }
        }
        


    }

    //Handles tint effect when you hover over a card
    void OnMouseEnter()
    {
        if (enableInput)
        {
        }
    }

    void OnMouseExit()
    {
        if (enableInput)
        {
        }
    }

    //Handles zooming and selection
    void OnMouseOver()
    {
        
        if (enableInput)
        { 
            var globalBehavior = GlobalBehavior.GetInstance();
            //Zoom in
            for (int i = 0; i < Player.num_players; i++)
            {
                Player player = Player.list[i];

                if (player.isHuman())
                {

                    if (globalBehavior != null && Input.GetMouseButtonDown(1) && !globalBehavior.zoomedin)
                    {
                        StartCoroutine(ZoomIn(player));
                    }
                    //Select
                    else if (globalBehavior != null && Input.GetMouseButtonDown(0) && !globalBehavior.zoomedin)
                    {
                        Select(player);
                    }
                }
            }
        }
    }

    //Has the card set visible to only player x after a short delay
    public IEnumerator setVisibleAfterDelay(int x, float delay)
    {
        yield return new WaitForSeconds(delay);

        setVisible(x);
    }

    //Changes whether the card is flipped
    public void flip()
    {
        if (!flipped)
        {
            //Flip back
            CardCreator creator = GameObject.Find("Manager").GetComponent<CardCreator>();
            CardCreator.setArt(this.gameObject, this.rear_sprite);
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

    public override bool Equals(object other)
    {
        if (other is Card)
        {
            Card other_card = other as Card;
            if (other_card.cardID == this.cardID)
            {
                return true;
            }
        }
        return false;
    }
}
