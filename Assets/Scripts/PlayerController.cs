using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _playerSpeed;
    Rigidbody rb;
    [HideInInspector] public Animator animator;
    SceneLoader sceneLoader;
    ScoreManager scoreManager;
    MovingTrap[] movingTrap;
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    public float MoveFactorX => _moveFactorX;
    private float _tendingSpeed=0.5f;
    private float _maxTendingAmount=50f;
    private float _maxMove_x=2f;

    void Awake() {

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sceneLoader=FindObjectOfType<SceneLoader>();
        scoreManager=FindObjectOfType<ScoreManager>();
        movingTrap=FindObjectsOfType<MovingTrap>();
        
    }

    void Start() {
        animator.SetBool("isIdle", true);
    }
    void FixedUpdate()
    {
        Debug.Log(GameManager.Instance.gameStatusValue);
        
        if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.PLAY){
            
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true);
            Movement();
            TendingMove();
        }

        if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.FAILED){
            animator.SetBool("isRunning", false);
            animator.SetBool("isDeath", true);
            scoreManager.scoreValue=0;
        }

        if(GameManager.Instance.gameStatusValue==GameManager.GameStatus.NEXTLEVEL){

            animator.SetBool("isFinished", true);
            StartCoroutine(sceneLoader.LoadNextLevel());
            StartCoroutine(StartPos());
        }

 
    }

    void Movement(){

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, _playerSpeed);
    }

    public void CheckTouches()
    {
        if (Input.GetMouseButtonDown(0)){

            _lastFrameFingerPositionX = Input.mousePosition.x;

        }
        else if (Input.GetMouseButton(0)){

            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;

        }else if (Input.GetMouseButtonUp(0)){

            _moveFactorX = 0f;
        }

        else _moveFactorX = 0f;
        
    }

    public void TendingMove()
    {
        CheckTouches();
        float _tendingAmount = _tendingSpeed * MoveFactorX;
        _tendingAmount = Mathf.Clamp(_tendingAmount, - _maxTendingAmount, _maxTendingAmount);
        _tendingAmount = EdgeController(_tendingAmount);
        rb.velocity = new Vector3(_tendingAmount, rb.velocity.y, rb.velocity.z);
    }

    public float EdgeController(float tendingValue)
    {
        tendingValue = LeftLimitter(tendingValue);
        tendingValue = RightLimitter(tendingValue);
        return tendingValue;
    }

    public float LeftLimitter(float tendingValue)
    {
        float _xPosition = rb.transform.position.x;
        if (_xPosition <= -(_maxMove_x))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            tendingValue = Mathf.Clamp(tendingValue, .1f, _maxTendingAmount);
            return tendingValue;
        }
        return tendingValue;
    }
    public float RightLimitter(float tendingValue)
    {
        float _xPosition = rb.transform.position.x;
        if (_xPosition >= _maxMove_x)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            tendingValue = Mathf.Clamp(tendingValue, -(_maxTendingAmount), -.1f);
            return tendingValue;
        }
        return tendingValue;
    }

    void OnTriggerEnter(Collider coll) {

        if(coll.CompareTag("Obstacle")){

            GameManager.Instance.gameStatusValue=GameManager.GameStatus.FAILED;

        }
        
        if(coll.CompareTag("Finish")){

            GameManager.Instance.gameStatusValue=GameManager.GameStatus.NEXTLEVEL;
            animator.SetBool("isRunning", false);
            sceneLoader.activeLevel++;    

        }else if(coll.CompareTag("Finish") && sceneLoader.activeLevel==5){
            GameManager.Instance.gameStatusValue=GameManager.GameStatus.FINISH;
        }

        if(coll.CompareTag("Diamond")){
            scoreManager.scoreValue++;
        }

        if(coll.CompareTag("TrapTrigger")){

            for (int i = 0; i < movingTrap.Length; i++)
            {
                movingTrap[i].isTrapActivated=true;
            }
        }

        
    }

    IEnumerator StartPos(){

        yield return new WaitForSeconds(4f);
        animator.SetBool("isFinished", false);
        transform.position= new Vector3(0f, 0.5f, 0f);
        animator.SetBool("isIdle", true);
        yield return new WaitForSeconds(4f);
        GameManager.Instance.gameStatusValue=GameManager.GameStatus.NONE;
        
    }

}
