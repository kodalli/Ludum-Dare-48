using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>, IDamageable {

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    #region State Variables 
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
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

    #endregion

    #region Other Variables
    private Vector2 previousVelocity;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    private float countDown;

    #endregion

    #region Unity Callback Functions
    private void Awake() {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }
    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();

        FacingDirection = 1;

        currentHealth = maxHealth;

        StateMachine.Initialize(IdleState);
    }
    private void Update() {
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

    public void SetVelocityX(float velocity) {
        previousVelocity.Set(velocity, CurrentVelocity.y);
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
            }
        }

        if (countDown >= 0) countDown -= Time.deltaTime;
    }

    public void TakeDamage(float damage) {

        Debug.Log(damage);

        currentHealth -= (int)damage;

        if (currentHealth <= 0) {
            Destroy(this.gameObject, 0.1f);
        }
    }
}
