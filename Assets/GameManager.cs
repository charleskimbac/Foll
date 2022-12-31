using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

//move lvl speed, etc to here instead of mapmanager or wtv
public class GameManager : MonoBehaviour {
    public GameObject loseTextObj;
    int level;
    public TMP_Text scoreText;

    //fading cover
    public GameObject coverObj;
    private Image cover;
    private Color currentFade;
    private int fadeTime;
    private bool fading;
    private int targetAlpha;
    private float currentVelocity = 0;

    //lasers
    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;
    public GameObject laser4;
    private GameObject chosen;
    private Vector3 laserLocation = new Vector3(0, 0, 0);
    private float xMaxBound;//bounds of the location where the prefab will spawn
    private float yMaxBound;
    private float xMinBound;
    private float yMinBound;


    public int score;

    //LEVELS (based on score; 2 before)
    int lvl2 = 8;
    int lvl3 = 13;


    //rotatingCam
    public Camera cam;
    public RectTransform rt;
    //Quaternion toAngle_90 = Quaternion.Euler(0, 0, 90);
    public Quaternion currentAngle;
    Quaternion toAngle_0 = Quaternion.Euler(0, 0, 0);

    Quaternion toAngle;
    float timeElapsed = 0;
    float rotationSpeed = 0;

    bool rotatingCam = false;
    char rotate = ' ';
    Quaternion startingAngle;
    bool rotatingSign = false;

    WaitForSeconds rotationTime = new WaitForSeconds(2f);
    //

    void Awake() {
        //CHANGE DEFAULTS!
        score = 12;
        level = 2;
        loseTextObj.SetActive(false);
        rt.gameObject.SetActive(false);
    }

    private void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (rotatingCam) {
            if (rotate == 'r') {
                //Debug.Log("right rotatingCam");
                //currentAngle = Quaternion.Euler(0, 0, currentAngle.eulerAngles.z + .01f); //ensure right turn
                rightRotation();
            }
            else if (rotate == 'l') {
                //Debug.Log("left rotatingCam");
                //currentAngle = Quaternion.Euler(0, 0, currentAngle.eulerAngles.z - .01f); //ensure left turn
                leftRotation();
            }
            convertRotation();
        }

        if (fading) {
            fadeCover();
        }

