using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : Singleton<HUD> {

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI oxygenText;
    [SerializeField] private Image healthFill;
    [SerializeField] private Image oxygenFill;


    // private float originalSizeHealth;
    // private float originalSizeOxygen;

    // private void Start() {
    //     // originalSizeHealth = healthFill.rectTransform.rect.width;
    //     SetHPHUD();
    //     SetOxgyenHUD();
    // }

    public void SetHPHUD() {
        float ratio = Player.Instance.CurrentHP / Player.Instance.MaxHP;
        // healthFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSizeHealth * ratio);
        healthFill.fillAmount = ratio;
        hpText.text = Player.Instance.CurrentHP.ToString() + "%";
    }

    public void SetOxgyenHUD() {

        float ratio = Player.Instance.CurrentOxygen / Player.Instance.MaxOxygen;
        // oxygenFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSizeOxygen * ratio);
        oxygenFill.fillAmount = ratio;

        oxygenText.text = Player.Instance.CurrentOxygen.ToString() + "%";
    }
    // private void LateUpdate() {
    //     SetHUD();

    //     if (hpSlider.value < hpSlider.maxValue * 0.3f) {
    //         healthFill.color = Color.red;
    //     } else if (hpSlider.value < hpSlider.maxValue * 0.6f) {
    //         healthFill.color = Color.yellow;
    //     } else {
    //         healthFill.color = Color.green;
    //     }
    // }

}
