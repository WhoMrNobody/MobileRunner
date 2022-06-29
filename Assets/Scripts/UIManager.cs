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
    GameObject crossfade;
    PlayerController playerController;

    void Awake() {

        gameOverCanvas_.gameObject.SetActive(false);
        sceneLoader=FindObjectOfType<SceneLoader>();
        playerController=FindObjectOfType<PlayerController>();
        crossfade=GameObject.FindGameObjectWithTag("Crossfade");
        crossfade.GetComponent<Animator>().gameObject.SetActive(false);
    }

    public void playGame(){

        playButton.gameObject.SetActive(false);
        GameManager.Instance.gameStatusValue = GameManager.GameStatus.PLAY;
        
    }

    void Update(){
        
        if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.FAILED){

            gameCanvas.gameObject.SetActive(false);
            gameOverCanvas_.gameObject.SetActive(true);

        }else if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.NEXTLEVEL){
            
            StartCoroutine(ActivatePlayButton());

        }else if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.NONE){

            playButton.gameObject.SetActive(true);
            gameCanvas.gameObject.SetActive(true);

        }

    }

    public void RestartGame(){

        gameOverCanvas_.gameObject.SetActive(false);
        GameManager.Instance.gameStatusValue=GameManager.GameStatus.NONE;
        playerController.transform.position= new Vector3(0f, 0.5f, 0f);
        playerController.animator.SetBool("isDeath", false);
        playerController.animator.SetBool("isIdle", true);
    }

    public void BackToMainMenu(){

        gameOverCanvas_.gameObject.SetActive(false);
        GameManager.Instance.gameStatusValue=GameManager.GameStatus.NONE;
        SceneManager.LoadScene(0);
        
    }

    IEnumerator ActivatePlayButton(){
        
        crossfade.GetComponent<Animator>().gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        crossfade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(8f);
        crossfade.SetActive(false);
        playButton.gameObject.SetActive(true);
    }
   
}
