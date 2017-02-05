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

        for (int i = 1; i <= 50; i++)
        {
            Card newCard = GameObject.Find("RoleCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.IncidentDeck);
        }

        yield return new WaitForSeconds(3.0f);

        for (int i=1; i <= 50; i++)
        {
            Card newCard = GameObject.Find("RoleCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.BoardBottom, 4);
        }

        yield return new WaitForSeconds(3.0f);

        for (int i=1; i <= 30; i++)
        {
            Card newCard = GameObject.Find("RoleCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.MainDeck);
        }

        yield return new WaitForSeconds(3.0f);

        for (int i=31; i<=45; i++)
        {
            Card newCard = GameObject.Find("RoleCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.BoardTop, 7);
        }

        yield return new WaitForSeconds(3.0f);

        for (int i = 1; i <= 20; i++)
        {
            Card newCard = GameObject.Find("RoleCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.IncidentCollectDeck, 4);
        }

        yield return new WaitForSeconds(3.0f);

        for (int i = 31; i <= 35; i++)
        {
            Card newCard = GameObject.Find("RoleCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.MainDeck);
        }

        yield return new WaitForSeconds(3.0f);

        for (int i = 21; i <= 22; i++)
        {
            Card newCard = GameObject.Find("RoleCard" + i).GetComponent<Card>();
            newCard.moveZone(Zone.DiscardDeck, 4);
        }
    }

    void Update () {
	}
}
