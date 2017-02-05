using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    LinkedList<Card> deck;

	// Use this for initialization
	void Start () {
        deck = new LinkedList<Card>();
	}

    public int count()
    {
        return deck.Count;
    }

    public void add(Card c)
    {
        Transform trans = this.gameObject.GetComponent<Transform>();

        if (deck.Count > 0)
        {
            deck.Last.Value.enableInput = false;
        }

        if (trans != null)
        {
            int deck_size = deck.Count;
            
            c.move(new Vector3(trans.position.x, trans.position.y + 0.03f + (0.03f * deck_size), trans.position.z), 25);
            deck.AddLast(c);
        }

    }

    public Card peek()
    {
        return deck.Last.Value;
    }

    //Removes the top value
    public Card remove()
    {
        Card c = deck.Last.Value;
        deck.RemoveLast();

        if (deck.Count > 0)
        {
            deck.Last.Value.enabled = true;
        }

        return c;
    }

    //Removes a card if it's the same as the specified one
    public Card remove(Card card)
    {
        int index = 0; //Index of the card removed
        bool found = false;
        Card found_card = null;

        foreach (Card c in deck)
        {
            if (!found)
            {
                if (c.cardID == card.cardID)
                {
                    //Last element in the deck
                    if (c.cardID == deck.Last.Value.cardID)
                    {
                        return remove();
                    }

                    //A middle element
                    found_card = c;
                    c.enableInput = true;
                  
                    found = true;
                }
            }
            else
            {
                //Move all cards down
                Transform obj_trans = c.gameObject.GetComponent<Transform>();
                Transform trans = this.gameObject.GetComponent<Transform>();

                if (trans != null && obj_trans != null)
                {
                    c.move(new Vector3(trans.position.x, trans.position.y + 0.03f + (0.03f * (index - 1)), trans.position.z), 40);
                }
           }

            index++;
        }

        deck.Remove(found_card);

        return found_card;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
