using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public Transform GroundPrefab;
    public Vector2 MapSize;
    [Range(0, 1)]    
    public float scale;

    List<Coord> allTileCoords;
    Queue<Coord> shuffeledTileCoords;
    int seed = 10;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        allTileCoords = new List<Coord>();

        for (int x = 0; x < MapSize.x; x++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        shuffeledTileCoords = new Queue<Coord> (ShuffeldArray(allTileCoords.ToArray(), seed));

        
        string HolderName = "Ground Holder";
        if (transform.FindChild(HolderName))
        {
            DestroyImmediate(transform.FindChild(HolderName).gameObject);
        }
        Transform GroundHolder = new GameObject(HolderName).transform;
        GroundHolder.parent = transform;

        for(int x = 0; x < MapSize.x; x++)
        {
            for (int y =0; y < MapSize.y; y++)
            {
                Vector3 groundPosition = new Vector3(-MapSize.x/2 + 0.5f + x, 0, -MapSize.y/2 + 0.5f + y);
                Transform newGround = Instantiate(GroundPrefab, groundPosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newGround.parent = GroundHolder;

                newGround.localScale = Vector3.one * (1 - scale);

            }
        }
    

    }
    private T[] ShuffeldArray<T>(T[] array , int seed)
    {
        System.Random indexPG = new System.Random(seed);

        for (int i = 0; i < array.Length -1; i++)
        {
            int randomIndex = indexPG.Next(i, array.Length);
            T tempItem = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = tempItem;
        }
        return array;
    }

    private Coord GetRandomIndex()
    {
        Coord randomCoord = shuffeledTileCoords.Dequeue();
        shuffeledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord
    {
        public int x;
        public int y; 

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

}
