using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager
{
    private static GameManager instance = null;
    private GameManager() { }
    public static GameManager Instance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }
    
    public int collectedResources = 0;
    public int scansRemaining = 6;
    public int extractionsRemaining = 3;
}
