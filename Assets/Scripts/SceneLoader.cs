using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   
    [SerializeField] GameObject[] Levels;
    public int activeLevel;
    
    void Awake() {

        activeLevel=1;
        
    }
    

    public IEnumerator LoadNextLevel(){

        for (int i = 0; i < GameManager.Instance.confetti.Length; i++)
        {
            GameManager.Instance.confetti[i].Play(true);
        }
        yield return new WaitForSeconds(4f);

        switch (activeLevel){
            case 2: 
                Levels[0].gameObject.SetActive(false);
                Levels[1].gameObject.SetActive(true); 
            break;
            case 3:
                Levels[1].gameObject.SetActive(false);
                Levels[2].gameObject.SetActive(true);
            break;
            case 4:
                Levels[2].gameObject.SetActive(false);
                Levels[3].gameObject.SetActive(true);
            break;
            case 5:
                Levels[3].gameObject.SetActive(false);
                Levels[4].gameObject.SetActive(true);
            break;

            default:
                activeLevel=1;
            break;

        }
        
    }
}
