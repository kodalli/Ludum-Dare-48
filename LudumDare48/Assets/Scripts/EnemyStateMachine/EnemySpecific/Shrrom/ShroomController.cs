using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : MonoBehaviour {
    Rigidbody2D rb;

    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float speed;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start() {
        rb.velocity = speed * Vector2.left;
    }

    void FixedUpdate() {
        transform.Rotate(0, 0, rotateSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other) {

    }
}
