using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] float _trapRotationSpeed;
    [SerializeField] float _trapMoveSpeed;
    public bool isTrapActivated =false;

    Vector3 _currentTransformPos;
    void Start() {
        
        _currentTransformPos = transform.position;

    }
    void Update()
    {
        
        if(isTrapActivated){

            transform.Translate(Vector3.back * _trapMoveSpeed * Time.deltaTime, Space.World);
            transform.Rotate(-_trapRotationSpeed, transform.rotation.y, transform.rotation.z);
            
        }

        if(GameManager.Instance.gameStatusValue== GameManager.GameStatus.NONE){
            transform.position= _currentTransformPos;
            isTrapActivated=false;
        } 
    }

    void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Obstacle")) Destroy(gameObject);
    }
}
