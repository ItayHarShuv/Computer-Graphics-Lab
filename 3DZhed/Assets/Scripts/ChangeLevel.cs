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
        if (addSub == AddSubLevel.add && MeshedGrid.Instance.currentLevel < LevelBuilder.Instance.maxLevel)
        {
            MeshedGrid.Instance.BuildGrid(++MeshedGrid.Instance.currentLevel);
            MeshedGrid.Instance.GetComponent<Transform>().eulerAngles = Vector3.zero;
        }
        else if (addSub == AddSubLevel.sub && MeshedGrid.Instance.currentLevel > 1)
        {
            MeshedGrid.Instance.BuildGrid(--MeshedGrid.Instance.currentLevel);
            MeshedGrid.Instance.GetComponent<Transform>().eulerAngles = Vector3.zero;
        }
    }

    public enum AddSubLevel
    {
        add,
        sub
    }
}
