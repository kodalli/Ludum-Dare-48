using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : MonoBehaviour {
    Rigidbody2D rb;

    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float speed;

    [SerializeField] float contactDamage;

    [SerializeField] LayerMask whatIsPlayer;

    [SerializeField] Transform playerCheck;
    [SerializeField] float playerCheckDistance;

    bool isPlayerInRange;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        isPlayerInRange = CheckPlayerInRange();
    }

    void FixedUpdate() {
        if (isPlayerInRange) {
            rb.velocity = speed * Vector2.left;
            transform.Rotate(0, 0, rotateSpeed);
        }
    }

    bool CheckPlayerInRange() => Physics2D.Raycast(playerCheck.position, -Vector2.right, playerCheckDistance, whatIsPlayer);

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<IIgnoreObject>()?.IgnoreMe() != null) return;

        var player = other.gameObject.GetComponent<IDamageable>();

        if (player != null) {
            player.TakeDamage(contactDamage);
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(-Vector2.right * playerCheckDistance));

    }
}
