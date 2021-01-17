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

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
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
    }
}
