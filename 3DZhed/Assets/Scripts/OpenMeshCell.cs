using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMeshCell : MonoBehaviour
{
    public static OpenMeshCell Instance { get; private set; }

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

    public Stack<Stack<Cell>> mainS = new Stack<Stack<Cell>>();
    public Stack<Cell> secondaryS = new Stack<Cell>();
    Cell unfolded = null;

    public void ClickHandlar(Cell cur)
    {
        if (cur.num > 0 && unfolded == null)
        {
            //Open dots
            cur.FillRow(0, cur.num, -1, 0);
            cur.FillRow(1, cur.num, -1, 1);
            cur.FillRow(2, cur.num, -1, 2);
            cur.FillRow(3, cur.num, -1, 3);
            unfolded = cur;
        }
        else if (unfolded == cur)
        {
            //Clear dots
            ClearStack(secondaryS);
            unfolded = null;
        }
        else if (secondaryS.Contains(cur))
        {
            //Open cell
            int openDir = cur.dotDirectionMesh;
            ClearStack(secondaryS);

            unfolded.FillRow(openDir, unfolded.num, 0);
            unfolded.AddFill(0);
            secondaryS.Push(unfolded);

            unfolded = null;
            Stack<Cell> temp = CloneSecondaryS();
            ClearStack(secondaryS);
            mainS.Push(temp);

        }

    }

    void ClearStack(Stack<Cell> s)
    {
        while (s.Count != 0)
        {
            Cell temp = s.Peek();
            temp.AddFill(s.Pop().originalNum);
            temp.dotDirectionMesh = -1;
        }
    }

    Stack<Cell> CloneSecondaryS()
    {
        Stack<Cell> clone = new Stack<Cell>();
        while (secondaryS.Count != 0)
        {
            Cell temp = secondaryS.Pop();
            clone.Push(temp);
        }
        return clone;
    }

    public void Undo()
    {
        try
        {
            ClearStack(secondaryS);
            Stack<Cell> temp = mainS.Pop();
            ClearStack(temp);
        }
        catch { }       
    }

    public void Restart()
    {
        while (mainS.Count != 0)
        {
            Undo();
        }
    }

    public void ClearAll()
    {
        secondaryS.Clear();
        mainS.Clear();
    }

}
