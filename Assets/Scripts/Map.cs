using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Map : MonoBehaviour {

    public GameObject selectedUnit;

    public HexTileType[] tileTypes;
    public int[,] tiles;

    Node[,] graph;

    public GameObject hexPrefab;

    //size of map in terms of hex tiles
    int mapSizeX = 10;
    int mapSizeY = 10;

    static float xOffset = 0.882f;
    static float yOffset = 0.764f;

    // Use this for initialization
    void Start() {

        GenerateMapData();
        GeneratePathFindingGraph();
        GenerateMapVisual();
    }

    void GenerateMapData()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }
        tiles[1, 0] = 2;
        tiles[1, 1] = 2;
        tiles[1, 2] = 2;
        tiles[1, 3] = 1;
        tiles[1, 4] = 1;
        tiles[1, 5] = 1;
        tiles[1, 6] = 1;
        tiles[1, 7] = 1;


    }

    void GenerateMapVisual()
    {
        for (int x = 0; x<mapSizeX; x++)
        {
            for (int y = 0; y<mapSizeY; y++)
            {
                //Debug.Log(x + " " + y + " " + tiles[x, y]);

                GameObject hex_go = (GameObject)Instantiate(tileTypes[tiles[x, y]].tileVisualPrefab, HexTileToVector3(x,y), Quaternion.identity);
                hex_go.name = "Hex_" + x + "_" + y;
                hex_go.GetComponent<Hex>().x = x;
                hex_go.GetComponent<Hex>().y = y;

                hex_go.transform.SetParent(this.transform);
                hex_go.isStatic = true;
            }
        }
    }

    public float CostToEnterTile(int x, int y)
    {
        HexTileType tt = tileTypes[ tiles[x, y] ];

        if (UnitCanEnterTile(x, y) == false)
            return Mathf.Infinity;

        return tt.tileMovementCost;
    }

    public bool UnitCanEnterTile (int x, int y)
    {
        return tileTypes [tiles[x,y]].isWalkable ;
    }

    public void GeneratePathTo(int x, int y, Unit selectedUnit)
    {
        selectedUnit.GetComponent<Unit>().currentPath = null;

        if( UnitCanEnterTile (x,y) == false )
        {
            //clicked on a mountain
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().hexX, 
                            selectedUnit.GetComponent<Unit>().hexY];

        Node target = graph[x,y];

        dist[source] = 0;
        prev[source] = null;

        //Init everything to infinity distance, since we dont know any better right now
        //Also possible our source cant be reached
        foreach(Node v in graph)
        {
            if( v != source )
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;
            foreach (Node possibleU in unvisited)
            {
                if (u==null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach(Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(v.x, v.y);
                if (alt < dist[v] )
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            //No route to target
            return;
        }

        List<Node> currentPath = new List<Node>();
        Node curr = target;

        // step through prev chain and add to path
        while(curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();
        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    public static Vector3 HexTileToVector3(int x, int y)
    {
        float xPos = x * xOffset;
        if (y % 2 == 1)
        {
            xPos += xOffset / 2f;
        }
        return new Vector3(xPos, 0, y * yOffset);
    }

    void GeneratePathFindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            { 
                if (x > 0)
                    graph[x, y].neighbours.Add(graph[x - 1, y]);             // L

                if (x < mapSizeX - 2)
                    graph[x, y].neighbours.Add(graph[x + 1, y]);       // R

                if (y % 2 == 0)
                {
                    if (y > 0)
                    {
                        if (x > 0)
                            graph[x, y].neighbours.Add(graph[x - 1 , y - 1]);  // Bot R

                        graph[x, y].neighbours.Add(graph[x , y - 1]);  // Bot L
                    }
                    if (y < mapSizeY - 2)
                    {
                        if (x > 0)
                            graph[x, y].neighbours.Add(graph[x -1 , y + 1]); // Top R
            
                        graph[x, y].neighbours.Add(graph[x , y + 1]);   // Bot R
                    }
                }
                else
                {
                    if (y > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x , y - 1]);  // Bot R
                        if (x < mapSizeX - 2)
                            graph[x, y].neighbours.Add(graph[x + 1, y - 1]);  // Bot L
                    }
                    if (y < mapSizeY - 2)
                    {
                        graph[x, y].neighbours.Add(graph[x , y + 1]); // Top R
                        if (x < mapSizeX - 2)
                            graph[x, y].neighbours.Add(graph[x + 1,  y + 1]);   // Bot R
                    }
                }


            }
        }
    }

}
