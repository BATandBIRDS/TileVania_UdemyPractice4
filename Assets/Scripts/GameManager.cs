using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public bool gotKey;
    [SerializeField] float levelLoadDelay = 1f;
    

    void Start()
    {
        
        gotKey = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "key") 
        { 
            gotKey = true;
            Destroy(other.gameObject);
            FindObjectOfType<GameSession>().GotKey();
            Debug.Log("gotKey");
        }

        if(gotKey && other.tag == "door")
        {
            Debug.Log("knocking");

            StartCoroutine(LoadNextLevel());

            // OLD CODE
            //if (SceneManager.GetActiveScene().buildIndex == 0)
            //{
            //    SceneManager.LoadScene(1);
            //}
            //else if (SceneManager.GetActiveScene().buildIndex == 1)
            //{
            //    SceneManager.LoadScene(2);
            //}
            //else if (SceneManager.GetActiveScene().buildIndex == 2)
            //{
            //    SceneManager.LoadScene(3);
            //}



        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelLoadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
        gotKey = false;

    }
}
