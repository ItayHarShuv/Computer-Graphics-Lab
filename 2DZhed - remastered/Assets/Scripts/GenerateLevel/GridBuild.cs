using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBuild : MonoBehaviour
{

    [SerializeField] public int rows = 13;
    public GameObject[,] cell;
    public int cellSize;

    public static GridBuild Instance { get; private set; }
    

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

    private const int panelSize = 800;
    private const int level = 6;

    void Start()
    {
        LevelBuilder.Instance.BuildLevel(level);
        cellSize = SetCellSize();
        cell = new GameObject[rows, rows];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                cell[row, col] = (GameObject)Instantiate(Resources.Load("Cell"), transform);
                cell[row, col].GetComponent<CellModifier>().row = row;
                cell[row, col].GetComponent<CellModifier>().col = col;
            }
        }
        LevelBuilder.Instance.BuildLevel(level);
        cell[6, 3].GetComponent<Image>().color = new Color32(255, 0, 255, 255);
    }

    int SetCellSize()
    {
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(panelSize / rows, panelSize / rows);
        return panelSize / rows;
    }

}