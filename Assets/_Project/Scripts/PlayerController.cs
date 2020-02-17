using UnityEngine;
using TMPro;
public class PlayerController : Singleton<PlayerController>
{

    public float pcSpeed;
    public float mobileSpeed;
    public float playerSpeed;
    public Camera mainCamera;
    
    public GameObject player;
    public Animator playerAnimator;
    private Vector2 screenBounds;
    private float objectWidth;
    private float screenCenterX;
    private SpriteRenderer playerSprite;

    public double playerScore = 0;

    public TextMeshProUGUI score;
    protected override void Awake() {
        this.IsPersistentBetweenScenes = false;
        base.Awake();
        GameController.Instance.updateScore += setScore;
    }

    public void setScore(int value) {
        this.playerScore += value;
        score.SetText(playerScore.ToString());
    }
    void Start() {
        mainCamera = Camera.main;
        playerSprite = player.GetComponent<SpriteRenderer>();
        screenCenterX = Screen.width * 0.5f;
        screenBounds = this.mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, this.mainCamera.transform.position.z));
        objectWidth = playerSprite.bounds.extents.x; //extents = size of width / 2
        playerSpeed = GameController.Instance.extraPlayerVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.gameOver) {
            return;
        }
        #if (UNITY_EDITOR) || (UNITY_WEBGL)
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            playerAnimator.SetBool("isRunning", movement.x > 0 || movement.x < 0);
            if (movement.x < 0) {
                playerSprite.flipX = true;
            }
            if (movement.x > 0) {
                playerSprite.flipX = false;
            }
            player.transform.position += movement * Time.deltaTime * playerSpeed;
        #endif

        #if (UNITY_IOS) || (UNITY_ANDROID)
            if(Input.touchCount > 0 && !GameController.Instance.gameOver)
            {
                Touch firstTouch = Input.GetTouch(0);
                if(firstTouch.phase == TouchPhase.Began || firstTouch.phase == TouchPhase.Stationary || firstTouch.phase == TouchPhase.Moved)
                {
                    if(firstTouch.position.x > screenCenterX) {
                        player.transform.position += Vector3.right * Time.deltaTime * playerSpeed;
                        playerAnimator.SetBool("isRunning", true);
                        playerSprite.flipX = false;
                    } else if(firstTouch.position.x < screenCenterX) {
                        player.transform.position += Vector3.left * Time.deltaTime * playerSpeed;
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

    void LateUpdate()
    {
        if(gameObject) {
            Vector3 viewPos = player.transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
            player.transform.position = viewPos;
        }
    }
}
