using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText_;

    AsyncOperation sceneLoader;

    void Start() {
        
        sceneLoader= SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        sceneLoader.allowSceneActivation=false;
    }
    public void StartGame(){
        sceneLoader.allowSceneActivation=true;
    }

    public void Quit(){
        Application.Quit();
    }
    private void OnApplicationFocus(bool focusStatus) {

        if(!focusStatus){ return; }

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreManager.HighScoreKey, 0);

        highScoreText_.text=$"High Score : {highScore}";
        
    }
}
