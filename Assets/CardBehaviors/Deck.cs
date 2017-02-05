using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    LinkedList<Card> deck;

	// Use this for initialization
	void Start () {
        deck = new LinkedList<Card>();
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

    // Update is called once per frame
    void Update () {
		
	}
}
