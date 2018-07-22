using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Vector3 destination;
    public Hex currentHex;
    float speed = 2;
    float movementTurn = 5;

	// Use this for initialization
	void Start () {
        destination = transform.position;
        GetCurrentHex();
	}

    RaycastHit hit;

    // Update is called once per frame
    void Update () {


        Vector3 dir = destination - transform.position;
        Vector3 velocity = dir.normalized * speed * Time.deltaTime;

        //Make sure the velocity doesnt actually exceed the distance we want
        velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);

        transform.Translate(velocity);
		
	}

    public void GetCurrentHex()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            GameObject ourHitObject = hit.collider.transform.parent.gameObject;
            Debug.Log("collider : " + hit.collider.transform.parent.gameObject.name);
            if (ourHitObject.GetComponent<Hex>() != null)
            {
                currentHex = ourHitObject.GetComponent<Hex>();
                Debug.Log("CURRENT HEX: " + currentHex.name);
            }
        }
        else { Debug.Log("Raycast failed" + transform.position + " " + hit.collider.name); }
    }
}
