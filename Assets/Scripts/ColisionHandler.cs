using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    [SerializeField] int waitTimeRespawn = 3;
    [SerializeField] int waitTimeNextLevel = 3;

    
    [SerializeField] AudioClip sucessSFX;
    [SerializeField] AudioClip failSFX;



    AudioSource audioSource;
    Rigidbody rb;
    
    
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();   
    }

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
        audioSource.PlayOneShot(sucessSFX);
        GetComponent<Movement>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll; //To stop all motion
        Invoke("LoadNextLevel",waitTimeNextLevel);
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
        audioSource.PlayOneShot(failSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",waitTimeRespawn);
    }

    private void ReloadLevel()
    {
        int currant_level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currant_level);
    }
}
