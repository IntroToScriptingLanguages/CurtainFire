using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour {

    public int roleCard = 1;

    //For small cards
    public static float S_WIDTH = 0.248f;
    public static float S_HEIGHT = 0.35f;

    //For large cards
    public static float L_WIDTH = 0.496f;
    public static float L_HEIGHT = 0.35f;

    //Zoom multipliers
    public static float NORM_MULTI = 0.67f;

    public Material defaultMaterial;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool setArt(GameObject card, string artPath)
    {
        Renderer card_r = card.GetComponent<MeshRenderer>();

        if (card_r != null)
        {
            Texture2D cardArt = Resources.Load(artPath) as Texture2D;
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

    //Creates a new role card
    public GameObject createRoleCard()
    {
        GameObject newCard = GameObject.CreatePrimitive(PrimitiveType.Plane);
        newCard.name = "RoleCard" + roleCard;
        roleCard++;
        
        newCard.AddComponent<RoleCard>();

        //Set Transform
        newCard.transform.position = new Vector3(0, 2, 0);
        newCard.transform.localScale = new Vector3(S_WIDTH * NORM_MULTI, 1, S_HEIGHT * NORM_MULTI);

        //Set rotation
        newCard.transform.Rotate(new Vector3(0, 180, 0));

        setArt(newCard, "CardArt/Roles/role_rival");

        //Set renderer
        var renderer = newCard.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_TintColor", Color.white);

        return newCard;
    }
}
