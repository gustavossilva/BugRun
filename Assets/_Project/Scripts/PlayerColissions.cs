using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerColissions : MonoBehaviour
{
  private Animator playerAnimator;

  public AudioSource dieSounds;
  public AudioSource collectionSounds;

  public AudioClip dieSound;
  public AudioClip collectionSound;

  private void Awake() {
     playerAnimator = PlayerController.Instance.playerAnimator;

  }
  private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("bug")) {
          other.gameObject.transform.parent.gameObject.SetActive(false);
          collectionSounds.clip = collectionSound;
          GameController.Instance.SetScore(10);
          collectionSounds.Play();

        } else {
          dieSounds.clip = dieSound;
          dieSounds.Play();
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
