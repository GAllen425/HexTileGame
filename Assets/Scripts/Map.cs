using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour {

    public HexTileType[] tileTypes;
    public int[,] tiles;

    public GameObject hexPrefab;

    //size of map in terms of hex tiles
    int mapSizeX = 20;
    int mapSizeY = 20;

    float xOffset = 0.882f;
    float zOffset = 0.764f;

	// Use this for initialization
	void Start () {
        GenerateMap();
	}
	
    void GenerateMap()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
                if (y > 10)
                {
                    tiles[x, y] = 1;
                }

                Debug.Log(x + " " + y + " " + tiles[x, y]);

                float xPos = x * xOffset;
                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }

                GameObject hex_go = (GameObject)Instantiate(tileTypes[tiles[x, y]].tileVisualPrefab, new Vector3(xPos, 0, y * zOffset), Quaternion.identity);
                hex_go.name = "Hex_" + x + "_" + y;
                hex_go.GetComponent<Hex>().x = x;
                hex_go.GetComponent<Hex>().y = y;

                hex_go.transform.SetParent(this.transform);

                hex_go.isStatic = true;
            }

        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
