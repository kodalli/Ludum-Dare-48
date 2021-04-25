using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointOfInterest : MonoBehaviour {

    public static event Action<PointOfInterest> OnPoiEntered;

    [SerializeField] private string _poiName;

    public string PoiName { get => uniqueID; }

    private string uniqueID = Guid.NewGuid().ToString("N");

    private void OnTriggerEnter2D(Collider2D other) {
        if (OnPoiEntered != null) {
            OnPoiEntered(this);
            // gameObject.SetActive(false);
        }
    }

}