        if (rotatingSign) {
            rotateSign();
        }
    }

    public void loseTextOn() {
        loseTextObj.SetActive(true);
    }

    public void loseTextOff() {
        loseTextObj.SetActive(false);
    }

    public int getLevel() {
        return level;
    }

    public void addScore() { //and level change
        score++;
        scoreText.text = "Score: " + score;

        if (score == lvl2) {
            level = 2;
            Debug.Log("LEVEL 2");
        }
        else if (score == lvl3) {
            StartCoroutine(doLVL3());
        }
    }

    IEnumerator doLVL3() {
        level = 3;
        Debug.Log("LEVEL 3");
        StartCoroutine(preRotation('r', 179.9f));
        yield return new WaitForSeconds(15f);
        StartCoroutine(preRotation('l', -179.9f));
    }

    IEnumerator doLVL4() {
        level = 4;
        Debug.Log("LEVEL 4");
        preFade(4);
        yield return null;
    }

    public IEnumerator preRotation(char direction, float degrees) { //use this overload instead -- speed is sign duration
        Debug.Log("start rotate all");
        preRotateSign(.25f / degrees + .04f);


        yield return new WaitForSeconds(1.6f);
        timeElapsed = 0;
        currentAngle = cam.transform.rotation;
        rotate = direction;
        toAngle = Quaternion.Euler(0, 0, currentAngle.eulerAngles.z + degrees);
        rotationSpeed = Math.Abs(.25f / degrees);

        rotatingCam = true;
    }

    public void rightRotation() {
        currentAngle = Quaternion.Slerp(currentAngle, toAngle, timeElapsed * rotationSpeed);
        cam.transform.rotation = currentAngle;
        //Debug.Log("doRotation()");

        if (currentAngle.eulerAngles.z >= toAngle.eulerAngles.z - .7f) { //.7 = getting to exact euler.z toAngle takes too long
            cam.transform.rotation = Quaternion.Euler(0, 0, toAngle.eulerAngles.z + .1f); //.1 from preRotation argument (bc of how Lerp works)
            rotatingCam = false;
            Debug.Log("exiting right rotation");
        }
        timeElapsed += Time.deltaTime;
    }

    public void leftRotation() {
        currentAngle = Quaternion.Slerp(currentAngle, toAngle, timeElapsed * rotationSpeed);
        cam.transform.rotation = currentAngle;
        //Debug.Log("doRotation()");

        if (currentAngle.eulerAngles.z <= toAngle.eulerAngles.z + .7f) { //.7 = getting to exact euler.z toAngle takes too long
            cam.transform.rotation = Quaternion.Euler(0, 0, toAngle.eulerAngles.z - .1f); //.1 from preRotation argument (bc of how Lerp works)
            rotatingCam = false;
            Debug.Log("exiting left rotation");
        }
        timeElapsed += Time.deltaTime;
    }

    public float getCamRotation() {
        return currentAngle.eulerAngles.z;
    }

    void convertRotation() { //if over 360 or under -360, convert
        while (currentAngle.z > 360) {
            currentAngle.z -= 360;
        }
        while (currentAngle.z < -360) {
            currentAngle.z += 360;
        }
    }

    void preFade(int fadeTime) { //call this if u want to fade; timeFade from 0-1
        this.fadeTime = fadeTime;
        fading = true;
    }

    void fadeCover() { //never directly call this except in update
        if (cover.color.a == 1) {
            targetAlpha = 0;
        }
        else {
            targetAlpha = 1;
        }

        currentFade.a = Mathf.SmoothDamp(cover.color.a, targetAlpha, ref currentVelocity, fadeTime);
        if (currentFade.a > 1) {
            currentFade.a = 1;
            fading = false;
            Debug.Log("exiting fading");
        }
        cover.color = currentFade;
    }

    void preLaser() {
        switch (UnityEngine.Random.Range(1, 5)){
          case 1:
            chosen = laser1; break;
          case 2:
            chosen = laser2; break;
          case 3:
            chosen = laser3; break;
          case 4:
            chosen = laser4; break;
        }

        laserLocation.x = UnityEngine.Random.Range(xMinBound, xMaxBound);
        laserLocation.y = UnityEngine.Random.Range(yMinBound, yMaxBound);

        //obj pool here!!!!!
        chosen = ObjectPooler.sharedInstance.getPooledObject("laser");
        chosen.transform.position = laserLocation;
    }

    void preRotateSign(float speed) {
        Debug.Log("start rotate sign");
        rt.gameObject.SetActive(true);
        timeElapsed = 0;
        currentAngle = rt.rotation;
        toAngle = Quaternion.Euler(0, 0, rt.rotation.eulerAngles.z + 179.9f);
        rotationSpeed = speed;

        rotatingSign = true;
    }

    void rotateSign() {
        currentAngle = Quaternion.Slerp(currentAngle, toAngle, timeElapsed * rotationSpeed);
        rt.rotation = currentAngle;
        //Debug.Log(toAngle.eulerAngles + "..." + currentAngle.eulerAngles + "..." + rt.rotation.eulerAngles);

        if (currentAngle.eulerAngles.z >= toAngle.eulerAngles.z - .8f) { //.7 = getting to exact euler.z toAngle takes too long
            rotatingSign = false;
            rt.gameObject.SetActive(false);
            Debug.Log("exiting sign rotation");
        }
        timeElapsed += Time.deltaTime;
    }
}

//TODO - refine rotation sign, make reverse sign movement - MAKE THE FIRST ROTATION SIGN GO THE OTHER WAY!