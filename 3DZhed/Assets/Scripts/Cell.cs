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

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mat = Resources.Load("EmptyCell", typeof(Material)) as Material;
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

    public void AddFill(int num)
    {
        int x = 140 * (num - 1);
        int y = 180;
        int width = 140;
        int hight = 180;
        uv[0] = ConvertPixelsToUVCoordinates(x + width, y);
        uv[1] = ConvertPixelsToUVCoordinates(x , y );
        uv[2] = ConvertPixelsToUVCoordinates(x + width, y - hight);
        uv[3] = ConvertPixelsToUVCoordinates(x, y - hight);

        mat = Resources.Load("Numbers", typeof(Material)) as Material;

        UpdateMesh();

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

    Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth = 1400, int textureHight = 180)
    {
        return new Vector2((float)x / textureWidth, (float)y / textureHight);
    }
}
