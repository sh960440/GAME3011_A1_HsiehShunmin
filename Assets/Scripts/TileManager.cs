﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrafab;
    public GameObject[,] tiles;
    public int size = 20;
    [Range(1,8)] public int maxValuePointAmount = 2;
    public float startXPosition = -4.75f;
    public float startYPosition = 4.75f;
    private int[] maxValueXs;
    private int[] maxValueYs;

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

        maxValueXs = new int[maxValuePointAmount];
        maxValueYs = new int[maxValuePointAmount]; 

        maxValueXs[0] = Random.Range(2, size - 3);
        maxValueYs[0] = Random.Range(2, size - 3);
        Debug.Log("Max value point: " + maxValueXs[0] + ", " + maxValueYs[0]);
        InitializeSpecialTiles(maxValueXs[0], maxValueYs[0]);

        if (maxValuePointAmount > 1)
        {
            for (int i = 1; i < maxValuePointAmount; i++)
            {
                FindAnotherMaxPoint(maxValueXs, maxValueYs, i);
                Debug.Log("Max value point: " + maxValueXs[i] + ", " + maxValueYs[i]);
                InitializeSpecialTiles(maxValueXs[i], maxValueYs[i]);
            }
        }
    }

    void InitializeSpecialTiles(int xIndex, int yIndex)
    {
        for (int i = xIndex - 2; i <= xIndex + 2; i++)
        {
            for (int j = yIndex - 2; j <= yIndex + 2; j++)
            {
                tiles[i, j].GetComponent<Tile>().Value = TileValue.QUARTER;
            }
        }

        for (int i = xIndex - 1; i <= xIndex + 1; i++)
        {
            for (int j = yIndex - 1; j <= yIndex + 1; j++)
            {
                tiles[i, j].GetComponent<Tile>().Value = TileValue.HALF;
            }
        }

        tiles[xIndex,yIndex].GetComponent<Tile>().Value = TileValue.MAX;
    }

    void FindAnotherMaxPoint(int[] xPositions, int[] yPositions, int index)
    {
        int triedTimes = 0;
        bool gotNewPosition = false;
        while (!gotNewPosition)
        {
            // Randomly get a point
            int tempX = Random.Range(2, size - 3);
            int tempY = Random.Range(2, size - 3);
            // Check if it is too close to the previous point(s)
            for (int i = 0; i < index; i++)
            {
                if (!(Mathf.Abs(xPositions[i] - tempX) < 4 && Mathf.Abs(yPositions[i] - tempY) < 4))
                {
                    if (i == index - 1)
                    {
                        maxValueXs[index] = tempX;
                        maxValueYs[index] = tempY;
                        gotNewPosition = true;
                    }
                }
                else
                {
                    break;
                }
            }
            triedTimes++;
            // In case the program cannot find a suitable position
            if (triedTimes > size * size)
            {
                maxValueXs[index] = tempX;
                maxValueYs[index] = tempY;
                gotNewPosition = true;
            }
        }
    }
}
