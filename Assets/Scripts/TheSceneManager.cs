using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheSceneManager : MonoBehaviour
{
    private Populator populator;
    public bool IsGameOver = false;
    public int TimeSpent;
    [Tooltip("Reference to GameObject controlling the Game HUD")]
    [SerializeField] UIController uiController;
    
    // Start is called before the first frame update
    void Start()
    {
        populator = GameObject.Find("Grid Manager").GetComponent<Populator>();
        TimeSpent = 0;
        StartCoroutine(CountUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
            return;
        if (populator.NumberOfEnemies <= 0)
        {
            DeclareGameWon();
            return;
        }
    }

    private IEnumerator CountUp()
    {
        while (!IsGameOver)
        {
            yield return new WaitForSeconds(1f);
            ++TimeSpent;
            uiController.TimeSpentText.text = $"Time Spent: {TimeSpent}";
        }
    }

    public void DeclareGameWon()
    {
        uiController.DisplayGameWin();
        IsGameOver = true;
        //Time.timeScale = 0f;
    }

    public void DeclareGameLost()
    {
        uiController.DisplayGameLost();
        IsGameOver = true;
        //Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Scenes/GameplayLevel");
        //Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
