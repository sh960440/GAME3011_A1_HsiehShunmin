using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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


    public TMP_Text toggleButtonText;
    public TMP_Text messageBar;
    public TMP_Text resourceCounter;
    public bool isExtractMode = true;


    void Start()
    {
        tiles = new GameObject[size,size];
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                Vector3 spawnPosition = new Vector3(startXPosition + i * 0.5f, startYPosition - j * 0.5f, transform.position.z);
                tiles[i,j] = Instantiate(tilePrafab, spawnPosition, transform.rotation);
                tiles[i,j].GetComponent<Tile>().positionIndex = new int[2] {i, j};
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

    public void ModeSwitching()
    {
        isExtractMode = isExtractMode == true ? false : true;
        toggleButtonText.text = isExtractMode == true ? "Extract Mode" : "Scan Mode";
        if (GameManager.Instance().extractionsRemaining > 0)
        {
            messageBar.text = isExtractMode == true ? "Click a tile to gather resources" : "Click a tile to scan";
        }        
        else
        {
            GameManager.Instance().collectedResources = 0;
            GameManager.Instance().scansRemaining = 6;
            GameManager.Instance().extractionsRemaining = 3;
            SceneManager.LoadScene("TileGame");
        }
    }

    public void DisplayTiles(int[] center)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                tiles[center[0] + i, center[1] + j].GetComponent<Tile>().DisplayTile();
            }
        }
    }

    public void DegradeTiles(int[] center)
    {
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                tiles[center[0] + i, center[1] + j].GetComponent<Tile>().Degrade();
                if (tiles[center[0] + i, center[1] + j].GetComponent<Tile>().isHidden == false)
                    tiles[center[0] + i, center[1] + j].GetComponent<Tile>().DisplayTile();
            }
        }
    }
}
