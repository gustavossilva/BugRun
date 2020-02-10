using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerColissions : MonoBehaviour
{
  private Animator playerAnimator;

  private void Awake() {
     playerAnimator = PlayerController.Instance.playerAnimator;

  }
  private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("bug")) {
          other.gameObject.transform.parent.gameObject.SetActive(false);
          GameController.Instance.SetScore(10);
        } else {
          StartCoroutine("GameOverAfterAnimation");
        }
  }
	private IEnumerator GameOverAfterAnimation() {
    GameController.Instance.gameOver = true;
    playerAnimator.SetBool("die", true);
		yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);
    GameController.Instance.GameOver();
  }

}
