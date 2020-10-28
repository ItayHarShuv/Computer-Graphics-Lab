using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridmanager : MonoBehaviour
{
    [SerializeField]
    private int rows = 8;
    [SerializeField]
    private int cols = 5;
    [SerializeField]
    private float tileSize = 1;


    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();   
    }
    private void GenerateGrid()
    {
        //init Arr
        GameObject[,] gridArr = new GameObject[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                gridArr[i, j] = null;
            }
        }

        

        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Cell3"));
        Sprite.Create(Sprites.Load("Cell3"), new Rect(0.0f, 50.0f, 0.0f, 50.0f), new Vector2(0.5f, 0.5f), 50.0f);
        referenceTile.transform.localScale = new Vector2(0.5f, 0.5f);


        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridArr[row, col] = (GameObject)Instantiate(referenceTile, transform); ;

                gridArr[row, col].AddComponent<BoxCollider2D> ();
                gridArr[row, col].AddComponent<CellThouch>();

                float posX = col * tileSize;
                float posY = row * -tileSize;

                var height = 2 * Camera.main.orthographicSize;
                var width = height * Camera.main.aspect;

                gridArr[row, col].transform.position = new Vector2(posX, posY);

            }
        }

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-(gridW - tileSize) / 2, (gridH - tileSize) / 2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
