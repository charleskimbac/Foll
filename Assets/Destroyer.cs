using UnityEngine;

public class Destroyer : MonoBehaviour
{

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        other.gameObject.transform.parent.gameObject.SetActive(false);
        //Debug.Log("set inactive");
    }
}