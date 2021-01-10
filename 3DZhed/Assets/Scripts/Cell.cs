using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{


    Mesh mesh;
    Vector3[] vertices = new Vector3[4];
    Vector2[] uv;
    int[] triangles;
    public Material mat;
    public int originalNum = -3;
    public int num = -3;
    public Dir dotDirection;

    public Cell cwx;
    public Cell ccwx;
    public Cell cwy;
    public Cell ccwy;
    public Cell cwz;
    public Cell ccwz;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;       
        mat = Resources.Load("EmptyCell", typeof(Material)) as Material;
    }

    void Start()
    {
        gameObject.AddComponent<MeshCollider>();
    }

    void OnMouseDown()
    {
        OpenCell.Instance.ClickHandlar(this);
    }

    public void CreatShape(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        vertices[0] = v0;
        vertices[1] = v1;
        vertices[2] = v2;
        vertices[3] = v3;

        uv = new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2(1, 1),

            new Vector2(0, 0),
            new Vector2(1, 0)


        };

        triangles = new int[]
        {
            0, 2, 1,
            2, 3 ,1
        };
        
        UpdateMesh();
    }

    public Cell CloneAndRotate(int deg, char dir)
    {

        GameObject cell1 = (GameObject)Instantiate(Resources.Load("Cell"), transform);
        Cell temp = cell1.GetComponent<Cell>();
        temp.CreatShape(vertices[0], vertices[1], vertices[2], vertices[3]);
        for (int i = 0; i < 4; i++)
        {
            if (dir == 'x')
                temp.vertices[i] = Quaternion.Euler(deg, 0, 0) * vertices[i];
            if (dir == 'y')
                temp.vertices[i] = Quaternion.Euler(0, deg, 0) * vertices[i];

        }
        temp.UpdateMesh();
        return temp;
    }

    

    public void AddFill(int fill)
    {
        uv[0] = new Vector2(1, 1);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(0, 0);
        switch(fill)
        {
            case -1:
                //DotCell
                mat = Resources.Load("DotCell", typeof(Material)) as Material;
                break;
            case -2:
                //WinCell
                mat = Resources.Load("WinCell", typeof(Material)) as Material;
                break;
            case -3:
                //EmptyCell
                mat = Resources.Load("EmptyCell", typeof(Material)) as Material;
                break;
            default:
                //Any other number
                mat = Resources.Load(fill.ToString(), typeof(Material)) as Material;
                break;

        }
        if(num == -2 && fill == 0)
        {
            //win event
            num = fill;
            UpdateMesh();
            ChangeLevel.Instance.ChangeLevelMet(ChangeLevel.AddSubLevel.add);
        }
        else if(num == -2)
        {
            num = fill;
        }
        else
        {
            num = fill;
            UpdateMesh();
        }
            
    }

    void UpdateMesh()
    {

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;


        mesh.RecalculateNormals();
        GetComponent<MeshRenderer>().material = mat;
    }

    public void Rename(int face, int row, int col)
    {
        gameObject.name = "Cell(" + face + ',' + row + ',' + col + ')';
    }

    public void UnfoldDots()
    {
        try
        {               
            cwx.FillRow(Dir.cwx, num, -1);
            ccwx.FillRow(Dir.ccwx, num, -1);                                
        }
        catch(NullReferenceException){ }
        try
        {
            cwy.FillRow(Dir.cwy, num, -1);
            ccwy.FillRow(Dir.ccwy, num, -1);
        }
        catch (NullReferenceException) { }
        try
        {
            cwz.FillRow(Dir.cwz, num, -1);
            ccwz.FillRow(Dir.ccwz, num, -1);
        }
        catch (NullReferenceException) { }
        
    }

    public void FillRow(Dir dir, int n, int fill)
    {
        
        if (n <= 0)
            return;
        bool needFill = false;
        if (num < 0)
        {
            n--;
            needFill = true;
        }           
        switch (dir)
        {
            case Dir.cwx:
                cwx.FillRow(dir, n, fill);
                break;
            case Dir.ccwx:
                ccwx.FillRow(dir, n, fill);
                break;
            case Dir.cwy:
                cwy.FillRow(dir, n, fill);
                break;
            case Dir.ccwy:
                ccwy.FillRow(dir, n, fill);
                break;
            case Dir.cwz:
                cwz.FillRow(dir, n, fill);
                break;
            case Dir.ccwz:
                ccwz.FillRow(dir, n, fill);
                break;
            default:
                return;
        }
        if(needFill)
        {
            if (fill == -1)
                dotDirection = dir;
            OpenCell.Instance.secondaryS.Push(this);
            AddFill(fill);
        }        
    }

    

}

public enum Dir
{
    defult,
    cwx,
    ccwx,
    cwy,
    ccwy,
    cwz,
    ccwz
}
