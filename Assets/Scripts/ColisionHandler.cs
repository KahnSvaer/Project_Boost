using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Respawn"){}
        else if (other.gameObject.tag == "Finish")
        {
            LevelCompleteSequence();
        }
        else
        {
            CrashSequence();
        }

    }

    private void LevelCompleteSequence()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        int currant_level = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currant_level + 1;
        nextLevel %= SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevel);
    }

    private void CrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        ReloadLevel();
    }

    private static void ReloadLevel()
    {
        int currant_level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currant_level);
    }
}
