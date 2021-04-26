using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public static class Helper {
    public static GameObject FindInChildren(this GameObject go, string name) {
        return (from x in go.GetComponentsInChildren<Transform>()
                where x.gameObject.name == name
                select x.gameObject).First();
    }
}
public class UIManager : Singleton<UIManager> {

    // Main Game HUD
    private TextMeshProUGUI currentHPText;
    private TextMeshProUGUI currentOxygenText;
    private Image healthFill;
    private Image oxygenFill;


    private void Awake() {
        currentHPText = gameObject.FindInChildren("HUD_CURRENT_HP_TEXT").GetComponent<TextMeshProUGUI>();
        currentOxygenText = gameObject.FindInChildren("HUD_CURRENT_O2_TEXT").GetComponent<TextMeshProUGUI>();
        healthFill = gameObject.FindInChildren("HUD_CURRENT_HEALTH_FILL").GetComponent<Image>();
        oxygenFill = gameObject.FindInChildren("HUD_CURRENT_O2_FILL").GetComponent<Image>();
    }

    public void SetHPHUD() {
        float ratio = Player.Instance.CurrentHP / Player.Instance.MaxHP;
        // healthFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSizeHealth * ratio);
        healthFill.fillAmount = ratio;
        currentHPText.text = Player.Instance.CurrentHP.ToString() + "%";
    }

    public void SetOxgyenHUD() {

        float ratio = Player.Instance.CurrentOxygen / Player.Instance.MaxOxygen;
        // oxygenFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSizeOxygen * ratio);
        oxygenFill.fillAmount = ratio;

        currentOxygenText.text = Player.Instance.CurrentOxygen.ToString() + "%";
    }

}
