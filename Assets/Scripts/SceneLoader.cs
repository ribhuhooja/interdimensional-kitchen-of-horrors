using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int indexToLoad;

    public void loadIndexScene()
    {
        SceneManager.LoadScene(indexToLoad);
    }
    public void loadNextScene() {
        if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
