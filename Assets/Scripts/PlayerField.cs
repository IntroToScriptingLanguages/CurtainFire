using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerField : MonoBehaviour {

    //Field cards in the top row
    static List<Card>[] field_cards_top;

    //Field cards in the bottom row
    static List<Card>[] field_cards_bottom;

    //Pivot
    public static GameObject pivot;
    public static Transform pivot_t;

    //Fields
    public static GameObject p1;
    public static GameObject p2;
    public static GameObject p3;
    public static GameObject p4;
    public static GameObject p5;
    public static GameObject p6;
    public static GameObject p7;
    public static GameObject p8;

    // Use this for initialization
    void Start () {
        //Setup fields
        p1 = GameObject.Find("Player1Field");
        p2 = GameObject.Find("Player2Field");
        p3 = GameObject.Find("Player3Field");
        p4 = GameObject.Find("Player4Field");
        p5 = GameObject.Find("Player5Field");
        p6 = GameObject.Find("Player6Field");
        p7 = GameObject.Find("Player7Field");
        p8 = GameObject.Find("Player8Field");

        pivot = GameObject.Find("Pivot");
        pivot_t = pivot.GetComponent<Transform>();

        field_cards_top = new List<Card>[8];
        field_cards_bottom = new List<Card>[8];

        for (int i = 0; i < 8; i++)
        {
            field_cards_top[i] = new List<Card>();
            field_cards_bottom[i] = new List<Card>();
        }
    }

    //Gets the field that belongs to the number
    public static GameObject get(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                return p1;
            case 2:
                return p2;
            case 3:
                return p3;
            case 4:
                return p4;
            case 5:
                return p5;
            case 6:
                return p6;
            case 7:
                return p7;
            case 8:
                return p8;
            default:
                return null;
        }
    }

    //Gets the position of the field that belongs to the player.  Returns a vector to 0, 0, 0 if that field can't be found.
    //Place card in either the top row or the bottom row
    public static Vector3 getPosition(int playerNum, bool top_row)
    {
        GameObject board = PlayerField.get(playerNum);

        if (board == null)
        {
            return new Vector3(0, 0, 0);
        }

        var field_trans = board.GetComponent<Transform>();

        if (field_trans == null)
        {
            return new Vector3(0, 0, 0);
        }

        

        return new Vector3(field_trans.position.x, 1, field_trans.position.z);
    }

    //Moves a card to the designated board
    //Place card in either the top row or the bottom row
    public static void move(Card card, int playerNum, bool top_row)
    {
        Vector3 target = PlayerField.getPosition(playerNum, top_row);

        float horOff = 0; //Plus is right, minus is left
        float vertOff = 0; //Plus is top, minus is bottom
        float upOff = 0; //Plus is up, minus is down

        if (target != null)
        {
            //Determine offsets
            //Find the right card list for the card
            List<Card>[] all_card_list;

            if (top_row)
            {
                all_card_list = PlayerField.field_cards_top;
            }
            else
            {
                all_card_list = PlayerField.field_cards_bottom;
            }



            int count;

            //Take into account rows
            if (!top_row)
            {
                vertOff += 3;
            }

            List<Card> card_list;

            int card_count;

            //REFORMAT THE OLD BOARD:
            if (card.owner >= 1 && card.owner <= 8)
            {
                card_list = all_card_list[card.owner - 1];
                count = card_list.Count;

                Vector3 source = PlayerField.getPosition(card.owner, top_row);

                //Will try to fit a four_card formation, if that doesn't work, the card list will be shrunk to allow a sixth card to fit.
                if (count > 4)
                {
                    //Over four cards remaining
                    count--;

                    //Calculate the amount of space between each card
                    float dist_between;

                    if (count == 4)
                    {
                        dist_between = 2.0f;
                    }
                    else
                    {
                        dist_between = 6.0f / (float)(count - 1);
                    }

                    card_count = 0;

                    //Removes card from list
                    card_list.Remove(card);

                    //Fixes formatting
                    foreach (Card c in card_list)
                    {
                        Transform t = c.gameObject.GetComponent<Transform>();

                        if (count <= 4)
                        {
                            upOff = 0;
                        }
                        else
                        {
                            upOff = (card_count * 0.01f);
                        }

                        if (t != null)
                        {
                            //Calculate offset from center of board
                            float cardOffset = (-3.0f) + (card_count * dist_between);

                            //Players 1/2: negative hor, negative vert
                            if (playerNum == 1 || playerNum == 2)
                            {
                                c.move(new Vector3(source.x - cardOffset, source.y + upOff, source.z - vertOff), 40);
                            }
                            //Players 3/4: positive vert, negative hor
                            else if (playerNum == 3 || playerNum == 4)
                            {
                                c.move(new Vector3(source.x + vertOff, source.y + upOff, source.z - cardOffset), 40);
                            }

                            //Player 5/6: positive hor, positive vert 
                            else if (playerNum == 5 || playerNum == 6)
                            {
                                c.move(new Vector3(source.x + cardOffset, source.y + upOff, source.z + vertOff), 40);
                            }

                            //Player 7/8: negative vert, positive hor
                            else if (playerNum == 7 || playerNum == 8)
                            {
                                c.move(new Vector3(source.x - vertOff, source.y + upOff, source.z + cardOffset), 40);
                            }

                            card_count++;
                        }
                    }

                }
                else
                {
                    //Four cards remaining

                    //Fixes formatting
                    int push_index = card_list.IndexOf(card);

                    if (push_index != -1)
                    {
                        //Removes card from list
                        card_list.Remove(card);

                        for (int i = push_index; i < card_list.Count; i++)
                        {

                            Card c = card_list[i];

                            float cardOffset = (-3.0f) + (i * 2.0f);

                            //Players 1/2: negative hor, negative vert
                            if (playerNum == 1 || playerNum == 2)
                            {
                                c.move(new Vector3(source.x - cardOffset, source.y + upOff, source.z - vertOff), 40);
                            }
                            //Players 3/4: positive vert, negative hor
                            else if (playerNum == 3 || playerNum == 4)
                            {
                                c.move(new Vector3(source.x + vertOff, source.y + upOff, source.z - cardOffset), 40);
                            }

                            //Player 5/6: positive hor, positive vert 
                            else if (playerNum == 5 || playerNum == 6)
                            {
                                c.move(new Vector3(source.x + cardOffset, source.y + upOff, source.z + vertOff), 40);
                            }

                            //Player 7/8: negative vert, positive hor
                            else if (playerNum == 7 || playerNum == 8)
                            {
                                c.move(new Vector3(source.x - vertOff, source.y + upOff, source.z + cardOffset), 40);
                            }
                        }
                    }
                }

            }

            //FORMAT THE NEW BOARD:
            card_list = all_card_list[playerNum - 1];
            count = card_list.Count;

            //Will try to fit a four_card formation, if that doesn't work, the card list will be shrunk to allow a sixth card to fit.
            if (count < 4)
            {
                horOff -= 3;
                horOff += count * 2f;
                upOff = 0;
            }
            else
            {
                //Calculate the amount of space between each card
                float dist_between = 6.0f / (float) (count);

                card_count = 0;

                //Push everything over
                foreach (Card c in card_list)
                {
                    Transform t = c.gameObject.GetComponent<Transform>();

                    

                    upOff = (card_count * 0.01f);

                    if (t != null)
                    {
                        //Calculate offset from center of board
                        float cardOffset = (-3.0f) + (card_count * dist_between);

                        //Players 1/2: negative hor, negative vert
                        if (playerNum == 1 || playerNum == 2)
                        {
                            c.move(new Vector3(target.x - cardOffset, target.y + upOff, target.z - vertOff), 40);
                        }
                        //Players 3/4: positive vert, negative hor
                        else if (playerNum == 3 || playerNum == 4)
                        {
                            c.move(new Vector3(target.x + vertOff, target.y + upOff, target.z - cardOffset), 40);
                        }

                        //Player 5/6: positive hor, positive vert 
                        else if (playerNum == 5 || playerNum == 6)
                        {
                            c.move(new Vector3(target.x + cardOffset, target.y + upOff, target.z + vertOff), 40);
                        }

                        //Player 7/8: negative vert, positive hor
                        else if (playerNum == 7 || playerNum == 8)
                        {
                            c.move(new Vector3(target.x - vertOff, target.y + upOff, target.z + cardOffset), 40);
                        }

                        card_count++;
                    }
                }

                horOff += 3;
                upOff += count * 0.01f;
            }

            


            //Apply offsets   (x)             (z)

            //Players 1/2: negative hor, negative vert
            if (playerNum == 1 || playerNum == 2)
            {
                target = new Vector3(target.x - horOff, target.y + upOff, target.z - vertOff);
                card.rotateCard(Facing.Down);
            }
            //Players 3/4: positive vert, negative hor
            else if (playerNum == 3 || playerNum == 4)
            {
                target = new Vector3(target.x + vertOff, target.y + upOff, target.z - horOff);
                card.rotateCard(Facing.Right);
            }

            //Player 5/6: positive hor, positive vert 
            else if (playerNum == 5 || playerNum == 6)
            {
                target = new Vector3(target.x + horOff, target.y + upOff, target.z + vertOff);
                card.rotateCard(Facing.Up);
            }

            //Player 7/8: negative vert, positive hor
            else if (playerNum == 7 || playerNum == 8)
            {
                target = new Vector3(target.x - vertOff, target.y + upOff, target.z + horOff);
                card.rotateCard(Facing.Left);
            }

            //Add the new card to the card_list
            card_list.Add(card);

            card.move(target, 20);

            card.owner = playerNum;
        }
    }

    //Removes the first card from the board
    public static Card removeFirst(int playerNum, bool top_row)
    {
        Card card;

        if (top_row)
        {
            if (field_cards_top[playerNum - 1].Count == 0)
            {
                return null;
            }

            card = field_cards_top[playerNum - 1][0];
        }
        else
        {
            if (field_cards_bottom[playerNum - 1].Count == 0)
            {
                return null;
            }

            card = field_cards_bottom[playerNum - 1][0];
        }

        return removeFromBoard(card, playerNum);
    }

    //Removes the last card from the board
    public static Card removeLast(int playerNum, bool top_row)
    {
        Card card;

        if (top_row)
        {
            if (field_cards_top[playerNum-1].Count == 0)
            {
                return null;
            }

            card = field_cards_top[playerNum - 1][field_cards_bottom[playerNum - 1].Count - 1];
        }
        else
        {
            if (field_cards_bottom[playerNum - 1].Count == 0)
            {
                return null;
            }

            card = field_cards_bottom[playerNum - 1][field_cards_bottom[playerNum - 1].Count - 1];
        }

        return removeFromBoard(card, playerNum);
    }

    //Removes a card from a board, if it can't be found, return null
    public static Card removeFromBoard(Card c, int playerNum)
    {
        if (playerNum >= 1 && playerNum <= 8)
        {
            List<Card> top_cards = field_cards_top[playerNum - 1];
            List<Card> bottom_cards = field_cards_bottom[playerNum - 1];

            float upOff = 0;

            foreach (Card current in top_cards)
            {
                if (current.cardID == c.cardID)
                {
                        List<Card> card_list = top_cards;

                        int count = card_list.Count;
                        float vertOff = 0;

                        Vector3 source = PlayerField.getPosition(current.owner, true);

                        //Will try to fit a four_card formation, if that doesn't work, the card list will be shrunk to allow a sixth card to fit.
                        if (count > 4)
                        {

                            count--;

                            //Calculate the amount of space between each card
                            float dist_between;

                            if (count == 4)
                            {
                                dist_between = 2.0f;
                            }
                            else
                            {
                                dist_between = 6.0f / (float)(count - 1);
                            }

                            int card_count = 0;

                            //Removes card from list
                            card_list.Remove(current);

                            //Fixes formatting
                            foreach (Card c_card in card_list)
                            {
                                Transform t = c_card.gameObject.GetComponent<Transform>();

                                if (count <= 4)
                                {
                                    upOff = 0;
                                }
                                else
                                {
                                    upOff = (card_count * 0.01f);
                                }

                                if (t != null)
                                {
                                    //Calculate offset from center of board
                                    float cardOffset = (-3.0f) + (card_count * dist_between);

                                    //Players 1/2: negative hor, negative vert
                                    if (playerNum == 1 || playerNum == 2)
                                    {
                                        c_card.move(new Vector3(source.x - cardOffset, source.y + upOff, source.z - vertOff), 40);
                                    }
                                    //Players 3/4: positive vert, negative hor
                                    else if (playerNum == 3 || playerNum == 4)
                                    {
                                        c_card.move(new Vector3(source.x + vertOff, source.y + upOff, source.z - cardOffset), 40);
                                    }

                                    //Player 5/6: positive hor, positive vert 
                                    else if (playerNum == 5 || playerNum == 6)
                                    {
                                        c_card.move(new Vector3(source.x + cardOffset, source.y + upOff, source.z + vertOff), 40);
                                    }

                                    //Player 7/8: negative vert, positive hor
                                    else if (playerNum == 7 || playerNum == 8)
                                    {
                                        c_card.move(new Vector3(source.x - vertOff, source.y + upOff, source.z + cardOffset), 40);
                                    }

                                    card_count++;
                                }
                            }

                        }
                        else
                        {
                            //Fixes formatting
                            int push_index = card_list.IndexOf(current);

                            //Removes card from list
                            card_list.Remove(current);

                            for (int i = push_index; i < card_list.Count; i++)
                            {
                                Card card = card_list[i];

                                float cardOffset = (-3.0f) + (i * 2.0f);

                                //Players 1/2: negative hor, negative vert
                                if (playerNum == 1 || playerNum == 2)
                                {
                                    card.move(new Vector3(source.x - cardOffset, source.y + upOff, source.z - vertOff), 40);
                                }
                                //Players 3/4: positive vert, negative hor
                                else if (playerNum == 3 || playerNum == 4)
                                {
                                    card.move(new Vector3(source.x + vertOff, source.y + upOff, source.z - cardOffset), 40);
                                }

                                //Player 5/6: positive hor, positive vert 
                                else if (playerNum == 5 || playerNum == 6)
                                {
                                    card.move(new Vector3(source.x + cardOffset, source.y + upOff, source.z + vertOff), 40);
                                }

                                //Player 7/8: negative vert, positive hor
                                else if (playerNum == 7 || playerNum == 8)
                                {
                                    card.move(new Vector3(source.x - vertOff, source.y + upOff, source.z + cardOffset), 40);
                                }
                            }
                        }

                        return current;
                    }
            }

            foreach (Card current in bottom_cards)
            {
                if (current.cardID == c.cardID)
                {
                    List<Card> card_list = bottom_cards;

                    int count = card_list.Count;
                    float vertOff = 3;

                    Vector3 source = PlayerField.getPosition(current.owner, false);

                    //Will try to fit a four_card formation, if that doesn't work, the card list will be shrunk to allow a sixth card to fit.
                    if (count > 4)
                    {

                        count--;

                        //Calculate the amount of space between each card
                        float dist_between;

                        if (count == 4)
                        {
                            dist_between = 2.0f;
                        }
                        else
                        {
                            dist_between = 6.0f / (float)(count - 1);
                        }

                        int card_count = 0;

                        //Removes card from list
                        card_list.Remove(current);

                        //Fixes formatting
                        foreach (Card c_card in card_list)
                        {
                            Transform t = c_card.gameObject.GetComponent<Transform>();

                            if (count <= 4)
                            {
                                upOff = 0;
                            }
                            else
                            {
                                upOff = (card_count * 0.01f);
                            }

                            if (t != null)
                            {
                                //Calculate offset from center of board
                                float cardOffset = (-3.0f) + (card_count * dist_between);

                                //Players 1/2: negative hor, negative vert
                                if (playerNum == 1 || playerNum == 2)
                                {
                                    c_card.move(new Vector3(source.x - cardOffset, source.y + upOff, source.z - vertOff), 40);
                                }
                                //Players 3/4: positive vert, negative hor
                                else if (playerNum == 3 || playerNum == 4)
                                {
                                    c_card.move(new Vector3(source.x + vertOff, source.y + upOff, source.z - cardOffset), 40);
                                }

                                //Player 5/6: positive hor, positive vert 
                                else if (playerNum == 5 || playerNum == 6)
                                {
                                    c_card.move(new Vector3(source.x + cardOffset, source.y + upOff, source.z + vertOff), 40);
                                }

                                //Player 7/8: negative vert, positive hor
                                else if (playerNum == 7 || playerNum == 8)
                                {
                                    c_card.move(new Vector3(source.x - vertOff, source.y + upOff, source.z + cardOffset), 40);
                                }

                                card_count++;
                            }
                        }

                    }
                    else
                    {
                        //Fixes formatting
                        int push_index = card_list.IndexOf(current);

                        //Removes card from list
                        card_list.Remove(current);

                        for (int i = push_index; i < card_list.Count; i++)
                        {
                            Card card = card_list[i];

                            float cardOffset = (-3.0f) + (i * 2.0f);

                            //Players 1/2: negative hor, negative vert
                            if (playerNum == 1 || playerNum == 2)
                            {
                                card.move(new Vector3(source.x - cardOffset, source.y + upOff, source.z - vertOff), 40);
                            }
                            //Players 3/4: positive vert, negative hor
                            else if (playerNum == 3 || playerNum == 4)
                            {
                                card.move(new Vector3(source.x + vertOff, source.y + upOff, source.z - cardOffset), 40);
                            }

                            //Player 5/6: positive hor, positive vert 
                            else if (playerNum == 5 || playerNum == 6)
                            {
                                card.move(new Vector3(source.x + cardOffset, source.y + upOff, source.z + vertOff), 40);
                            }

                            //Player 7/8: negative vert, positive hor
                            else if (playerNum == 7 || playerNum == 8)
                            {
                                card.move(new Vector3(source.x - vertOff, source.y + upOff, source.z + cardOffset), 40);
                            }
                        }
                    }

                    return current;
                }
            }
        }

        return null;
    }

    //Teleports a GameObject with a Transform on a field
    public static bool place(GameObject obj, int playernum)
    {
        var trans = obj.GetComponent<Transform>();

        if (trans == null)
        {
            return false;
        }

        GameObject field = get(playernum);

        if (field == null)
        {
            return false;
        }

        var field_trans = field.GetComponent<Transform>();

        if (field_trans == null)
        {
            return false;
        }

        trans.position = new Vector3(field_trans.position.x, 1, field_trans.position.z);

        return true;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
