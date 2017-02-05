using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    List<Card> deck;
    private static System.Random rng = new System.Random();

    // Use this for initialization
    void Start () {
        deck = new List<Card>();
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
            deck[count() - 1].enableInput = false;
        }

        if (trans != null)
        {
            int deck_size = deck.Count;
            
            c.move(new Vector3(trans.position.x, trans.position.y + 0.03f + (0.03f * deck_size), trans.position.z), 25);
            deck.Add(c);
        }

    }

    public Card peek()
    {
        return deck[count() - 1];
    }

    //Removes the top value
    public Card remove()
    {
        Card c = deck[count() - 1];
        deck.RemoveAt(count() - 1);

        if (deck.Count > 0)
        {
            deck[count() - 1].enableInput = true;
        }


        return c;
    }

    //Removes a card if it's the same as the specified one
    public Card remove(Card card)
    {
        int index = 0; //Index of the card removed
        bool found = false;
        Card found_card = null;

        //Last element in the deck
        if (count() > 0 && card.cardID == deck[count() - 1].cardID)
        {
            return remove();
        }

        foreach (Card c in deck)
        {
            if (!found)
            {
                if (c.cardID == card.cardID)
                {
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

    //Shuffles the deck
    public void Shuffle()
    {
        //Performs the shuffling based on Fisher-Yates
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
        
        //Reorders the cards based on the deck
        int index = 0;

        foreach (Card c in deck)
        {
            Transform trans = this.gameObject.GetComponent<Transform>();

            if (trans != null)
            {
                c.move(new Vector3(trans.position.x, trans.position.y + 0.03f + (0.03f * (index)), trans.position.z), 40);
                index++;
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
