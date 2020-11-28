using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int rows;
    public Cell[,,] cells;
    float cellSize;
    public int currentLevel;

    void Start()
    {
        currentLevel = 2;
        SetGrid(currentLevel);
    }

    public void SetGrid(int level)
    {
        currentLevel = level;
        LevelNum.Instance.DisplayLevelNum();
        OpenCell.Instance.ClearAll();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        cells = null;
        LevelBuilder.Instance.BuildLevel(level);
        

        cellSize = 100 / rows;
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
        LevelBuilder.Instance.BuildLevel(level);
        RenameCells();
        ConnectCells();
    }

    void RenameCells()
    {
        for (int face = 0; face < 6; face++)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < rows; col++)
                {
                    cells[face, row, col].Rename(face + 1, row, col);
                }
            }
        }
    }

    void ConnectCells()
    {
        //face 0
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                //Around the X axis
                try
                {
                    cells[0, row, col].ccwx = cells[0, row - 1, col];
                }
                catch
                {
                    cells[0, row, col].ccwx = cells[5, rows - 1, col];
                }

                try
                {
                    cells[0, row, col].cwx = cells[0, row + 1, col];
                }
                catch
                {
                    cells[0, row, col].cwx = cells[4, 0, col];
                }

                //Around the Y axis
                try
                {
                    cells[0, row, col].ccwy = cells[0, row, col + 1];
                }
                catch
                {
                    cells[0, row, col].ccwy = cells[1, row, 0];
                }

                try
                {
                    cells[0, row, col].cwy = cells[0, row, col - 1];
                }
                catch
                {
                    cells[0, row, col].cwy = cells[3, row, rows-1];
                }
            }
        }

        //face 1
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                //Around the Z axis
                try
                {
                    cells[1, row, col].ccwz = cells[1, row - 1, col];
                }
                catch
                {
                    cells[1, row, col].ccwz = cells[5, rows - 1 - col, rows - 1];
                }

                try
                {
                    cells[1, row, col].cwz = cells[1, row + 1, col];
                }
                catch
                {
                    cells[1, row, col].cwz = cells[4, col, rows - 1];
                }

                //Around the Y axis
                try
                {
                    cells[1, row, col].ccwy = cells[1, row, col + 1];
                }
                catch
                {
                    cells[1, row, col].ccwy = cells[2, row, 0];
                }

                try
                {
                    cells[1, row, col].cwy = cells[1, row, col - 1];
                }
                catch
                {
                    cells[1, row, col].cwy = cells[0, row, rows - 1];
                }
            }
        }

        //face 2
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                //Around the X axis
                try
                {
                    cells[2, row, col].ccwx = cells[2, row + 1, col];
                }
                catch
                {
                    cells[2, row, col].ccwx = cells[4, rows - 1 , rows - 1 - col];
                }

                try
                {
                    cells[2, row, col].cwx = cells[2, row - 1, col];
                }
                catch
                {
                    cells[2, row, col].cwx = cells[5, 0, rows - 1 - col];
                }

                //Around the Y axis
                try
                {
                    cells[2, row, col].ccwy = cells[2, row, col + 1];
                }
                catch
                {
                    cells[2, row, col].ccwy = cells[3, row, 0];
                }

                try
                {
                    cells[2, row, col].cwy = cells[2, row, col - 1];
                }
                catch
                {
                    cells[2, row, col].cwy = cells[1, row, rows - 1];
                }
            }
        }

        //face 3
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                //Around the Z axis
                try
                {
                    cells[3, row, col].ccwz = cells[3, row + 1, col];
                }
                catch
                {
                    cells[3, row, col].ccwz = cells[4, rows - 1 - col, 0];
                    
                }

                try
                {
                    cells[3, row, col].cwz = cells[3, row - 1, col];
                }
                catch
                {
                    cells[3, row, col].cwz = cells[5, col, 0];
                }

                //Around the Y axis
                try
                {
                    cells[3, row, col].ccwy = cells[3, row, col + 1];
                }
                catch
                {
                    cells[3, row, col].ccwy = cells[0, row, 0];
                }

                try
                {
                    cells[3, row, col].cwy = cells[3, row, col - 1];
                }
                catch
                {
                    cells[3, row, col].cwy = cells[2, row, rows - 1];
                }
            }
        }

        //face 4
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                //Around the X axis
                try
                {
                    cells[4, row, col].ccwx = cells[4, row - 1, col];
                }
                catch
                {
                    cells[4, row, col].ccwx = cells[0, rows - 1, col];
                }

                try
                {
                    cells[4, row, col].cwx = cells[4, row + 1, col];
                }
                catch
                {
                    cells[4, row, col].cwx = cells[2, rows - 1, rows - 1 - col];
                }

                //Around the Z axis
                try
                {
                    cells[4, row, col].ccwz = cells[4, row, col + 1];
                }
                catch
                {
                    cells[4, row, col].ccwz = cells[1, rows - 1, row];
                }

                try
                {
                    cells[4, row, col].cwz = cells[4, row, col - 1];
                }
                catch
                {
                    cells[4, row, col].cwz = cells[3, rows - 1, rows - 1 - row];
                }
            }
        }

        //face 5
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                //Around the X axis
                try
                {
                    cells[5, row, col].ccwx = cells[5, row - 1, col];
                }
                catch
                {
                    cells[5, row, col].ccwx = cells[2, 0, rows - 1 - col];
                }

                try
                {
                    cells[5, row, col].cwx = cells[5, row + 1, col];
                }
                catch
                {
                    cells[5, row, col].cwx = cells[0, 0, col];
                }

                //Around the Z axis
                try
                {
                    cells[5, row, col].ccwz = cells[5, row, col - 1];
                }
                catch
                {
                    cells[5, row, col].ccwz = cells[3, 0, row];
                }

                try
                {
                    cells[5, row, col].cwz = cells[5, row, col + 1];
                }
                catch
                {
                    cells[5, row, col].cwz = cells[1, 0, rows - 1 - row];
                }
            }
        }
    }
}