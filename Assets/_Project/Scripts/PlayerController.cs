using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float pcSpeed;
    public float mobileSpeed;
    private float screenCenterX;

    public GameObject player;
    public Animator playerAnimator;
    SpriteRenderer playerSprite;

    void Start() {
        playerSprite = player.GetComponent<SpriteRenderer>();
        screenCenterX = Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        #if (UNITY_EDITOR) || (UNITY_WEBGL)
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            playerAnimator.SetBool("isRunning", movement.x > 0 || movement.x < 0);
            if (movement.x < 0) {
                playerSprite.flipX = true;
            }
            if (movement.x > 0) {
                playerSprite.flipX = false;
            }
            player.transform.position += movement * Time.deltaTime * pcSpeed;
        #endif

        #if (UNITY_IOS) || (UNITY_ANDROID)
            // if there are any touches currently
            if(Input.touchCount > 0)
            {
                // get the first one
                Touch firstTouch = Input.GetTouch(0);
    
                // if it began this frame
                if(firstTouch.phase == TouchPhase.Began || firstTouch.phase == TouchPhase.Stationary || firstTouch.phase == TouchPhase.Moved)
                {
                    if(firstTouch.position.x > screenCenterX) {
                        player.transform.position += Vector3.right * Time.deltaTime * mobileSpeed;
                        playerAnimator.SetBool("isRunning", true);
                        playerSprite.flipX = false;
                    } else if(firstTouch.position.x < screenCenterX) {
                        player.transform.position += Vector3.left * Time.deltaTime * mobileSpeed;
                        playerAnimator.SetBool("isRunning", true);
                        playerSprite.flipX = true;
                    }
                }
                if (firstTouch.phase == TouchPhase.Ended) {
                    playerAnimator.SetBool("isRunning", false);
                }
            }
        #endif
    }
}
