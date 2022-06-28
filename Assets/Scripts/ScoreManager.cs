using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TMP_Text scoreText_;   

    public const string HighScoreKey = "HighScore";
    public int scoreValue=0;

    void Update()
    {
        scoreText_.text=scoreValue.ToString();
    }

    public void OnDestroy() {
        
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if(scoreValue>currentHighScore){
            PlayerPrefs.SetInt(HighScoreKey, scoreValue);
        }

    }
}
