using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AND RE-ENABLING GOAL (disables so player doesnt get more pts for a single wall)
public class WallCollision : MonoBehaviour {
    public GameManager GM;
    public GameObject goal;

    private void Awake() {
        GM = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            //Debug.Log("lost");
            GM.loseTextOn();
        }
    }

    private void OnDisable() {
        goal.SetActive(true);
    }
}
