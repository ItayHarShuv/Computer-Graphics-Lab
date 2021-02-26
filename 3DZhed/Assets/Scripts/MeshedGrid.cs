using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshedGrid : MonoBehaviour
{
    public static MeshedGrid Instance { get; private set; }

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

    public Vector3[] vertices;
    public int[] rectangles;
    public int currentLevel;
    public Cell[] cells = null;

    void Start()
    {
        currentLevel = 1;
        BuildGrid(currentLevel); 
    }
    public void BuildGrid(int level)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        LevelBuilder.Instance.BuildLevel(level);
        LinkedList<int>[] verToSq = new LinkedList<int>[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            verToSq[i] = new LinkedList<int>();
        }
        cells = new Cell[rectangles.Length/4];

        for (int i = 0; i < rectangles.Length; i += 4)
        {           
            GameObject cell1 = (GameObject)Instantiate(Resources.Load("Cell"), transform);
            Cell temp = cell1.GetComponent<Cell>();
            temp.name = "Cell(" + i/4 + ")";
            temp.CreatShape(vertices[rectangles[i]], vertices[rectangles[i + 1]], 
                            vertices[rectangles[i + 2]], vertices[rectangles[i + 3]]);
            for (int j = 0; j < 4; j++)
            {
                verToSq[rectangles[i + j]].AddLast(i / 4);
                temp.verticesInt[j] = rectangles[i + j];
            }           
            cells[i / 4] = temp;
        }
        for (int i = 0; i < cells.Length; i++)
        {           
            for (int v = 0; v < 4; v++)
            {
                int neighbor = equalInLists(verToSq[cells[i].verticesInt[v]], verToSq[cells[i].verticesInt[(v + 1) % 4]], i);
                    
                for (int k = 0; k < 4; k++)
                {
                    if ((cells[neighbor].verticesInt[k] == cells[i].verticesInt[v] &&
                        cells[neighbor].verticesInt[(k + 1) % 4] == cells[i].verticesInt[(v + 1) % 4]) ||
                        (cells[neighbor].verticesInt[k] == cells[i].verticesInt[(v + 1) % 4] &&
                        cells[neighbor].verticesInt[(k + 1) % 4] == cells[i].verticesInt[v]))
                    {
                        cells[i].nigh[v].edge = k;
                    }
                }
                                      
                cells[i].nigh[v].n = cells[neighbor];

            }                                    
        }
        LevelBuilder.Instance.BuildLevel(level);
    }
    int equalInLists(LinkedList<int> l1, LinkedList<int> l2, int org)
    {
        LinkedListNode<int> n = l1.First;
        while (n != null)
        {
            if (l2.Contains(n.Value) && n.Value != org)
            {
                return n.Value;
            }
            n = n.Next;
        }
        return -1;
    }
}