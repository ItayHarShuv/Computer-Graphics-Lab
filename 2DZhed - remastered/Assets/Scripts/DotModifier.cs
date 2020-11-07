using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotModifier : MonoBehaviour
{
    private float scale = 0.3f;

    void Start()
    {
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2( scale*GridBuild.Instance.cellSize, scale*GridBuild.Instance.cellSize);
    }

   
}
