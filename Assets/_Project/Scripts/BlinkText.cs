using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkText : MonoBehaviour {
    public TextMeshProUGUI text;
    public Color blinkTo;
    public float animationSpeed = 2f;
    public bool playAnimation = true;

    void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(blinkAnimation());
    }

    void OnDestroy() {
        StopAllCoroutines();
    }

    void OnDisable() {
        StopAllCoroutines();
    }

    private IEnumerator blinkAnimation() {
        Color initialColor = this.text.color;
        Color currentColor = this.text.color;
        float counter = 0;

        while(playAnimation) {
            if (counter > animationSpeed ) {
                currentColor = this.blinkTo;
                this.blinkTo = initialColor;
                initialColor = currentColor;
                counter = 0;
            }
            counter += Time.deltaTime;
            this.text.color = Color.Lerp(currentColor, this.blinkTo, counter / this.animationSpeed);
            yield return null;
        }
    }
}
