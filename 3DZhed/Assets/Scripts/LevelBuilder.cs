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
    public int maxLevel = 10;

    public void BuildLevel(int level)
    {
        //(num, face, row, col)
        switch (level)
        {
            case 1:
                Grid.Instance.rows = 2;
                AddCellNum(1, 2, 1, 0);
                AddCellNum(-2, 2, 0, 0);
                break;
            case 2:
                Grid.Instance.rows = 2;
                AddCellNum(2, 2, 1, 0);
                AddCellNum(-2, 3, 1, 0);
                break;
            case 3:
                Grid.Instance.rows = 3;
                AddCellNum(4, 2, 1, 1);
                AddCellNum(1, 5, 1, 2);
                AddCellNum(-2, 0, 0, 1);
                break;
            case 4:
                Grid.Instance.rows = 4;
                AddCellNum(-2, 5, 2, 1);
                AddCellNum(2, 2, 0, 2);
                AddCellNum(1, 2, 1, 3);
                AddCellNum(1, 3, 1, 0);
                AddCellNum(2, 3, 2, 2);
                AddCellNum(1, 0, 0, 0);
                AddCellNum(2, 0, 1, 1);
                break;
            case 5:
                Grid.Instance.rows = 4;
                AddCellNum(-2, 5, 2, 2);
                AddCellNum(1, 5, 1, 1);
                AddCellNum(2, 5, 1, 0);
                AddCellNum(1, 3, 0, 3);
                AddCellNum(2, 3, 3, 0);
                AddCellNum(5, 0, 1, 0);
                AddCellNum(3, 1, 0, 2);
                AddCellNum(4, 4, 2, 2);
                AddCellNum(1, 4, 2, 0);
                break;
            case 6:
                Grid.Instance.rows = 4;
                AddCellNum(-2, 0, 2, 2);
                AddCellNum(1, 3, 1, 2);
                AddCellNum(1, 2, 0, 3);
                AddCellNum(1, 2, 0, 2);
                AddCellNum(1, 2, 1, 1);
                AddCellNum(3, 2, 2, 1);
                AddCellNum(1, 1, 1, 2);
                AddCellNum(2, 1, 0, 2);
                AddCellNum(1, 5, 2, 2);
                AddCellNum(2, 5, 2, 0);
                AddCellNum(3, 5, 3, 1);
                AddCellNum(2, 4, 1, 1);
                AddCellNum(2, 4, 2, 0);
                AddCellNum(3, 4, 3, 2);
                break;
            case 7:
                Grid.Instance.rows = 6;
                AddCellNum(-2, 1, 4, 2);
                AddCellNum(1, 1, 1, 4);
                AddCellNum(1, 1, 2, 4);
                AddCellNum(2, 1, 0, 5);
                AddCellNum(3, 1, 5, 3);
                AddCellNum(5, 2, 3, 4);
                AddCellNum(1, 2, 1, 3);
                AddCellNum(1, 3, 0, 1);
                AddCellNum(1, 3, 2, 2);
                AddCellNum(1, 3, 3, 3);
                AddCellNum(2, 3, 4, 4);
                AddCellNum(3, 0, 0, 3);
                AddCellNum(5, 0, 1, 0);
                AddCellNum(1, 0, 3, 1);
                AddCellNum(1, 0, 4, 3);
                AddCellNum(3, 0, 2, 2);
                AddCellNum(5, 5, 3, 0);
                AddCellNum(1, 4, 1, 5);
                AddCellNum(2, 4, 4, 3);
                AddCellNum(3, 4, 4, 4);
                AddCellNum(2, 4, 3, 1);
                AddCellNum(3, 4, 2, 2);
                AddCellNum(1, 4, 0, 1);
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            default:              
                break;
        }
    }

    void AddCellNum(int num, int face, int row, int col)
    {
        if (Grid.Instance.cells != null)
        {
            if(num == -2)
            {
                Grid.Instance.winCell = new Vector3(face, row, col);
            }
            Grid.Instance.cells[face, row, col].GetComponent<Cell>().AddFill(num);
            Grid.Instance.cells[face, row, col].GetComponent<Cell>().originalNum = num;
            Grid.Instance.cells[face, row, col].GetComponent<Cell>().num = num;
        }
    }
}

