using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollected : MonoBehaviour {
    [SerializeField] private float animationLength;
    private SpriteRenderer sp;

    IEnumerator Collection() {
        var countDown = animationLength;
        var pos = transform.position;
        var color = sp.color;
        while (countDown >= 0) {
            countDown -= Time.deltaTime;

            pos.y += 0.01f;
            transform.position = pos;

            color.a = countDown;
            sp.color = color;

            yield return default;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<ICollector>()?.OnCollect() != null) {
            sp = GetComponent<SpriteRenderer>();
            StartCoroutine(Collection());
        }
    }
}
