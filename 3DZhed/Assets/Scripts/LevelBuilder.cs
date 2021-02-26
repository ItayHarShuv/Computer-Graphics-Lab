using System;
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
                MeshedGrid.Instance.vertices = MeshInformation.Instance.rmesh0000Ver;
                MeshedGrid.Instance.rectangles = MeshInformation.Instance.rmesh0000Tri;
                AddCellNum(80, 4);
                AddCellNum(31, 2);
                AddCellNum(16, -2);
                break;
            case 2:
                MeshedGrid.Instance.vertices = MeshInformation.Instance.TorusVer;
                MeshedGrid.Instance.rectangles = MeshInformation.Instance.TorusTri;
                AddCellNum(80, 4);
                AddCellNum(31, 2);
                break;
            case 3:
                MeshedGrid.Instance.vertices = MeshInformation.Instance.Cube4X4Ver;
                MeshedGrid.Instance.rectangles = MeshInformation.Instance.Cube4X4Tri;
                //AddCellNum(83, 4);
                //AddCellNum(47, 2);
                //AddCellNum(13, 4);
                //AddCellNum(89, 2);
                AddCellNum(17, 4);
                AddCellNum(34, 4);
                break;
            case 4:                
                break;
            case 5:                
                break;
            case 6:                
                break;
            case 7:               
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
        LevelNum.Instance.DisplayLevelNum();
    }

    void AddCellNum(int face, int num)
    {
        try
        {
            MeshedGrid.Instance.cells[face].AddFill(num);
            MeshedGrid.Instance.cells[face].originalNum = num;
        }
        catch {}
    }
}

