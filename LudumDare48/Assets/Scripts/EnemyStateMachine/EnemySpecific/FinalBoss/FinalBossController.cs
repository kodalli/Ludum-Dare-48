using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : Singleton<FinalBossController>, IDamageable {

    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Vector2 hitForce;
    [SerializeField] private float maxHealth;
    [SerializeField] private float contactDamage;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private float drag;
    [SerializeField] private float speed;
    [SerializeField] private float attackTime;

    private Rigidbody2D rb;
    private Animator anim;
    private float currentHealth;
    private bool isDetected;
    private bool bossFightStarted;
    private bool transformationDone;
    private bool isWalking;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bossFightStarted = false;
        transformationDone = false;
        isWalking = false;
    }

    private void Update() {
        if (isWalking && !bossFightStarted) {
            rb.velocity = Vector2.right * speed;
        } else if (isWalking && transformationDone && bossFightStarted && !CheckPlayerInRange()) {
            rb.velocity = Player.Instance.transform.position.x > transform.position.x ? Vector2.right * speed : -Vector2.right * speed;
        }
    }

    private void FixedUpdate() {
        if (Player.Instance.transform.position.x > transform.position.x && !bossFightStarted) {
            anim.SetBool("idle", false);
            if (!isWalking) {
                anim.SetTrigger("getup");
                isWalking = true;
            }
        } else if (!bossFightStarted) {
            anim.SetBool("idle", true);
        } else if (transformationDone && bossFightStarted && CheckPlayerInRange()) {
            StartAttackPhase();
        }
    }


    public void MoveToNextLocation() {

    }


    public void StartBossFight() {
        bossFightStarted = true;
        anim.SetTrigger("ultimateform");
    }

    public void StartAttackPhase() {
        StartCoroutine(DoAttack());
        // Do attack, wait for animation done, then walk
    }
    // public void OnAttackDone() {
    //     anim.SetBool("bigwalk", true);
    //     isWalking = true;
    // }
    public void OnGetUpDone() {
        anim.SetBool("littlewalk", true);
        isWalking = true;
    }
    IEnumerator DoAttack() {
        anim.SetBool("bigwalk", false);
        isWalking = false;
        anim.SetTrigger("bigattack");
        var countDown = attackTime;
        while (countDown > 0f) {
            countDown -= Time.deltaTime;
            // do attack shoot something
            yield return default;
        }
        isWalking = true;
        anim.SetBool("bigwalk", true);
    }

    public void OnTransformationDone() {
        transformationDone = true;
        anim.SetBool("bigwalk", true);
    }

    public void TakeDamage(float damage, bool hitFromRight) {


        PlayDamageEffect();

        hitForce.x *= hitFromRight ? -1 : 1;

        rb.AddForce(hitForce, ForceMode2D.Impulse);

        currentHealth -= (int)damage;

        CinemachineShake.Instance.ShakeCamera(3f, 0.2f);

        if (currentHealth <= 0) {
            RandomDrop.SpawnRandomDrop(transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.1f);
        }
        Debug.Log("Creedit God" + " " + currentHealth);

        // Debug.Log(EnemyDelegateCount);
    }

    private void PlayDamageEffect() {
        var damageEffect = ObjectPooler.Instance.SpawnFromPool("damageEffect", transform.position, Quaternion.identity);
        var scale = transform.localScale;
        scale.x *= -1;
        damageEffect.transform.localScale = scale;
        damageEffect.transform.position = transform.position;

        damageEffect.GetComponent<ParticleSystem>().Play();
    }


    private void OnCollisionEnter2D(Collision2D other) {

    }

    private void OnTriggerEnter2D(Collider2D other) {

    }

    bool CheckPlayerInRange() => Physics2D.Raycast(playerCheck.position, -Vector2.right, playerCheckDistance, whatIsPlayer);

    public void TakeDamage(float damage) {
        throw new System.NotImplementedException();
    }
}
