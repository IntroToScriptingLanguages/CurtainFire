using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Helper class that sets up the field.  Also performs player setup and UI setup
*/


public class SetupField : MonoBehaviour {
    GameObject field;
    Vector3 field_dim; //Dimensions of the green field
    Vector3 field_loc; //Locations of the field transform

   

    float max_width; //Maximum distance between edge of the field and the center of the field.  Horizontal
    float max_height; //Maximum distance between edge of the field and the center of the field.  Vertical

    // Use this for initialization
    void Start () {
        field = this.gameObject;

        //Setup player zones
        /*var renderer = field.GetComponent<MeshRenderer>();
        var transform = field.GetComponent<Transform>();

        field_loc = transform.position;
        field_dim = renderer.bounds.size;

        max_width = field_dim.y / 2;
        max_height = field_dim.x / 2;*/

        //Setup players
        for (int i=1; i<=Player.num_players; i++)
        {
            bool is_human;

            //Check if the new player is a human
            if (i == 1)
            {
                is_human = true;
            }
            else
            {
                is_human = false;
            }

            Player.list.Add(new Player(i, is_human));
        }

        StartCoroutine(LateStart());
    }

    

    //Sets up the UI elements, should run after cards have been set up
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.4f);

        float zoom_scale = 1.0f;

        GameObject newImage;
        RectTransform newImage_t;
        Image image;
        Vector3 point;
        TextMesh text;

        for (int i = 0; i < Player.num_players; i++)
        {
            Player human = Player.list[i];

            //Displays character cards and other starting information for all the human players
            if (human != null && human.isHuman())
            {
                GameObject canvas = human.canvas;
                GameObject cameraObj = human.playerCamera;
                Camera camera = cameraObj.GetComponent<Camera>();
                
                //Do this for each character:

                for (int j = 0; j < Player.num_players; j++)
                {
                    Player player = Player.list[j];

                    if (player != null)
                    {
                        GameObject field = PlayerField.get(j + 1);

                        //Rotation
                        Vector3 rotation;
                        int playerNum = j + 1;

                        if (playerNum == 1 || playerNum == 2)
                        {
                            rotation = new Vector3(90, 180, 0);
                        }
                        else if (playerNum == 3 || playerNum == 4)
                        {
                            rotation = new Vector3(90, 90, 0);
                        }
                        else if (playerNum == 7 || playerNum == 8)
                        {
                            rotation = new Vector3(90, 270, 0);
                        }
                        else
                        {
                            rotation = new Vector3(90, 0, 0);
                        }

                        //Add card image
                        newImage = new GameObject();
                        newImage.name = "avatar" + (playerNum);

                        newImage.AddComponent<SpriteRenderer>();

                        //Set Transform
                        newImage.transform.position = PlayerField.getUIPosition(playerNum, PlayerUI.Image);
                        newImage.transform.localScale = new Vector3(2.0f, 2.0f, 1);

                        //Set rotation
                        newImage.transform.Rotate(rotation);

                        //Add a cropped version of this sprite
                        Sprite oldSprite = player.chara_sprite;
                        Rect crop_rect = new Rect(0, 0, oldSprite.);
                        Sprite newSprite = Sprite.Create(oldSprite.texture, crop_rect, new Vector2(0.5f, 0.5f), 100.0f);

                        CardCreator.setArt(newImage, newSprite);

                        newImage.AddComponent<BoxCollider>();


                        //Add name
                        newImage = new GameObject();
                        newImage.name = "playerName" + (playerNum);

                        text = newImage.AddComponent<TextMesh>();
                        text.text = Player.list[j].chara_name;
                        text.characterSize = 0.85f;
                        text.fontStyle = FontStyle.Bold;
                        

                        //Set Transform
                        newImage.transform.position = PlayerField.getUIPosition(playerNum, PlayerUI.Name);

                        //Set rotation
                        newImage.transform.Rotate(rotation);

                        newImage.AddComponent<BoxCollider>();

                        text.anchor = TextAnchor.MiddleCenter;


                        //Add handSize
                        newImage = new GameObject();
                        newImage.name = "handSize"+(playerNum);
                        
                        newImage.AddComponent<SpriteRenderer>();

                        //Set Transform
                        newImage.transform.position = PlayerField.getUIPosition(playerNum, PlayerUI.Hand);

                        //Set rotation
                        newImage.transform.Rotate(rotation);

                        CardCreator.setArt(newImage, "CardArt/hand");

                        newImage.AddComponent<BoxCollider>();


                        //Add life
                        newImage = new GameObject();
                        newImage.name = "life"+(playerNum);
                        
                        newImage.AddComponent<SpriteRenderer>();

                        //Set Transform
                        newImage.transform.position = PlayerField.getUIPosition(playerNum, PlayerUI.Life);

                        //Set rotation
                        newImage.transform.Rotate(rotation);

                        CardCreator.setArt(newImage, "CardArt/life");

                        newImage.AddComponent<BoxCollider>();


                        //Add hand text
                        newImage = new GameObject();
                        newImage.name = "handText" + (playerNum);

                        text = newImage.AddComponent<TextMesh>();
                        text.text = Convert.ToString(Player.list[j].handSize);
                        text.characterSize = 0.75f;

                        //Set Transform
                        newImage.transform.position = PlayerField.getUIPosition(playerNum, PlayerUI.HandText);

                        //Set rotation
                        newImage.transform.Rotate(rotation);

                        newImage.AddComponent<BoxCollider>();


                        //Add life text
                        newImage = new GameObject();
                        newImage.name = "lifeText" + (playerNum);

                        text = newImage.AddComponent<TextMesh>();
                        text.text = Convert.ToString(Player.list[j].life);
                        text.characterSize = 0.75f;

                        //Set Transform
                        newImage.transform.position = PlayerField.getUIPosition(playerNum, PlayerUI.LifeText);

                        //Set rotation
                        newImage.transform.Rotate(rotation);

                        newImage.AddComponent<BoxCollider>();

                    }
                }
            }
        }
    }

   

    // Update is called once per frame
    void Update () {
		
	}
}
