using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public int Value = 1;
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
        Debug.Log("Clicked!!");
        var tileSprite = Resources.Load<Sprite>("Sprites/Max");
        spriteRenderer.sprite = tileSprite;
    }
}
