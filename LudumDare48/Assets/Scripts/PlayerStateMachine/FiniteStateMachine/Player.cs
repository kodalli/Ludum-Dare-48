using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>, IDamageable, ICollector {

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentOxygen;
    [SerializeField] private float maxOxygen;
    [SerializeField] private float oxygenUsageRate;
    [SerializeField] private GameObject damageEffect;

    #region State Variables 
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerDamagedState DamagedState { get; private set; }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private Weapon weapon;

    #endregion

    #region Components
    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public SpriteRenderer SR { get; private set; }

    #endregion

    #region Check Transforms
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    #endregion

    #region Other Variables
    private Vector2 previousVelocity;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public float MaxHP { get => maxHealth; }
    public float MaxOxygen { get => maxOxygen; }
    public float CurrentHP { get => currentHealth; }
    public float CurrentOxygen { get => currentOxygen; }
    private float countDown;
    private float oxygenCountDown;
    #endregion

    #region Unity Callback Functions
    private void Awake() {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        DamagedState = new PlayerDamagedState(this, StateMachine, playerData, "damaged");
    }
    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();

        FacingDirection = 1;

        currentHealth = maxHealth;
        currentOxygen = maxOxygen;

        HUD.Instance.SetHPHUD();
        HUD.Instance.SetOxgyenHUD();


        StateMachine.Initialize(IdleState);
    }
    private void Update() {

        LocalSave.Instance.saveData.oxygen = currentOxygen;
        LocalSave.Instance.saveData.health = currentHealth;

        ConsumeOxygen();

        CurrentVelocity = RB.velocity;
        Shoot();
        StateMachine.CurrentState.LogicUpdate();

    }
    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Animation Triggers
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    public bool CheckIfTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    public bool CheckIfTouchingLedge() => Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);


    public void SetVelocityX(float velocity) {
        previousVelocity.Set(velocity, CurrentVelocity.y);
        RB.velocity = previousVelocity;
        CurrentVelocity = previousVelocity;
    }
    public void SetVelocityY(float velocity) {
        previousVelocity.Set(CurrentVelocity.x, velocity);
        RB.velocity = previousVelocity;
        CurrentVelocity = previousVelocity;
    }

    public void CheckIfShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) {
            Flip();
        }
    }
    private void Flip() {
        FacingDirection *= -1;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Shoot() {
        if (InputHandler.AttackInputs[(int)CombatInputs.PRIMARY]) {
            // stateMachine.ChangeState(player.PrimaryAttackState);
            if (countDown <= 0) {
                countDown = 1f / weapon.FireRate;
                weapon.ShootBullet();
                currentOxygen--;
                HUD.Instance.SetOxgyenHUD();
            }
        }

        if (countDown >= 0) countDown -= Time.deltaTime;
    }

    public void TakeDamage(float damage) {
        StateMachine.ChangeState(DamagedState);

        currentHealth -= (int)damage;

        HUD.Instance.SetHPHUD();

        PlayDamageEffect();

        if (currentHealth <= 0) {
            Destroy(this.gameObject, 0.1f);
        }
        Debug.Log(currentHealth);
    }
    private void ConsumeOxygen() {

        if (oxygenCountDown <= 0) {
            oxygenCountDown = oxygenUsageRate;
            currentOxygen--;
            HUD.Instance.SetOxgyenHUD();
        }

        if (oxygenCountDown >= 0) oxygenCountDown -= Time.deltaTime;
    }

    private void PlayDamageEffect() {
        var scale = transform.localScale;
        scale.x *= -1;
        damageEffect.transform.localScale = scale;

        damageEffect.GetComponent<ParticleSystem>().Play();
    }

    public bool OnCollect() {
        LocalSave.Instance.saveData.gems++;
        return true;
    }

    public void TakeDamage(float damage, bool hitFromRight) {
        throw new System.NotImplementedException();
    }
}
