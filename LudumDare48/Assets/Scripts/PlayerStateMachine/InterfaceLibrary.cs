public interface IDamageable {
    void TakeDamage(float damage);
}

public interface IPooledObject {
    void OnObjectSpawn();
}