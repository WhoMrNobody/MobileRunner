using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas gameOverCanvas_;

    SceneLoader sceneLoader;

    void Awake() {

        gameOverCanvas_.gameObject.SetActive(false);
        sceneLoader=FindObjectOfType<SceneLoader>();
    }

    public void playGame(){

        playButton.gameObject.SetActive(false);
        GameManager.Instance.gameStatusValue = GameManager.GameStatus.PLAY;
        
    }

    void Update() {
        
        if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.FAILED){

            gameCanvas.gameObject.SetActive(false);
            gameOverCanvas_.gameObject.SetActive(true);

        }else if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.NONE){
            
            gameOverCanvas_.gameObject.SetActive(false);

        }else if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.NEXTLEVEL){

            Invoke(nameof(ActivatePlayButton), 4.5f);
        }

    }

    public void RestartGame(){

        gameOverCanvas_.gameObject.SetActive(false);
        GameManager.Instance.gameStatusValue=GameManager.GameStatus.NONE;
        SceneManager.LoadScene(sceneLoader.activeLevel);
        
    }

    public void BackToMainMenu(){

        gameOverCanvas_.gameObject.SetActive(false);
        GameManager.Instance.gameStatusValue=GameManager.GameStatus.NONE;
        SceneManager.LoadScene(0);
        
    }

    void ActivatePlayButton(){
        playButton.gameObject.SetActive(true);
    }
   
}
