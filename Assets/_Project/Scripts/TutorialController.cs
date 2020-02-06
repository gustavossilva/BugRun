using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    public GameObject playerStep1;
    public GameObject playerStep2;
    public GameObject playerStep3;
    public Animator playerStep3Animator;

    public GameObject step1;
    public GameObject step2;
    public GameObject step3;
    // Start is called before the first frame update
    void Start()
    {
        playerStep3Animator = playerStep3.GetComponent<Animator>();
    }


    public void SetStep(string step) {
        switch(step) {
            case "2":
                playerStep1.SetActive(false);
                playerStep2.SetActive(true);
                step1.SetActive(false);
                step2.SetActive(true);
                break;
            case "3":
                playerStep2.SetActive(false);
                playerStep3.SetActive(true);
                step2.SetActive(false);
                step3.SetActive(true);
                playerStep3Animator.SetTrigger("die");
            break;
            case "final":
                SceneManager.LoadScene("Game");
            break;
            default:
            break;
        }
    }
}
