using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{

    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        OpenMeshCell.Instance.Undo();       
    }
}
