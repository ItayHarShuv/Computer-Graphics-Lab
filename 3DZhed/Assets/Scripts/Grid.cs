using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    int rows = 4;
    Cell[,,] cells;
    float cellSize = 20;

    void Start()
    {
        cellSize = 120 / rows;
        cells = new Cell[6, rows, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                GameObject cell = (GameObject)Instantiate(Resources.Load("Cell"), transform);
                cells[0, row, col] = cell.GetComponent<Cell>();
                float corner = cellSize * rows / 2;
                cell.GetComponent<Cell>().CreatShape(new Vector3(corner - col * cellSize - cellSize, corner - row * cellSize, corner),
                                                     new Vector3(corner - col * cellSize, corner - row * cellSize, corner),
                                                     new Vector3(corner - col * cellSize - cellSize, corner - row * cellSize - cellSize, corner),
                                                     new Vector3(corner - col * cellSize, corner - row * cellSize - cellSize, corner));
                for (int i = 1; i <= 3; i++)
                {                   
                    cells[i, row, col] = cells[0, row, col].GetComponent<Cell>().CloneAndRotate(-90 * i, 'y');
                }
                int deg = 90;
                for (int i = 4; i <= 5; i++)
                {
                    cells[i, row, col] = cells[0, row, col].GetComponent<Cell>().CloneAndRotate(deg, 'x');            
                    deg = -deg;
                }
            }
        }
        cells[0, 1, 0].GetComponent<Cell>().AddFill(1);
        cells[1, 1, 0].GetComponent<Cell>().AddFill(2);
        cells[2, 1, 0].GetComponent<Cell>().AddFill(3);
        cells[3, 1, 0].GetComponent<Cell>().AddFill(4);
        cells[4, 1, 0].GetComponent<Cell>().AddFill(5);
        cells[5, 1, 0].GetComponent<Cell>().AddFill(6);
    }
}