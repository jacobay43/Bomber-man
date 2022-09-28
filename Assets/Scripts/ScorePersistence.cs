using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class ScorePersistence : MonoBehaviour
{
    private string DataPath;
    private string HighScoresFileName;
    private ScoreModel Leaderboard;

    private void Awake()
    {
        DataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(DataPath);
        HighScoresFileName = DataPath + "HighScores.json";
    }
    // Start is called before the first frame update
    void Start()
    {
        Leaderboard = new ScoreModel("Dan",50,"Samantha",51,"Duke",200,"Liam",250,"Anna",300);

        Initialize();
    }

    private void Initialize()
    {
        NewDirectory();
        NewJSONFile(HighScoresFileName);
    }

    private void NewDirectory()
    {
        if (Directory.Exists(DataPath))
            Debug.Log("Persistence Directory already exists");
        else
        {
            Directory.CreateDirectory(DataPath);
            Debug.Log("Persistence Directory Created");
        }
    }

    private void NewJSONFile(string filename)
    {
        if (File.Exists(filename))
            return;
        //else
        string jsonString = JsonUtility.ToJson(Leaderboard);
     
        using (StreamWriter stream = File.CreateText(filename))
        {
            stream.WriteLine(jsonString);
        }
    }

    public ScoreModel RetrieveHighScores()
    {
        if (File.Exists(HighScoresFileName))
        {
            using (StreamReader stream = new StreamReader(HighScoresFileName))
            {
                string jsonString = stream.ReadToEnd();
                return JsonUtility.FromJson<ScoreModel>(jsonString);
            }
        }
        else
            return Leaderboard;
    }

    public void UpdateLeaderboard(ScoreModel NewBoard)
    {
        if (!File.Exists(HighScoresFileName))
            NewJSONFile(HighScoresFileName);
        
        string jsonString = JsonUtility.ToJson(NewBoard);
        using (StreamWriter stream = File.CreateText(HighScoresFileName))
        {
            stream.WriteLine(jsonString);
        }
    }
}
