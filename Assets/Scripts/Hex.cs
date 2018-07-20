using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

    public int x;
    public int y;
    public bool occupied = false;

  /*  public Hex[] GetNeighbours()
    {
        GameObject left = GameObject.Find("Hex_" + (x - 1) + "_" + y);
        GameObject right = GameObject.Find("Hex_" + (x + 1) + "_" + y);

        //need to check y % 2 == 1
        GameObject topLeft = GameObject.Find("Hex_" + (x + 1) + "_" + (y+1));
        GameObject topRight = GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1));
        GameObject bottomLeft = GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1));
        GameObject bottomRight = GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1));
        return;
    }
    */
}
