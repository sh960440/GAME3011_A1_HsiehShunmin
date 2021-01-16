using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrafab;
    public GameObject[,] tiles;
    public int size = 20;
    public float startXPosition = -4.75f;
    public float startYPosition = 4.75f;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[size,size];
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                Vector3 spawnPosition = new Vector3(startXPosition + i * 0.5f, startYPosition - j * 0.5f, transform.position.z);
                tiles[i,j] = Instantiate(tilePrafab, spawnPosition, transform.rotation);
            }
        }

        int x = Random.Range(0, 9);
        int y = Random.Range(0, 9);
        
        tiles[x,y].GetComponent<Tile>().Value = 8;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
