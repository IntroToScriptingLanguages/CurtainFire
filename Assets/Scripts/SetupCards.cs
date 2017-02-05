using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCards : MonoBehaviour {

    const int NUM_INCIDENTS = 16;
    const int NUM_MAINS = 60;
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
    
	// Generates all the cards
	void Start () {

        Sprite[] incidents = Resources.LoadAll<Sprite>("CardArt/Incidents");

        //Setup the incident deck
        for (int i = 0; i < NUM_INCIDENTS; i++)
        {
            CardCreator.createIncidentCard(incident_names[i], incidents[i], i);
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
        
    }

    void Update () {
	}
}
