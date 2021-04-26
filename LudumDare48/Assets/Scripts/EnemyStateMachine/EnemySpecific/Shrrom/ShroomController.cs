using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : MonoBehaviour, IDamageable {
    Rigidbody2D rb;

    [SerializeField] private float health = 10f;

    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float speed;

    [SerializeField] float contactDamage;

    [SerializeField] LayerMask whatIsPlayer;

    [SerializeField] Transform playerCheck;
    [SerializeField] float playerCheckDistance;

    bool isPlayerInRange;
    bool isDetected;

    float destroyTime = 3f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        isPlayerInRange = CheckPlayerInRange();
        PlayerDetected();
    }

    void PlayerDetected() {
        if (isPlayerInRange) {
            isDetected = true;
            rb.velocity = speed * Vector2.left;
            transform.Rotate(0, 0, rotateSpeed);
            StartCoroutine(DestroyShroom());
        } else if (isDetected) {
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
    public void TakeDamage(float damage) {
        health -= (int)damage;
        if (health <= 0) {
            Destroy(this.gameObject, 0.1f);
        }
    }
    IEnumerator DestroyShroom() {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(-Vector2.right * playerCheckDistance));

    }
}
