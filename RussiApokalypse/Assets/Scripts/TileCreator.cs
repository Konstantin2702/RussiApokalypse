using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreator : MonoBehaviour
{
    public Camera camera;

    public Transform grid;

    private float defaultHeight;

    private float defaultWidth;
    
    public Vector2 zeroPosition;

    private int tileSize = 33;

    private bool generate;
    
    private Vector2 correction = new Vector2(-8f, 2.5f);

    private GameObject[] tiles;

    //public GameObject currentTile;

    // Start is called before the first frame update
    void Start()
    {
        zeroPosition = (Vector2)camera.transform.position + correction;
        defaultHeight = camera.orthographicSize;
        defaultWidth = camera.orthographicSize * camera.aspect;
        tiles = Resources.LoadAll<GameObject>("Tiles");
        generate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!generate)
        {
            if (Mathf.Abs(camera.transform.position.x - zeroPosition.x) >= tileSize / 2)
            {
                if(camera.transform.position.x > zeroPosition.x)
                {
                    zeroPosition.x += tileSize;
                }
                // else
                // {
                //     zeroPosition.x -= tileSize;
                // }
                generate = true;
            }
            if (Mathf.Abs(camera.transform.position.y - zeroPosition.y) >= tileSize / 2)
            {
                if(camera.transform.position.y > zeroPosition.y)
                {
                    zeroPosition.y += tileSize;
                }
                // else
                // {
                //     zeroPosition.y -= tileSize;
                // }
                generate = true;
            }
        }
        
        if(camera.transform.position.x + correction.x > zeroPosition.x + defaultHeight - 1)
        {
            if (generate) 
            {
                foreach( var tile in tiles )
                {
                    zeroPosition.x += tileSize;
                    GameObject newTile = Instantiate(tile, zeroPosition, Quaternion.identity) as GameObject;
                    newTile.transform.SetParent(grid);
                }
                generate = false;
            }
        }
        if(camera.transform.position.y + correction.y > zeroPosition.y + defaultWidth - 1)
        {
            if(generate)
            {
                foreach( var tile in tiles )
                {
                    zeroPosition.y += tileSize;
                    GameObject newTile = Instantiate(tile, zeroPosition, Quaternion.identity) as GameObject;
                    newTile.transform.SetParent(grid);
                }
                generate = false;
            }
            
        }
    }
}
