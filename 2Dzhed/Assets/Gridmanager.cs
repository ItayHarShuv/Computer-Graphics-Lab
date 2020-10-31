using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public GameObject obj;
    public int num = 0;
}

public class Gridmanager : MonoBehaviour
{
    [SerializeField]
    private int rows = 5;
    private int cols;
    private float cellSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();   
    }

    private void GenerateGrid()
    {
        cols = rows;


        Cell[,] gridArr = new Cell[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridArr[row, col] = new Cell();
            }
        }
        GameObject eReferenceCell = new GameObject("Empty Cell");
        GameObject fReferenceCell = new GameObject("Filled Cell");
        Texture2D eCellTex = Resources.Load<Texture2D>("EmptyCell");
        Texture2D fCellTex = Resources.Load<Texture2D>("FilledCell");
        SpriteRenderer eCellRenderer = eReferenceCell.AddComponent<SpriteRenderer>();
        SpriteRenderer fCellRenderer = fReferenceCell.AddComponent<SpriteRenderer>();

        float PPU = 122.0f; //pixel per unit of the original texture
        float cellScale = 1.0f;

        var mainCameraHeight = 2 * Camera.main.orthographicSize;
        var mainCameraWidth = mainCameraHeight * Camera.main.aspect;

        cellScale = mainCameraWidth / rows;
        cellSize = cellSize * cellScale;

        eCellRenderer.sprite = Sprite.Create(eCellTex, new Rect(0.0f, 0.0f, eCellTex.width, eCellTex.height), new Vector2(0.5f, 0.5f), PPU / cellScale);
        fCellRenderer.sprite = Sprite.Create(fCellTex, new Rect(0.0f, 0.0f, fCellTex.width, fCellTex.height), new Vector2(0.5f, 0.5f), PPU/cellScale);

        gridArr[1, 1].num = 1;
        gridArr[2, 2].num = 4;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (gridArr[row, col].num == 0)
                    gridArr[row, col].obj = Instantiate(eReferenceCell, transform);
                else
                {
                    gridArr[row, col].obj = Instantiate(fReferenceCell, transform);
                    //DisplayNum(gridArr[row, col].num);
                }


                gridArr[row, col].obj.AddComponent<BoxCollider2D> ();
                gridArr[row, col].obj.AddComponent<CellThouch>();

                float posX = col * cellSize + 0.74f;
                float posY = row * -cellSize - 1.5f;

                

                gridArr[row, col].obj.transform.position = new Vector2(posX, posY);

            }
        }

        Destroy(eReferenceCell);
        Destroy(fReferenceCell);

        float gridW = cols * cellSize;
        float gridH = rows * cellSize;
        transform.position = new Vector2(-(gridW - cellSize) / 2, (gridH - cellSize) / 2);
    }

    /*void DisplayNum(int num)
    {
        GameObject newGO = new GameObject("myTextGO");
        newGO.transform.SetParent(transform);

        Text myText = newGO.AddComponent<Text>();
        myText.text = "Ta-dah!";
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
