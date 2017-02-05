using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Direction a card is facing
public enum Facing { Up, Right, Down, Left };

//Zones of the game board
public enum Zone { None, BoardTop, BoardBottom, Hand, MainDeck, DiscardDeck, IncidentDeck, IncidentDiscardDeck, IncidentCollectDeck }

public abstract class Card : MonoBehaviour {

    public string cardName; //Card name

    static int cur_id = 0;
    public int cardID;
    int testInt;
    Facing facing; //Changes card direction
    protected bool flipped; //If true, back is facing top
    public bool enableInput;
    Zone loc; //NOTE: Only used with Card.moveZone
    public int spriteIndex; //Stores index of sprite in the sprite array

    System.Random random = new System.Random();

    //Player owner, 0 is no one
    public int owner;

    //For card movement
    float movementSpeed;
    Vector3 movementTarget;

    // Use this for initialization
    protected void Start () {
        testInt = 0;
        facing = Facing.Up;
        owner = 0;
        flipped = false;
        enableInput = true;

        cardName = "";
        cardID = cur_id;
        cur_id++;

        loc = Zone.None;
        spriteIndex = 0;
    }

    IEnumerator ZoomIn()
    {
        var globalBehavior = GlobalBehavior.GetInstance();
        if (globalBehavior != null && Input.GetMouseButtonDown(1) && !globalBehavior.zoomedin)
        {
            if (globalBehavior.zooming)
            {
                yield break;
            }

            globalBehavior.zooming = true;

            yield return new WaitForSeconds(1.0f);

            // Code to execute after the delay
            globalBehavior.zoomedin = true;
            Debug.Log("zoomed in");

            globalBehavior.zooming = false;
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

            card_t.Rotate(new Vector3(0, 1, 0), 90 * diff);
        }
    }

    IEnumerator ZoomOut()
    {
        var globalBehavior = GlobalBehavior.GetInstance();
        
        if (globalBehavior != null && globalBehavior.zoomedin && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Return))
            )
        {
            if (globalBehavior.zooming)
            {
                yield break;
            }

            globalBehavior.zooming = true;

            yield return new WaitForSeconds(1.0f);

            // Code to execute after the delay
            globalBehavior.zoomedin = false;
            Debug.Log("zoomed out");

            globalBehavior.zooming = false;
        }
    }

    //Called when you left-click a card when not Zoomed
    void Select()
    {
        //For testing LZ LZ LZ
        this.moveZone(Zone.BoardBottom, random.Next(1, 8));
        //End testing
    }

    //The ultimate move function, moves anywhere you want
    //Use this over any other move function, as it's the only one that updates MoveTarget
    public void moveZone(Zone target, int new_owner = 1)
    {
        Zone card_loc = loc;

        //Removes card from original location
        switch (card_loc)
        {
            case Zone.Hand:
                //TODO
                break;
            case Zone.BoardBottom:
                PlayerField.removeFromBoard(this, owner);
                break;
            case Zone.BoardTop:
                PlayerField.removeFromBoard(this, owner);
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
                //TODO
                owner = new_owner;
                break;
            case Zone.BoardBottom:
                PlayerField.move(this, new_owner, false);
                owner = new_owner;
                break;
            case Zone.BoardTop:
                PlayerField.move(this, new_owner, true);
                owner = new_owner;
                break;
            case Zone.MainDeck:
                Deck mdeck = GameObject.Find("MainDeck").GetComponent<Deck>();
                mdeck.add(this);
                owner = 0;
                break;
            case Zone.DiscardDeck:
                Deck ddeck = GameObject.Find("DiscardDeck").GetComponent<Deck>();
                ddeck.add(this);
                owner = 0;
                break;
            case Zone.IncidentDeck:
                Deck ideck = GameObject.Find("IncidentDeck").GetComponent<Deck>();
                ideck.add(this);
                owner = 0;
                break;
            case Zone.IncidentDiscardDeck:
                Deck icdeck = GameObject.Find("IncidentCollectDeck").GetComponent<Deck>();
                icdeck.add(this);
                owner = 0;
                break;
            case Zone.IncidentCollectDeck:
                Deck iddeck = GameObject.Find("IncidentDiscardDeck").GetComponent<Deck>();
                iddeck.add(this);
                owner = 0;
                break;
            default:
                owner = 0;
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

    // Update is called once per frame
    protected void Update() {
        StartCoroutine(ZoomOut());

        if (movementSpeed > 0)
        {
            if (movementTarget != null)
            {
                Transform transform = this.gameObject.GetComponent<Transform>();

                if (transform.position.Equals(movementTarget))
                {
                    movementSpeed = 0;
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
            }
        }
    }

    //Handles tint effect when you hover over a card
    void OnMouseEnter()
    {
        if (enableInput)
        {
            var renderer = this.GetComponent<MeshRenderer>();

            renderer.material.SetColor("_TintColor", Color.yellow);
        }
    }

    void OnMouseExit()
    {
        if (enableInput)
        {
            var renderer = this.GetComponent<MeshRenderer>();

            renderer.material.SetColor("_TintColor", Color.white);
        }
    }

    //Handles zooming
    void OnMouseOver()
    {
        
        if (enableInput)
        { 
            var globalBehavior = GlobalBehavior.GetInstance();
            //Zoom in
            if (globalBehavior != null && Input.GetMouseButtonDown(1) && !globalBehavior.zoomedin)
            {
                StartCoroutine(ZoomIn());
            }
            //Select
            else if (globalBehavior != null && Input.GetMouseButtonDown(0) && !globalBehavior.zoomedin)
            {
                Select();
            }
        }
    }

    //Changes the card's direction
    public abstract void flip();
}
