using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Helper class that generates card sprite objects from images.
*/
public class CardCreator : MonoBehaviour {

    public static int mCard = 1;
    public static int iCard = 1;
    public static int cCard = 1;
    public static int roleCard = 1;

    /* FOR PLANES */
    //For small cards
    public static float S_WIDTH = 0.248f;
    public static float S_HEIGHT = 0.350f;

    //For large cards
    public static float L_WIDTH = 0.496f;
    public static float L_HEIGHT = 0.350f;

    //Zoom multipliers
    public static float NORM_MULTI = 0.67f;

    /* FOR OBJECTS */
    //For small cards
    public static float S_WIDTH_Q = 0.74f;
    public static float S_HEIGHT_Q = 0.7f;

    //For large cards
    public static float L_WIDTH_Q = 1.4f;
    public static float L_HEIGHT_Q = 0.7f;

    public static Material defaultMaterial;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Sets the art of a card, can pass either a sprite or a path
    public static bool setArt(GameObject card, Sprite art)
    {
        SpriteRenderer card_r = card.GetComponent<SpriteRenderer>();

        if (card_r != null)
        {
            card_r.sprite = art;
            /*Texture2D cardArt = spriteToTexture(art);

            if (cardArt == null)
            {
                card_r.material = defaultMaterial;
                return false;
            }

            cardArt.wrapMode = TextureWrapMode.Repeat;
            */

            Material material = new Material(Shader.Find("Standard"));

            card_r.material = material;

            return true;
        }

        return false;
    }

    public static bool setArt(GameObject card, string art)
    {
        SpriteRenderer card_r = card.GetComponent<SpriteRenderer>();

        if (card_r != null)
        {
            card_r.sprite = Resources.Load<Sprite>(art);
            /*Texture2D cardArt = Resources.Load(art) as Texture2D;
            if (cardArt == null)
            {
                card_r.material = defaultMaterial;
                return false;
            }*/

            Material material = new Material(Shader.Find("Standard"));

            card_r.material = material;
            


            return true;
        }

        return false;
    }

    //Creates a new main deck card
    public static GameObject createMainCard(string name, Sprite sprite)
    {
        GameObject newCard = new GameObject();
        newCard.name = "MainCard" + mCard;
        mCard++;

        newCard.AddComponent<MainCard>();
        newCard.AddComponent<SpriteRenderer>();

        newCard.GetComponent<MainCard>().cardName = name;
        newCard.GetComponent<MainCard>().sprite = sprite;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH_Q * NORM_MULTI, S_HEIGHT_Q * NORM_MULTI, 1);

        //Set rotation
        newCard.transform.Rotate(new Vector3(90, 0, 0));

        setArt(newCard, sprite);

        newCard.AddComponent<BoxCollider>();

        //Set renderer
        /*var renderer = newCard.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_TintColor", Color.white);*/

        return newCard;
    }

    //Creates a new incident card
    public static GameObject createIncidentCard(string name, Sprite sprite)
    {
        GameObject newCard = new GameObject();
        newCard.name = "IncidentCard" + iCard;
        iCard++;
        
        newCard.AddComponent<IncidentCard>();
        newCard.AddComponent<SpriteRenderer>();

        newCard.GetComponent<IncidentCard>().cardName = name;
        newCard.GetComponent<IncidentCard>().sprite = sprite;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH_Q* NORM_MULTI, S_HEIGHT_Q * NORM_MULTI, 1);

        //Set rotation
        newCard.transform.Rotate(new Vector3(90, 0, 0));

        setArt(newCard, sprite);

        newCard.AddComponent<BoxCollider>();

        //Set renderer
        //var renderer = newCard.GetComponent<MeshRenderer>();

        //renderer.material.SetColor("_TintColor", Color.white);

        //renderer.material.SetTextureScale("_MainTex", new Vector2(1, 1));
        //renderer.material.SetTextureOffset("_MainTex", new Vector2(0, 0));

        //UV Mapping
        /*var mesh = newCard.GetComponent<MeshFilter>();
         var uvs = mesh.mesh.uv;

         uvs[0] = new Vector2(0.0f, 0.0f);
         uvs[1] = new Vector2(0.0f, 0.5f);
         uvs[2] = new Vector2(0.5f, 1.0f);
         uvs[3] = new Vector2(1.0f, 1.0f);
        mesh.mesh.uv = uvs;*/

        return newCard;
    }

    //Creates a new character card
    public static GameObject createCharacterCard(string name, Sprite sprite)
    {
        GameObject newCard = new GameObject();
        newCard.name = "CharaCard" + cCard;
        cCard++;

        newCard.AddComponent<CharaCard>();
        newCard.AddComponent<SpriteRenderer>();

        newCard.GetComponent<CharaCard>().cardName = name;
        newCard.GetComponent<CharaCard>().sprite = sprite;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH_Q * NORM_MULTI, S_HEIGHT_Q * NORM_MULTI, 1);

        //Set rotation
        newCard.transform.Rotate(new Vector3(90, 0, 0));

        setArt(newCard, sprite);

        newCard.AddComponent<BoxCollider>();

        //Set renderer
        //var renderer = newCard.GetComponent<MeshRenderer>();

        //renderer.material.SetColor("_TintColor", Color.white);

        return newCard;
    }

    //Creates a new role card
    public static GameObject createRoleCard(string name, Sprite sprite)
    {
        GameObject newCard = new GameObject();
        newCard.name = "RoleCard" + roleCard;
        roleCard++;
        
        newCard.AddComponent<RoleCard>();
        newCard.AddComponent<SpriteRenderer>();

        newCard.GetComponent<RoleCard>().cardName = name;
        newCard.GetComponent<RoleCard>().sprite = sprite;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH_Q * NORM_MULTI, S_HEIGHT_Q * NORM_MULTI, 1);

        //Set rotation
        newCard.transform.Rotate(new Vector3(90, 0, 0));

        setArt(newCard, sprite);

        newCard.AddComponent<BoxCollider>();

        //Set renderer
        /*var renderer = newCard.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_TintColor", Color.white);*/

        return newCard;
    }

    //Utility:
    public static Texture2D spriteToTexture(Sprite sprite)
    {
            Texture2D croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                    (int)sprite.textureRect.y,
                                                    (int)sprite.textureRect.width,
                                                    (int)sprite.textureRect.height);
            
            croppedTexture.SetPixels(pixels);
            croppedTexture.Apply();

            return croppedTexture;
    }
}
