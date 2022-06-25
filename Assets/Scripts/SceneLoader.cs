using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int activeScene;
    
    void Awake() {

        activeScene=SceneManager.GetActiveScene().buildIndex;
        
    }
    
    void Update()
    {
        activeScene=SceneManager.GetActiveScene().buildIndex;
        
    }

    public IEnumerator LoadNextLevel(){

        for (int i = 0; i < GameManager.Instance.confetti.Length; i++)
        {
            GameManager.Instance.confetti[i].Play(true);
        }
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(activeScene + 1);

    }
}
