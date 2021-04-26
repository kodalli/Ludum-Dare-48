public interface IDamageable {
    void TakeDamage(float damage);
}

public interface IPooledObject {
    void OnObjectSpawn();
}

public interface IIgnoreObject {
    bool IgnoreMe();
}

public interface ICollector {
    void OnCollect();
}