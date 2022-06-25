using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button playButton;


    public void playGame(){

        playButton.gameObject.SetActive(false);
        GameManager.Instance.gameStatusValue = GameManager.GameStatus.PLAY;
        
    }
   
}
