using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices = new Vector3[4];
    public int[] verticesInt = new int[4];
    Vector2[] uv;
    int[] triangles;
    public Material mat;
    public int originalNum = -3;
    public int num = -3;
    public int dotDirectionMesh = -1;

    [Serializable]
    public class neighbors
    {
        [SerializeField] public Cell n;
        public int edge;
    }

    [SerializeField] public neighbors[] nigh = new neighbors[4];

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
        OpenMeshCell.Instance.ClickHandlar(this);                        
    }

    public void CreatShape(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3,
        int t0 = 0, int t1 = 1, int t2 = 2, int t3 = 0, int t4 = 2, int t5 = 3)
    {
        vertices[0] = v0;
        vertices[1] = v1;
        vertices[2] = v2;
        vertices[3] = v3;
        uv = new Vector2[]
        {               
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0),
            new Vector2(0, 1)                
        };
        

        triangles = new int[]
        {
            t0,t1,t2,
            t3,t4,t5
        };
        
        UpdateMesh();
    }

    

    public void AddFill(int fill)
    {
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

    public void FillRow(int startEdge, int n, int fill, int dotDir = -1)
    {
        if (n <= 0)
            return;
        bool needFill = false;
        if (num < 0)
        {
            n--;
            needFill = true;
        }
        if (needFill)
        {
            OpenMeshCell.Instance.secondaryS.Push(this);
            AddFill(fill);
            if (fill == -1)
                dotDirectionMesh = dotDir;
        }
        
        nigh[(startEdge + 2) % 4].n.FillRow(nigh[(startEdge + 2) % 4].edge, n, fill, dotDir);       
    }




}
