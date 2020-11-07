using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CellModifier: MonoBehaviour
{
    public int num = 0;
    public bool filled = false;

    public int row;
    public int col;


    void Start()
    {
        if (num != 0 && num != -1) filled = true;
        SetColor();
        PrintNum();
    }

    

    public void SetColor()
    {
        if(!filled)
        {
            byte col = (byte)UnityEngine.Random.Range(0, 40);
            GetComponent<Image>().color = new Color32(col, col, col, 150);
        }
        else
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        }

        if(num == -1)
        {
            GameObject winSquare = new GameObject();
            winSquare = (GameObject)Instantiate(Resources.Load("WinCell"), transform);
            RectTransform rt = winSquare.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(GridBuild.Instance.cellSize, GridBuild.Instance.cellSize);
        }
    }

    public void PrintNum()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject.GetComponent<Text>());
        }
        if (num == 0 || num == -1)
            return;
        GameObject text = (GameObject)Instantiate(Resources.Load("Text"), transform);
        RectTransform rt = text.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);
        text.GetComponent<Text>().text = num.ToString();

    }

}
