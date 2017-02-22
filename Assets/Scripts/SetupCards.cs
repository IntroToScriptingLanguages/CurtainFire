using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Helper class that sets up all the cards at the start of a match.
*/
public class SetupCards : MonoBehaviour {

    const int NUM_INCIDENTS = 16; //Different types of incidents
    const int NUM_DECK = 19; //Different types of cards
    const int NUM_MAINS = 60;

    int num_deck_cards; //Number of cards in the main deck
    int num_characters = 8;

    //Expansion numbers: 0 = Danmaku!!, 1 = Lunatic Extra

    //Characters: take note the format is "{name, expansion, index}"
    string[][] characters =
    {
        new string[] {"Alice Margatroid", "0", "0"},
        new string[] {"Cirno", "0", "1"},
        new string[] {"Hakurei Reimu", "0", "2"},
        new string[] {"Hijiri Byakuren", "0", "3"},
        new string[] {"Hinanawi Tenshi", "0", "4"},
        new string[] {"Hong Meiling", "0", "5"},
        new string[] {"Ibuki Suika", "0", "6"},
        new string[] {"Izayoi Sakuya", "0", "7"},
        new string[] {"Kamishirasawa Keine", "0", "8"},
        new string[] {"Nitori Kawashiro", "0", "9"},
        new string[] {"Kazami Yuuka", "0", "10"},
        new string[] {"Kirisame Marisa", "0", "11"},
        new string[] {"Kochiya Sanae", "0", "12"},
        new string[] {"Komeiji Satori", "0", "13"},
        new string[] {"Konpaku Youmu", "0", "14"},
        new string[] {"Mononobe no Futo", "0", "15"},
        new string[] {"Patchouli Knowledge", "0", "16"},
        new string[] {"Reisen Udongein Inaba", "0", "17"},
        new string[] {"Reiuji Utsuho", "0", "18"},
        new string[] {"Remilia Scarlet", "0", "19"},
        new string[] {"Shameimaru Aya", "0", "20"},
        new string[] {"Toyosatomimi no Miko", "0", "21"},
        new string[] {"Yagokoro Eirin", "0", "22"},
        new string[] {"Yakumo Yukari", "0", "23"},
        new string[] {"Flandre Scarlet", "1", "0"},
        new string[] {"Fujiwara no Mokou", "1", "1"},
        new string[] {"Hoshiguma Yuugi", "1", "2"},
        new string[] {"Komeiji Koishi", "1", "3"},
        new string[] {"Saigyouji Yuyuko", "1", "4"},
        new string[] {"Toramaru Shou", "1", "5"}
    };

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

    List<GameObject> iCards; //Incident cards
    List<GameObject> dCards; //Main deck cards
    
	// Generates all the cards
	void Start () {

        iCards = new List<GameObject>();
        dCards = new List<GameObject>();

        Sprite[] incidents = Resources.LoadAll<Sprite>("CardArt/Incidents");

        //Setup the incident deck
        for (int i = 0; i < NUM_INCIDENTS; i++)
        {
            GameObject new_card = CardCreator.createIncidentCard(incident_names[i], incidents[i]);
            iCards.Add(new_card);
        }

        Sprite[] main_deck = Resources.LoadAll<Sprite>("CardArt/Deck1");

        //Setup the main deck
        num_deck_cards = 0;
        for (int i = 0; i < NUM_DECK; i++)
        {
            for (int j=0; j < deck_count[i]; j++)
            {
                GameObject new_card = CardCreator.createMainCard(deck_names[i], main_deck[i]);
                dCards.Add(new_card);
            }

            num_deck_cards += deck_count[i];
        }

        StartCoroutine(LateStart());
    }

    //Sets up field after all cards have been created
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.01f);

        //Fetch and randomize the character sprites
        Sprite[] charaSprites = Resources.LoadAll<Sprite>("CardArt/Characters");
        Sprite[] charaSpritesX1 = Resources.LoadAll<Sprite>("CardArt/CharactersX1");

        //Flip the iCards and dCards
        for (int i=0; i<iCards.Count; i++)
        {
            iCards[i].GetComponent<IncidentCard>().flip();
        }

        for (int i = 0; i < dCards.Count; i++)
        {
            dCards[i].GetComponent<MainCard>().flip();
        }

        //Scramble characters
        for (int i=0; i<characters.Length; i++)
        {
            int new_index = Utilities.random.Next(0, characters.Length);

            string[] temp = characters[i];
            characters[i] = characters[new_index];
            characters[new_index] = temp;
        }
        

        //Setup character cards and UI for the player
        for (int i=0; i<Player.num_players; i++)
        {
            Player player = Player.list[i];

            //Generate character cards for each player
            if (player != null)
            {
                string[] cur_chara = characters[i];
                int index = System.Convert.ToInt32(cur_chara[2]);

                Debug.Log("Player " + i + " is " + cur_chara[0]);

                //Add the sprite for our card
                Sprite cur_sprite;

                if (cur_chara[1] == "0")
                {
                    cur_sprite = charaSprites[index];
                }
                else if (cur_chara[1] == "1")
                {
                    cur_sprite = charaSpritesX1[index];
                }
                else
                {
                    cur_sprite = null;
                }

                //Attach the name and sprite of our card to the player
                player.chara_name = cur_chara[0];
                player.chara_sprite = cur_sprite;
            }

        }

        

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

        //Shuffle the decks
        Deck mdeck = GameObject.Find("MainDeck").GetComponent<Deck>();
        Deck ideck = GameObject.Find("IncidentDeck").GetComponent<Deck>();

        mdeck.Shuffle();
        ideck.Shuffle();
    }

    void Update () {
	}
}
