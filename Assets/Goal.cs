using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public GameManager GM;

    private void Awake() {
        GM = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player") {
            GM.addScore();
            this.gameObject.SetActive(false);
        }
    }
}
