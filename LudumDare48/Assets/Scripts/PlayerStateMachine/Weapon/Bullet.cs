using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float Speed { private get; set; }
    public Vector2 Direction { private get; set; }
    public float Damage { private get; set; }
    public float DestroyDelay { private get; set; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(DestroyBullet());
    }

    public void Shoot() {
        sr.flipX = (Direction.x < 0);
        rb.velocity = Direction * (Speed + Mathf.Abs(Player.Instance.CurrentVelocity.x));
    }

    private void FixedUpdate() {
        Accelerate();
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(DestroyDelay);
        Destroy(this.gameObject, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.GetComponentInParent<IDamageable>()?.TakeDamage(Damage);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(this.gameObject, 0.1f);
    }

    private void Accelerate() {
        Speed *= 1.01f;
    }
}


