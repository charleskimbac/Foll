using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameManager GM;

    //holders
    Vector3 spawnLocation;
    Rigidbody rb;
    Vector3 initialSpeed = new Vector3(0, 0, 0);
    Quaternion rot = Quaternion.identity;
    GameObject currentObst;

    //(Score needed to progress, time between spawns, obstacle prefab, location of spawning)
    WaitForSeconds lvl1Time = new WaitForSeconds(1f);
    int lvl1Speed = 10;
    public GameObject obst1;

    int lvl2Speed = 13;
    WaitForSeconds lvl2Time = new WaitForSeconds(.9f);

    int lvl3Speed = 15;
    WaitForSeconds lvl3Time = new WaitForSeconds(.8f);

    int lvl4Speed = 16;
    WaitForSeconds lvl4Time = new WaitForSeconds(.8f);

    void Start() {
        StartCoroutine(spawnObst());
    }

    public IEnumerator spawnObst() {

        currentObst = ObjectPooler.sharedInstance.getPooledObject("obst1");
        if (currentObst != null) {
            //Debug.Log("not null");
            spawnLocation.z = 0;
            spawnLocation.y = 0;
            if (GM.getCamRotation() > 0 && GM.getCamRotation() < 180 || GM.getCamRotation() > -180 && GM.getCamRotation() < 0) {
                spawnLocation.x = Random.Range(-5f, 5f);
            }
            else {
                spawnLocation.x = Random.Range(-9f, 9f);
            }
            rb = currentObst.GetComponent<Rigidbody>();
            currentObst.transform.position = spawnLocation;
            currentObst.transform.rotation = rot;
            currentObst.SetActive(true);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            initialSpeed.y = getSpeed();
            rb.AddForce(initialSpeed, ForceMode.VelocityChange);
            rb.rotation = Quaternion.Euler(0, 0, 0);
            yield return getTime();
            StartCoroutine(spawnObst());
        }
        //Debug.Log("outside if");
    }

    public int getSpeed() {
        if (GM.getLevel() == 1) {
            return -lvl1Speed;
        }
        else if (GM.getLevel() == 2) {
            return -lvl2Speed;
        }
        else if (GM.getLevel() == 3) {
            return -lvl3Speed;
        }
        else if (GM.getLevel() == 4) {
            return -lvl4Speed;
        }



        return 0;
    }

    public WaitForSeconds getTime() { //spawn time
        if (GM.getLevel() == 1) {
            return lvl1Time;
        }
        else if (GM.getLevel() == 2) {
            return lvl2Time;
        }
        else if (GM.getLevel() == 3) {
            return lvl3Time;
        }
        else if (GM.getLevel() == 4) {
            return lvl4Time;
        }




        return lvl1Time;
    }

}
