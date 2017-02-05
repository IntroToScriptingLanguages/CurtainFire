using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour {

    public static int mCard = 1;
    public static int iCard = 1;
    public static int cCard = 1;
    public static int roleCard = 1;

    //For small cards
    public static float S_WIDTH = 0.248f;
    public static float S_HEIGHT = 0.35f;

    //For large cards
    public static float L_WIDTH = 0.496f;
    public static float L_HEIGHT = 0.35f;

    //Zoom multipliers
    public static float NORM_MULTI = 0.67f;

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
        Renderer card_r = card.GetComponent<MeshRenderer>();

        if (card_r != null)
        {
            Texture2D cardArt = spriteToTexture(art);
            if (cardArt == null)
            {
                card_r.material = defaultMaterial;
                return false;
            }

            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = cardArt;

            card_r.material = material;

            return true;
        }

        return false;
    }

    public static bool setArt(GameObject card, string art)
    {
        Renderer card_r = card.GetComponent<MeshRenderer>();

        if (card_r != null)
        {
            Texture2D cardArt = Resources.Load(art) as Texture2D;
            if (cardArt == null)
            {
                card_r.material = defaultMaterial;
                return false;
            }

            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = cardArt;

            card_r.material = material;

            return true;
        }

        return false;
    }

    //Creates a new main deck card
    public static GameObject createMainCard(string name, Sprite sprite, int sprite_index)
    {
        GameObject newCard = GameObject.CreatePrimitive(PrimitiveType.Plane);
        newCard.name = "MainCard" + mCard;
        mCard++;

        newCard.AddComponent<MainCard>();
        newCard.GetComponent<MainCard>().cardName = name;
        newCard.GetComponent<MainCard>().spriteIndex = sprite_index;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH * NORM_MULTI, 1, S_HEIGHT * NORM_MULTI);

        //Set rotation
        newCard.transform.Rotate(new Vector3(0, 180, 0));

        setArt(newCard, sprite);

        //Set renderer
        var renderer = newCard.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_TintColor", Color.white);

        return newCard;
    }

    //Creates a new incident card
    public static GameObject createIncidentCard(string name, Sprite sprite, int sprite_index)
    {
        GameObject newCard = GameObject.CreatePrimitive(PrimitiveType.Plane);
        newCard.name = "IncidentCard" + iCard;
        iCard++;

        newCard.AddComponent<IncidentCard>();
        newCard.GetComponent<IncidentCard>().cardName = name;
        newCard.GetComponent<IncidentCard>().spriteIndex = sprite_index;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH * NORM_MULTI, 1, S_HEIGHT * NORM_MULTI);

        //Set rotation
        newCard.transform.Rotate(new Vector3(0, 180, 0));

        setArt(newCard, sprite);

        //Set renderer
        var renderer = newCard.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_TintColor", Color.white);

        return newCard;
    }

    //Creates a new character card
    public static GameObject createCharacterCard(string name, Sprite sprite, int sprite_index)
    {
        GameObject newCard = GameObject.CreatePrimitive(PrimitiveType.Plane);
        newCard.name = "CharaCard" + cCard;
        cCard++;

        newCard.AddComponent<CharaCard>();
        newCard.GetComponent<CharaCard>().cardName = name;
        newCard.GetComponent<CharaCard>().spriteIndex = sprite_index;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(L_WIDTH * NORM_MULTI, 1, L_HEIGHT * NORM_MULTI);

        //Set rotation
        newCard.transform.Rotate(new Vector3(0, 180, 0));

        setArt(newCard, sprite);

        //Set renderer
        var renderer = newCard.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_TintColor", Color.white);

        return newCard;
    }

    //Creates a new role card
    public static GameObject createRoleCard(string name, Sprite sprite, int sprite_index)
    {
        GameObject newCard = GameObject.CreatePrimitive(PrimitiveType.Plane);
        newCard.name = "RoleCard" + roleCard;
        roleCard++;
        
        newCard.AddComponent<RoleCard>();
        newCard.GetComponent<RoleCard>().cardName = name;
        newCard.GetComponent<RoleCard>().spriteIndex = sprite_index;

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH * NORM_MULTI, 1, S_HEIGHT * NORM_MULTI);

        //Set rotation
        newCard.transform.Rotate(new Vector3(0, 180, 0));

        setArt(newCard, sprite);

        //Set renderer
        var renderer = newCard.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_TintColor", Color.white);

        return newCard;
    }

    //Utility:
    public static Texture2D spriteToTexture(Sprite sprite)
    {
        Texture2D croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }
}
