using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SpreadCell);
    }

    public void SpreadCell()
    {
        Debug.Log(gameObject.name + "spreaded");
    }
}
