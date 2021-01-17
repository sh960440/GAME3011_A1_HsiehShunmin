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

                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Min");
                tileManager.resourceCounter.text = "Collected resources: " + GameManager.Instance().collectedResources;
                GameManager.Instance().extractionsRemaining--;
            }
            else if (GameManager.Instance().extractionsRemaining == 1)
            {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Min");
                tileManager.messageBar.text = "GAME OVER";
                tileManager.resourceCounter.text = "Total resources: " + GameManager.Instance().collectedResources;
                GameManager.Instance().extractionsRemaining--;
            }
            else
            {
                tileManager.messageBar.text = "GAME OVER";
                tileManager.resourceCounter.text = "Total resources: " + GameManager.Instance().collectedResources;
            }
        }
        else
        {
            if (GameManager.Instance().scansRemaining > 0)
            {
                switch (Value)
                {
                    case TileValue.MIN:
                        var minTileSprite = Resources.Load<Sprite>("Sprites/Min");
                        spriteRenderer.sprite = minTileSprite;
                        break;
                    case TileValue.QUARTER:
                        var quarterTileSprite = Resources.Load<Sprite>("Sprites/Quarter");
                        spriteRenderer.sprite = quarterTileSprite;
                        break;
                    case TileValue.HALF:
                        var halfTileSprite = Resources.Load<Sprite>("Sprites/Half");
                        spriteRenderer.sprite = halfTileSprite;
                        break;
                    case TileValue.MAX:
                        var maxTileSprite = Resources.Load<Sprite>("Sprites/Max");
                        spriteRenderer.sprite = maxTileSprite;
                        break;
                }

                GameManager.Instance().scansRemaining--;
            }
        }
    }
}