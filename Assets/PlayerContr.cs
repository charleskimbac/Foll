using UnityEngine;

public class PlayerContr : MonoBehaviour {
    const float X_MAX = 10.7f;

    public GameObject player;
    public Rigidbody rb;
    public GameManager GM;
    public bool movementActive = true;
    //
    Vector3 mousePos;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        if (movementActive) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            /* keeping player in bounds
            if (mousePos.x > X_MAX) {
                mousePos.x = X_MAX;
            }
            else if (mousePos.x < -X_MAX) {
                mousePos.x = -X_MAX;
            }
            if (mousePos.y > 11) {
                mousePos.y = 11f;
            }
            else if (mousePos.y < -1.1) {
                mousePos.y = -1.1f;
            }
            */

            transform.position = mousePos;

            /* add support later?
            if (Input.GetKey("d") || Input.GetKey("right")) {
                rb.AddForce(speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                Debug.Log("d pressed");
            }
            if (Input.GetKey("a") || Input.GetKey("left")) {
                rb.AddForce(-speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                Debug.Log("a pressed");
            }
            */
        }
    }
}
