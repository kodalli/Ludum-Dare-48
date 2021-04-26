using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICreeditMart : MonoBehaviour {

    private TextMeshProUGUI debtMeter;
    private TextMeshProUGUI gunLevel;
    private TextMeshProUGUI o2Meter;

    private void Awake() {
        debtMeter = gameObject.FindInChildren("UI_CURRENT_DEBT_AMOUNT").GetComponent<TextMeshProUGUI>();
        gunLevel = gameObject.FindInChildren("UI_CURRENT_GUN_LEVEL").GetComponent<TextMeshProUGUI>();
        o2Meter = gameObject.FindInChildren("UI_CURRENT_O2_AMOUNT").GetComponent<TextMeshProUGUI>();
    }
    private void Update() {
        debtMeter.text = LocalSave.Instance.saveData.debt.ToString();
        gunLevel.text = LocalSave.Instance.saveData.gunLevel.ToString();
        o2Meter.text = LocalSave.Instance.saveData.oxygen.ToString();
    }
}
