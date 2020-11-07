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
        switch(level)
        {
            case 6:
                GridBuild.Instance.rows = 8;
                AddCellNum(2, 4, 2);
                AddCellNum(2, 6, 3);
                AddCellNum(3, 5, 5);
                AddCellNum(3, 1, 6);
                AddCellNum(-1, 1, 2);
                break;
            default:
                break;
        }
    }

    void AddCellNum(int num, int row, int col)
    {
        if(GridBuild.Instance.cell != null)
        {
            GridBuild.Instance.cell[row, col].GetComponent<CellModifier>().num = num;
        }        
    }
}
