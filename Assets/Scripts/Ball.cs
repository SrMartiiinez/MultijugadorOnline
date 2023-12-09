using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    void Update() {
        transform.Translate(Vector3.forward * 8f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}