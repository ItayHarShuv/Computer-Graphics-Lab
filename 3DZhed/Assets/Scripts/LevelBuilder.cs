using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public static LevelBuilder Instance { get; private set; }

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

    public void BuildLevel(int level)
    {
        switch (level)
        {
            case 1:
                Grid.Instance.rows = 2;
                AddCellNum(1, 0, 1, 0);
                AddCellNum(2, 1, 1, 0);
                break;
            case 2:
                Grid.Instance.rows = 10;
                AddCellNum(1, 0, 1, 0);
                AddCellNum(2, 1, 1, 0);
                AddCellNum(3, 2, 1, 0);
                AddCellNum(4, 3, 2, 1);
                AddCellNum(5, 4, 1, 0);
                AddCellNum(6, 5, 1, 0);
                break;
            default:              
                break;
        }
    }

    void AddCellNum(int num, int face, int row, int col)
    {
        if (Grid.Instance.cells != null)
        {
            Grid.Instance.cells[face, row, col].GetComponent<Cell>().AddFill(num);
            Grid.Instance.cells[face, row, col].GetComponent<Cell>().originalNum = num;
            Grid.Instance.cells[face, row, col].GetComponent<Cell>().num = num;
        }
    }
}

