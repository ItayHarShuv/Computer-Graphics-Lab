using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler2048 : MonoBehaviour
{
    public static GameControler2048 instance;
    public static int ticker;

    [SerializeField] GameObject fillPrefab;
    [SerializeField] Cell2048[] allCells;

    public static Action<string> slide;
    public int myScore;
    [SerializeField] Text scoreDisplay;

    int isGameOver;
    [SerializeField] GameObject gameOverPanel;

    void Start()
    {
        instance = this;
        StartSpawnFill();
        StartSpawnFill();
    }

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            ticker = 0;
            isGameOver = 0;
            slide("w");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ticker = 0;
            isGameOver = 0;
            slide("d");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ticker = 0;
            isGameOver = 0;
            slide("s");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ticker = 0;
            isGameOver = 0;
            slide("a");
        }
    }

    public void SpawnFill()
    {
        bool isFull = true;
        for (int i = 0; i < allCells.Length; i++)
        {
            if(allCells[i].fill == null)
            {
                isFull = false;
            }
        }
        Debug.Log(isFull);
        if(isFull == true)
        {
            return;
        }

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if(allCells[whichSpawn].transform.childCount != 0)
        {
            SpawnFill();
            return;
        }
        float chance = UnityEngine.Random.Range(0f, 1f);
        
        if(chance < .2f)
        {
            return;
        }
        else if(chance < .8f)
        {

            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else
        {

            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }

    public void StartSpawnFill()
    {
        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            StartSpawnFill();
            return;
        }
        
        GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
        Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
        allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
        tempFillComp.FillValueUpdate(2);
    }

    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        scoreDisplay.text = myScore.ToString();
    }

    public void GameOverCheck()
    {
        isGameOver++;
        {
            if(isGameOver >= 16)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}