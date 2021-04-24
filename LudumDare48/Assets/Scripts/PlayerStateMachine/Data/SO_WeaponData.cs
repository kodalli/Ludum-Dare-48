using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Player Data/Weapon Data/Gun")]
public class SO_WeaponData : ScriptableObject {
    public float bulletDamage = 10f;
    public float fireRate = 1f;
    public float bulletSpeed = 5f;
    public Vector2 bulletDirection = Vector2.right;
    public float bulletDestroyDelay = 2f;
}
