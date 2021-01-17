using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileValue
{
    MAX,
    HALF,
    QUARTER,
    MIN,
}

public class Tile : MonoBehaviour
{
    public TileValue Value = TileValue.MIN;
    public bool isHidden = true;
    public int[] positionIndex = new int[2] {-1, -1};
    private TileManager tileManager;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    void OnMouseDown()
    {
        if (tileManager.isExtractMode == true)
        {
            if (GameManager.Instance().extractionsRemaining > 1)
            {
                ExtractResource();
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/OreMin");
                isHidden = false;
                tileManager.resourceCounter.text = "Collected resources: " + GameManager.Instance().collectedResources;
                GameManager.Instance().extractionsRemaining--;
                tileManager.DegradeTiles(positionIndex);
            }
            else if (GameManager.Instance().extractionsRemaining == 1)
            {
                ExtractResource();
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/OreMin");
                isHidden = false;
                tileManager.toggleButtonText.text = "Restart";
                tileManager.promptMessage.text = "GAME OVER";
                tileManager.resourceCounter.text = "Total resources: " + GameManager.Instance().collectedResources;
                GameManager.Instance().extractionsRemaining--;
                tileManager.DegradeTiles(positionIndex);
            }
            else
            {
                tileManager.promptMessage.text = "GAME OVER";
                tileManager.resourceCounter.text = "Total resources: " + GameManager.Instance().collectedResources;
            }
        }
        else
        {
            if (GameManager.Instance().scansRemaining > 0)
            {
                tileManager.DisplayTiles(positionIndex);

                GameManager.Instance().scansRemaining--;
            }
        }
        tileManager.messageBar.text = "[Scan: " + GameManager.Instance().scansRemaining + "/6]   [Extract: " + GameManager.Instance().extractionsRemaining + "/3]";
    }

    void ExtractResource()
    {
        switch (Value)
        {
            case TileValue.MIN:
                GameManager.Instance().collectedResources += 500;
                break;
            case TileValue.QUARTER:
                GameManager.Instance().collectedResources += 1000;
                Value = TileValue.MIN;
                break;
            case TileValue.HALF:
                GameManager.Instance().collectedResources += 2000;
                Value = TileValue.MIN;
                break;
            case TileValue.MAX:
                GameManager.Instance().collectedResources += 4000;
                Value = TileValue.MIN;
                break;
        }
    }

    public void DisplayTile()
    {
        switch (Value)
        {
            case TileValue.MIN:
                var minTileSprite = Resources.Load<Sprite>("Sprites/OreMin");
                spriteRenderer.sprite = minTileSprite;
                break;
            case TileValue.QUARTER:
                var quarterTileSprite = Resources.Load<Sprite>("Sprites/OreQuarter");
                spriteRenderer.sprite = quarterTileSprite;
                break;
            case TileValue.HALF:
                var halfTileSprite = Resources.Load<Sprite>("Sprites/OreHalf");
                spriteRenderer.sprite = halfTileSprite;
                break;
            case TileValue.MAX:
                var maxTileSprite = Resources.Load<Sprite>("Sprites/OreMax");
                spriteRenderer.sprite = maxTileSprite;
                break;
        }
        isHidden = false;
    }

    public void Degrade()
    {
        switch (Value)
        {
            case TileValue.HALF:
                Value = TileValue.QUARTER;
                break;
            case TileValue.MAX:
                Value = TileValue.HALF;
                break;
            default:
                Value = TileValue.MIN;
                break;
        }
    }
}