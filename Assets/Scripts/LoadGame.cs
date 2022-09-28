using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadGameLevel()
    {
        StartCoroutine(LoadAsyncScene());
    }
    private IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/GameplayLevel");
        while (!asyncLoad.isDone)
            yield return null;
    }
}
