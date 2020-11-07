using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ButtonHandler : MonoBehaviour
{
    private Stack dots = new Stack();

    public void SpreadCell()
    {
        if(OpenCell.Instance.rowTargetCell == gameObject.GetComponent<CellModifier>().row &&
           OpenCell.Instance.colTargetCell == gameObject.GetComponent<CellModifier>().col &&
           OpenCell.Instance.unfolded)
        {
            ClearDots();           
        }           
        else if(OpenCell.Instance.unfolded)
        {
            GridBuild.Instance.cell[OpenCell.Instance.rowTargetCell, OpenCell.Instance.colTargetCell].GetComponent<ButtonHandler>().ClearDots();
            OpenCell.Instance.UnfoldCell(GetComponent<CellModifier>().row, GetComponent<CellModifier>().col);
        }
        else
        {
            PrintDot(gameObject.GetComponent<CellModifier>(), gameObject.GetComponent<CellModifier>().num);
            
        }
        return;
    }

    void PrintDot(CellModifier cell, int num)
    {
        if (num <= 0)
            return;
        int row = gameObject.GetComponent<CellModifier>().row;
        int col = gameObject.GetComponent<CellModifier>().col;

        int upFilled = 0;
        int rightFilled = 0;
        int downFilled = 0;
        int leftFilled = 0;

        for (int i = 1; i <= num; i++)
        {

            try
            {
                while (GridBuild.Instance.cell[row - i - upFilled, col].GetComponent<CellModifier>().filled)
                {
                    upFilled++;
                }
                GameObject dot = (GameObject)Instantiate(Resources.Load("Circle"), GridBuild.Instance.cell[row - i - upFilled, col].transform);
                dots.Push(dot);
            }
            catch (IndexOutOfRangeException){}

            try
            {
                while (GridBuild.Instance.cell[row , col + i + rightFilled].GetComponent<CellModifier>().filled)
                {
                    rightFilled++;
                }
                GameObject dot = (GameObject)Instantiate(Resources.Load("Circle"), GridBuild.Instance.cell[row, col + i + rightFilled].transform);
                dots.Push(dot);
            }
            catch (IndexOutOfRangeException) {}

            try
            {
                while (GridBuild.Instance.cell[row + i + downFilled, col].GetComponent<CellModifier>().filled)
                {
                    downFilled++;
                }
                GameObject dot = (GameObject)Instantiate(Resources.Load("Circle"), GridBuild.Instance.cell[row + i + downFilled, col].transform);
                dots.Push(dot);
            }
            catch (IndexOutOfRangeException) {}

            try
            {
                while (GridBuild.Instance.cell[row, col - i - leftFilled].GetComponent<CellModifier>().filled)
                {
                    leftFilled++;
                }
                GameObject dot = (GameObject)Instantiate(Resources.Load("Circle"), GridBuild.Instance.cell[row, col - i - leftFilled].transform);
                dots.Push(dot);
            }
            catch (IndexOutOfRangeException) {}

        }
        OpenCell.Instance.rowTargetCell = row;
        OpenCell.Instance.colTargetCell = col;
        OpenCell.Instance.unfolded = true;

    }

    public void ClearDots()
    {

        while (dots.Count != 0)
        {
            Destroy((GameObject)dots.Pop());
        }
        OpenCell.Instance.unfolded = false;
        
        return;  
    }
}
