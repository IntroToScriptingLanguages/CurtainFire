using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCards : MonoBehaviour {

    const int NUM_INCIDENTS = 16; //Different types of incidents
    const int NUM_DECK = 19; //Different types of cards
    const int NUM_MAINS = 60;

    int num_deck_cards; //Number of cards in the main deck
    int num_characters = 8;

    //Incidents
    string[] incident_names =
    {
        "Crisis of Faith",
        "Crossing to Higan",
        "Endless Party",
        "Eternal Night",
        "Five Impossible Requests",
        "Great Barrier Weakening",
        "Great Fairy Wars",
        "Lily White",
        "Overdrive",
        "Rekindle Blazing Hell",
        "Saigyou Ayakashi Blooming",
        "Scarlet Weather Rhapsody",
        "Spring Snow",
        "Undefined Fantastic Object",
        "Voyage to Makai",
        "Worldly Desires"
    };

    //Deck
    string[] deck_names =
    {
        "1UP",
        "Bomb",
        "Borrow",
        "Capture Spell Card",
        "Focus",
        "Graze",
        "Grimoire",
        "Kourindou",
        "Last Word",
        "Master Plan",
        "Melee",
        "Mini-Hakkero",
        "Party",
        "Laser Shot",
        "Power",
        "Seal Away",
        "Shoot",
        "Sorcerer's Sutra Scroll",
        "Stopwatch"
    };

    //Number of cards of each type in the deck
    int[] deck_count =
    {
        4, //1Up
        4, //Bomb
        2, //Borrow
        1, //Capture Spell Card
        2, //Focus
        13, //Graze
        2, //Grimore
        1, //Kourindou
        1, //Last Word
        1, //Master Plan
        1, //Melee
        1, //Mini-Hakkero
        1, //Party
        1, //Laser Shot
        6, //Power
        4, //Seal Away
        24, //Shoot
        1, //Sorcerer's Sutra Scroll
        1 //Stopwatch
    };
    
	// Generates all the cards
	void Start () {

        Sprite[] incidents = Resources.LoadAll<Sprite>("CardArt/Incidents");

        //Setup the incident deck
        for (int i = 0; i < NUM_INCIDENTS; i++)
        {
            CardCreator.createIncidentCard(incident_names[i], incidents[i], i);
        }

        Sprite[] main_deck = Resources.LoadAll<Sprite>("CardArt/Deck1");

        //Setup the main deck
        num_deck_cards = 0;
        for (int i = 0; i < NUM_DECK; i++)
        {
            for (int j=0; j < deck_count[i]; j++)
            {
                CardCreator.createMainCard(deck_names[i], main_deck[i], num_deck_cards);
            }

            num_deck_cards += deck_count[i];
        }

        StartCoroutine(LateStart());
    }

    //Sets up field after all cards have been created
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.01f);

        //Setup the incident deck
        for (int i = 1; i <= NUM_INCIDENTS; i++)
        {
            Card newCard = GameObject.Find("IncidentCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.IncidentDeck);
        }
        
        //Setup the main deck
        for (int i = 1; i <= num_deck_cards; i++)
        {
            Card newCard = GameObject.Find("MainCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.MainDeck);
        }
    }

    void Update () {
	}
}
