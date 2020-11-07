using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenCell : MonoBehaviour
{
    public static OpenCell Instance { get; private set; }    

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

    public bool unfolded = false;
    public int rowTargetCell = 0;
    public int colTargetCell = 0;

    public void UnfoldCell(int rowClickedtCell, int colClickedtCell)
    {
        CellModifier targetCell = GridBuild.Instance.cell[rowTargetCell, colTargetCell].GetComponent<CellModifier>();
        int num = targetCell.num;

        int row = rowTargetCell;
        int col = colTargetCell;

        int upFilled = 0;
        int rightFilled = 0;
        int downFilled = 0;
        int leftFilled = 0;

        for (int i = 1; i <= num; i++)
        {
            GridBuild.Instance.cell[row, col].GetComponent<CellModifier>().num = 0;
            GridBuild.Instance.cell[row, col].GetComponent<CellModifier>().PrintNum();
            try
            {
                if (GetDirection(rowClickedtCell, colClickedtCell) == "up")
                {
                    while (GridBuild.Instance.cell[row - i - upFilled, col].GetComponent<CellModifier>().filled)
                    {
                        upFilled++;
                    }
                    FillCell(GridBuild.Instance.cell[row - i - upFilled, col].GetComponent<CellModifier>());
                }
                if (GetDirection(rowClickedtCell, colClickedtCell) == "right")
                {
                    while (GridBuild.Instance.cell[row, col + i + rightFilled].GetComponent<CellModifier>().filled)
                    {
                        rightFilled++;
                    }
                    FillCell(GridBuild.Instance.cell[row, col + i + rightFilled].GetComponent<CellModifier>());
                }
                if (GetDirection(rowClickedtCell, colClickedtCell) == "down")
                {
                    while (GridBuild.Instance.cell[row + i + downFilled, col].GetComponent<CellModifier>().filled)
                    {
                        downFilled++;
                    }
                    FillCell(GridBuild.Instance.cell[row + i + downFilled, col].GetComponent<CellModifier>());
                }
                if (GetDirection(rowClickedtCell, colClickedtCell) == "left")
                {
                    while (GridBuild.Instance.cell[row, col - i - leftFilled].GetComponent<CellModifier>().filled)
                    {
                        leftFilled++;
                    }
                    FillCell(GridBuild.Instance.cell[row, col - i - leftFilled].GetComponent<CellModifier>());
                }
            }
            catch (IndexOutOfRangeException) { }
        }
    }

    string GetDirection(int rowClickedtCell, int colClickedtCell)
    {
        if(Math.Abs(rowClickedtCell- rowTargetCell) < Math.Abs(colClickedtCell - colTargetCell))
        {
            if (colClickedtCell < colTargetCell)
                return "left";
            else
                return "right";
        }
        else
        {
            if (rowClickedtCell < rowTargetCell)
                return "up";
            else
                return "down";
        }
    }

    void FillCell(CellModifier cell)
    {
        cell.filled = true;
        cell.SetColor();
        return;
    }


}
