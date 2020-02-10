using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColissions : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other) {
    Debug.Log("Colidi com" + other.ToString());
  }

  private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Colidi com" + other.name);
        if(other.CompareTag("bug")) {
          other.gameObject.transform.parent.gameObject.SetActive(false);
          GameController.Instance.SetScore(10);
        } else {
          GameController.Instance.GameOver();
        }

  }

}
