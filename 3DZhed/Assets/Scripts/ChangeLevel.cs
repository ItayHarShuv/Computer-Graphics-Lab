using System.Collections;
using UnityEngine;


public class ChangeLevel : MonoBehaviour
{
    public static ChangeLevel Instance { get; private set; }

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

    public void ChangeLevelMet(AddSubLevel addSub)
    {
        if (addSub == AddSubLevel.add && Grid.Instance.currentLevel < LevelBuilder.Instance.maxLevel)
        {
            Grid.Instance.SetGrid(Grid.Instance.currentLevel + 1);
            Grid.Instance.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
        }
        else if (addSub == AddSubLevel.sub && Grid.Instance.currentLevel > 1)
        {
            Grid.Instance.SetGrid(Grid.Instance.currentLevel - 1);
            Grid.Instance.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
        }
    }

    public enum AddSubLevel
    {
        add,
        sub
    }
}
