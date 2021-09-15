using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    public void LoadNext()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;

        if (currentSceneIndex >= SceneManager.sceneCountInBuildSettings)
            currentSceneIndex = 0;
        
        SceneManager.LoadScene(currentSceneIndex);
    }
}
