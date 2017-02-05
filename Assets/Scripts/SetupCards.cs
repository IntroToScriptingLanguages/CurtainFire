using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCards : MonoBehaviour {
    
	// Generates all the cards
	void Start () {
        
        for (int i = 0; i < 50; i++)
        {
            GameObject.FindObjectOfType<CardCreator>().createRoleCard();
        }

        StartCoroutine(LateStart());
        
    }

    //Sets up field after all cards have been created
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.01f);

        Deck mdeck = GameObject.Find("IncidentDeck").GetComponent<Deck>();

        for (int i = 1; i <= 50; i++)
        {
            GameObject newCard = GameObject.Find("RoleCard" + i);
            mdeck.add(newCard.GetComponent<Card>());
        }
    }

    void Update () {
	}
}
