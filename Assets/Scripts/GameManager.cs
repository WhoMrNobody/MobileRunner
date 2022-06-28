using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance { get; private set; }
    [HideInInspector] public GameStatus gameStatusValue;
    [HideInInspector] public ParticleSystem[] confetti;
    
    void Awake() {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            gameStatusValue = GameStatus.NONE;
            DontDestroyOnLoad(gameObject);
        }

        confetti=FindObjectsOfType<ParticleSystem>();

    }

    void Update() {

        if(Instance.gameStatusValue==GameStatus.NONE){

            for (int i = 0; i < confetti.Length; i++)
            {
                Instance.confetti[i].Stop();
            }
        }

    }

    public enum GameStatus{
        PLAY,
        FAILED,
        NEXTLEVEL,
        FINISH,
        NONE,
    }
}
