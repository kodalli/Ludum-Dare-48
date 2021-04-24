using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {
    private ObjectPooler objectPooler;

    void Start() {
        objectPooler = ObjectPooler.Instance;
    }

    public GameObject FireBullet(string tag, Vector3 spawnpt, Quaternion quaternion) {
        return objectPooler.SpawnFromPool(tag, spawnpt, quaternion);
    }
}
