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
        OpenCell.Instance.Restart();
        Grid.Instance.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
    }
}
