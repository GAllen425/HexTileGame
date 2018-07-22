using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    public int hexX;
    public int hexY;
    public Map map;

    public List<Node> currentPath = null;

    public Vector3 destination;
    public Hex currentHex;
    float speed = 1.8f;
    public float movementTurn = 5;

	// Use this for initialization
	void Start () {
        Debug.Log(map);
        destination = transform.position;
        GetCurrentHex();
	}

    RaycastHit hit;

    // Update is called once per frame
    void Update () {

        if(currentPath != null)
        {
            int currNode = 0;
            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = Map.HexTileToVector3 ( currentPath[currNode].x, currentPath[currNode].y ) + new Vector3(0 , +1f ,0) ;
                Vector3 end = Map.HexTileToVector3(currentPath[currNode+1].x, currentPath[currNode+1].y) + new Vector3(0, +1f ,0);
                Debug.DrawLine(start, end, Color.red);
                currNode++;
            }
        }

        if (currentPath != null && destination == transform.position)
        {
            MoveNextTile();
        }

        Vector3 dir = destination - transform.position;
        Vector3 velocity = dir.normalized * speed * Time.deltaTime;

        //Make sure the velocity doesnt actually exceed the distance we want
        velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);
        transform.Translate(velocity);

        
		
	}


    public void RefreshMovementSpeed()
    {
        movementTurn = 5;
    }

    public void MoveNextTile()
    {

        if (currentPath == null)
            return;

        if (movementTurn > 0)
        {
            destination = Map.HexTileToVector3(currentPath[1].x, currentPath[1].y) + new Vector3(0, 0.1f, 0);
            hexX = currentPath[1].x;
            hexY = currentPath[1].y;
            movementTurn -= map.CostToEnterTile(currentPath[1].x, currentPath[1].y);
            currentPath.RemoveAt(0);
        }
        else
        {
            destination = transform.position;
            currentPath = null;
        }
        

        // must be destination so clear our pathfinding info
        if (currentPath != null && currentPath.Count == 1)
        {
            currentPath = null;
        } 
        
    }

        public void GetCurrentHex()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            GameObject ourHitObject = hit.collider.transform.parent.gameObject;
            //Debug.Log("collider : " + hit.collider.transform.parent.gameObject.name);
            if (ourHitObject.GetComponent<Hex>() != null)
            {
                currentHex = ourHitObject.GetComponent<Hex>();
                //Debug.Log("CURRENT HEX: " + currentHex.name);
            }
        }
        else { Debug.Log("Raycast failed" + transform.position + " " + hit.collider.name); }
    }
}
