using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelNum : MonoBehaviour
{
    public static LevelNum Instance { get; private set; }

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

    public void DisplayLevelNum()
    {
        GetComponent<Text>().text = MeshedGrid.Instance.currentLevel.ToString();
    }
}
