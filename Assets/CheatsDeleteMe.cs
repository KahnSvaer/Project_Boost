using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatsDeleteMe : MonoBehaviour
{
    Collider[] colliders; 
    void Start()
    {
        colliders = GetComponentsInChildren<Collider>(); //Using upper level reference here to get all Colliders
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            ToggleColliders();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    private void ToggleColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = !collider.enabled;
        }
    }
    
    private void LoadNextLevel()
    {
        int currant_level = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currant_level + 1;
        nextLevel %= SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevel);
    }
}
