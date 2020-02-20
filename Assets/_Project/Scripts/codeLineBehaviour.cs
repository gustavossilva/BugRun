using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codeLineBehaviour : MonoBehaviour
{
    RectTransform codeTransform;
    public float downVelocity = 1f; 
    private EnemyController controller;
    // Start is called before the first frame update
    void Start()
    {
        codeTransform = gameObject.GetComponent<RectTransform>();
        controller = EnemyController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.Instance.gameOver && !GameController.Instance.gamePaused) {  
            codeTransform.anchoredPosition  +=  Vector2.down * GameController.Instance.gameSpeed * Time.deltaTime;
        }
        if(GameController.Instance.gameOver && gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }
}
