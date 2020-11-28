using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField]AddSubLevel addSub;

    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if(addSub == AddSubLevel.add)
        {
            Grid.Instance.SetGrid(Grid.Instance.currentLevel + 1);
        }
        else
        {
            Grid.Instance.SetGrid(Grid.Instance.currentLevel - 1);
        }
        
    }

    public enum AddSubLevel
    {
        add,
        sub
    }
}
