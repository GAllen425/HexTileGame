using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

    public int x;
    public int y;
    public bool occupied = false;
    public Color normalColor;
    public Color highlightColor;
    

    public void OnMouseEnter(GameObject ourHitObject, Unit selectedUnit, MeshRenderer mr, Color normalColor, Color highlightColor)
    {
        if (selectedUnit != null && ourHitObject.GetComponent<Hex>() != null)
        {
            Debug.Log("Enter hex");
            mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
            normalColor = mr.material.color;
            mr.material.color = highlightColor;
        }
    }

    public void OnMouseExit(GameObject ourHitObject, Unit selectedUnit, MeshRenderer mr, Color normalColor)
    {
        if (selectedUnit != null && ourHitObject.GetComponent<Hex>() != null)
        {
            Debug.Log("Exit Hex");
            mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
            mr.material.color = normalColor;
        }
    }

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
