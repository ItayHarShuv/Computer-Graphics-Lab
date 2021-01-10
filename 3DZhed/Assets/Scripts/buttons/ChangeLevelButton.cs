using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField] ChangeLevel.AddSubLevel addSub;

    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        ChangeLevel.Instance.ChangeLevelMet(addSub);
    }
}
