using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TMP_Text EnemyCounterLabel;
    public TMP_Text GameOverStatusLabel;
    public TMP_Text ScoreboardLabel;
    public TMP_Text TimeSpentText;
    public GameObject GameOverCanvas;
    public GameObject NameInputField;
    public ScorePersistence scorePersistence;
    private TheSceneManager SceneManager;
    private bool GameLost;
    private string PlayerName;
    private bool IsHigherThanAnyPrevious;
    private int PlayerScore;
    private ScoreModel Leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScore = 0;
        IsHigherThanAnyPrevious = false;
        GameLost = false;
        GameOverCanvas.SetActive(false);
        NameInputField.SetActive(false);
        PlayerName = "PLAYER";
        SceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<TheSceneManager>();
        EnemyCounterLabel.text = $"Enemies Left: {GameObject.Find("Grid Manager").GetComponent<Populator>().NumberOfEnemies.ToString()}";
        scorePersistence = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScorePersistence>();
    }

    public void DisplayGameWin()
    {
        GameOverStatusLabel.color = new Color(0, 1f, 0);
        GameOverStatusLabel.text = "YOU WIN";
        GameLost = false;
        PopulateScoreBoard();
    }

    public void DisplayGameLost()
    {
        GameOverStatusLabel.color = new Color(1f, 0f, 0f);
        GameOverStatusLabel.text = "YOU LOSE";
        GameLost = true;
        PopulateScoreBoard();
    }

    public void ResetGameOverStatusLabel()
    {
        GameOverStatusLabel.color = new Color(1f, 1f, 1f);
        GameOverStatusLabel.text = "";
    }

    private void PopulateScoreBoard()
    {
        //perform I/O with json file storing top 5 scores
        //if current score is higher than any of the particular ones
        //provide input field for name of player
        //rearrange persist and reload in ascending order of time used to clear level

        Leaderboard = scorePersistence.RetrieveHighScores();
        if (GameLost)
        {
            GameOverCanvas.SetActive(true);
            //display
            ScoreboardLabel.text = $"SCOREBOARD\nNAME\tTIME\n{Leaderboard.FirstPlaceName}\t{Leaderboard.FirstPlaceScore}\n{Leaderboard.SecondPlaceName}\t{Leaderboard.SecondPlaceScore}\n{Leaderboard.ThirdPlaceName}\t{Leaderboard.ThirdPlaceScore}\n{Leaderboard.FourthPlaceName}\t{Leaderboard.FourthPlaceScore}\n{Leaderboard.FifthPlaceName}\t{Leaderboard.FifthPlaceScore}";
        }
        else
        {
            //check if score is lower than any other, else don't bother including it in scoreboard
            //THIS IS WHERE YOU ALTER LEADERBOARD IF PLAYER WINS GAME AND HAS A SCORE HIGHER(IN THIS CASE LOWER IN VALUE) THAN THE ONES CURRENTLY STORED
            PlayerScore = SceneManager.TimeSpent;
            IsHigherThanAnyPrevious = false;

            if (PlayerScore < Leaderboard.FirstPlaceScore || PlayerScore < Leaderboard.SecondPlaceScore || PlayerScore < Leaderboard.ThirdPlaceScore || PlayerScore < Leaderboard.FourthPlaceScore || PlayerScore < Leaderboard.FifthPlaceScore)
            {
                NameInputField.SetActive(true);
            }
        }

    }

    public void OnSubmitName(string name)
        {
        NameInputField.SetActive(false);
        GameOverCanvas.SetActive(true);
        PlayerName = name;
            //TODO: Add Logic for Showing An InputTextField for Retrieving Player Name for Storage
            if (PlayerScore < Leaderboard.FirstPlaceScore)
            {
                Leaderboard.FirstPlaceName = PlayerName;
                Leaderboard.FirstPlaceScore = PlayerScore;
                IsHigherThanAnyPrevious = true;
            }
            else if (PlayerScore < Leaderboard.SecondPlaceScore)
            {
                Leaderboard.SecondPlaceName = PlayerName;
                Leaderboard.SecondPlaceScore = PlayerScore;
                IsHigherThanAnyPrevious = true;
            }
            else if (PlayerScore < Leaderboard.ThirdPlaceScore)
            {
                Leaderboard.ThirdPlaceName = PlayerName;
                Leaderboard.ThirdPlaceScore = PlayerScore;
                IsHigherThanAnyPrevious = true;
            }
            else if (PlayerScore < Leaderboard.FourthPlaceScore)
            {
                Leaderboard.FourthPlaceName = PlayerName;
                Leaderboard.FourthPlaceScore = PlayerScore;
                IsHigherThanAnyPrevious = true;
            }
            else if (PlayerScore < Leaderboard.FifthPlaceScore)
            {
                Leaderboard.FifthPlaceName = PlayerName;
                Leaderboard.FifthPlaceScore = PlayerScore;
                IsHigherThanAnyPrevious = true;
            }

            if (IsHigherThanAnyPrevious)
                scorePersistence.UpdateLeaderboard(Leaderboard);

        ScoreboardLabel.text = $"SCOREBOARD\nNAME\tTIME\n{Leaderboard.FirstPlaceName}\t{Leaderboard.FirstPlaceScore}\n{Leaderboard.SecondPlaceName}\t{Leaderboard.SecondPlaceScore}\n{Leaderboard.ThirdPlaceName}\t{Leaderboard.ThirdPlaceScore}\n{Leaderboard.FourthPlaceName}\t{Leaderboard.FourthPlaceScore}\n{Leaderboard.FifthPlaceName}\t{Leaderboard.FifthPlaceScore}";
    }
}

