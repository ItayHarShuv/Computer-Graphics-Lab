using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Stack<Stack<Cell>> mainS = new Stack<Stack<Cell>>();
    public Stack<Cell> secondaryS = new Stack<Cell>();
    Cell unfolded = null;

    public void ClickHandlar(Cell cur)
    {
        if(cur.num > 0 && unfolded == null)
        {
            //Open dots
            cur.UnfoldDots();
            unfolded = cur;
        }
        else if(unfolded == cur)
        {
            //Clear dots
            ClearStack(secondaryS);
            unfolded = null;
        }
        else if(secondaryS.Contains(cur))
        {
            //Open cell
            Dir openDir = cur.dotDirection;
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
            temp.dotDirection = Dir.defult;
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
        ClearStack(secondaryS);
        Stack<Cell> temp = mainS.Pop();
        ClearStack(temp);
    }

    public void Restart()
    {
        while(mainS.Count != 0)
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
