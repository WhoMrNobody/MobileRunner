using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText_;

    public void StartGame(){
        SceneManager.LoadScene(1);
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
