using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void ReplayBtn()
    {
        //SceneManager.LoadScene(0);
        
        FindObjectOfType<ScenePersist>().ResetScenePersist();

    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
