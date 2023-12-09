using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour {
	
	public GameObject ballPrefab;

    public float shootTime = 0.5f;
    private float shootTimer = 0f;
    public float ballDuration = 5f;

    public GameObject[] cannons;
	
    void Start() {
        cannons = GameObject.FindGameObjectsWithTag("Cannon");

        shootTimer = shootTime;
    }

    void Update() {
        if (shootTimer > 0f) {
            shootTimer -= Time.deltaTime;
        }
        else {
            shootTimer = shootTime;
            Shoot();
        }
    }

    void Shoot() {
        GameObject cannon = cannons[Random.Range(0, cannons.Length)];

        GameObject ball = Instantiate(ballPrefab, cannon.transform.position, cannon.transform.rotation);

        Destroy(ball, ballDuration);
    }
}