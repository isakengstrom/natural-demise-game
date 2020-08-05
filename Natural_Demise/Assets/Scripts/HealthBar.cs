using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private Image foregroundImage;

    private readonly float _updateSpeedSeconds = 0.2f;

    private void Awake() {
        GetComponentInParent<CharacterMotor>().OnHealthPctChange += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct) {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct) {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while(elapsed < _updateSpeedSeconds) {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / _updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
