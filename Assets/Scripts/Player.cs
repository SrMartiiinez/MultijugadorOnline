using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPun, IPunObservable {

    public float speed = 5f;
    public float jumpForce = 5f;
    public int health = 3;

    public Rigidbody rb;

    private Vector3 movement;

    private Vector3 netPosition;
    private Quaternion netRotation;

    public GameObject winPanel;
    public GameObject defeatPanel;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        } else {
            netPosition = (Vector3)stream.ReceiveNext();
            netRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update() {
        if (photonView.IsMine) { 
            movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            rb.velocity = movement * speed;

            if (Physics.Raycast(transform.position, Vector3.down, 1.1f)) {
                if (Input.GetButtonDown("Jump")) {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
            else {
                rb.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
            }
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, netPosition, 10f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, netRotation, 500f * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage) {
        photonView.RPC(nameof(RPC_TakeDamage), photonView.Owner, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(int damage) {
        if (health > 0) {
            health -= damage;
            if (health <= 0) {
                photonView.RPC(nameof(RPC_Die), RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void RPC_Die() {
        if (health > 0) {
            winPanel.SetActive(true);
        }
        else {
            defeatPanel.SetActive(true);
        }
    }
}