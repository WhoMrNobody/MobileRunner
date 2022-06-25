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

    public enum GameStatus{
        PLAY,
        FAILED,
        NEXTLEVEL,
        FINISH,
        NONE,
    }
}
