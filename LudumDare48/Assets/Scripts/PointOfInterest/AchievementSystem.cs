using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour {
    [SerializeField] private GameObject dialoguePanel;

    private void OnDestroy() {
        PointOfInterest.OnPoiEntered -= OnPoiEnteredNotification;
    }
    private void OnPoiEnteredNotification(PointOfInterest poi) {
        throw new System.NotImplementedException();
    }
}
