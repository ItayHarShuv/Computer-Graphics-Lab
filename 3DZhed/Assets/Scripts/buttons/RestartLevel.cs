using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour
{
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        OpenMeshCell.Instance.Restart();
        MeshedGrid.Instance.GetComponent<Transform>().eulerAngles = Vector3.zero;       
    }
}
