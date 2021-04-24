using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    private PlayerState state;
    private float timeSinceFirstAttack;
    [SerializeField] private SO_WeaponData weaponData;
    [SerializeField] private Transform bulletShootPosition;
    public float FireRate { get => weaponData.fireRate; }
    private void Start() {

    }

    public virtual void EnterWeapon() {

    }

    public void ShootBullet() {
        GameObject bullet = Instantiate((GameObject)Resources.Load("Bullet"), bulletShootPosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Damage = weaponData.bulletDamage;
        bullet.GetComponent<Bullet>().Speed = weaponData.bulletSpeed;
        bullet.GetComponent<Bullet>().Direction = Player.Instance.FacingDirection == 1 ? Vector2.right : Vector2.left;
        bullet.GetComponent<Bullet>().DestroyDelay = weaponData.bulletDestroyDelay;
        bullet.GetComponent<Bullet>().Shoot();
    }

    public void InitializeWeapon(PlayerState state) => this.state = state;

}
