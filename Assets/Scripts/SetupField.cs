using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
