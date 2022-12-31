using UnityEngine;
using Unity.RemoteConfig;
using UnityEngine.UI;

public class RemoteConfig : MonoBehaviour {
    public struct userAttributes { }
    public struct appAttributes { }

    public bool updateGame = false;
    public GameObject fullScreen;
    public GameObject fullAnnouncementText; //actual text obj
    

    public string announcement = "";

    public bool smallNeeded = false;
    public GameObject smallScreen; //small Canvas warning
    public GameObject smallAnnouncementText; //actual text obj

    void Awake() {          //reminder that whenever updateconfig is run then setFullScreen() will also run (bc of below statement)
        ConfigManager.FetchCompleted += updateString;
        ConfigManager.FetchCompleted += setFullScreen;
        ConfigManager.FetchCompleted += setSmallAnnouncement;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        updateConfig();
    }


    void updateString(ConfigResponse response) {
        announcement = ConfigManager.appConfig.GetString("string");
    }

    void setSmallAnnouncement(ConfigResponse response) {
        smallNeeded = ConfigManager.appConfig.GetBool("smallNeeded");


        if (smallNeeded){
            smallScreen.SetActive(true);
            smallAnnouncementText.GetComponent<Text>().text = announcement;
        }
        else {
            //smallAnnouncementText.SetActive(false);
            smallScreen.SetActive(false);
        }
    }



    void setFullScreen(ConfigResponse response) {
        updateGame = ConfigManager.appConfig.GetBool("fullNeeded");

        if (updateGame) {
            fullScreen.SetActive(true);
            fullAnnouncementText.GetComponent<Text>().text = announcement;
        }
        else {
            fullScreen.SetActive(false);
        }
    }

    public void updateConfig() {
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void OnDestroy() {
        ConfigManager.FetchCompleted -= updateString;
        ConfigManager.FetchCompleted -= setFullScreen;
        ConfigManager.FetchCompleted -= setSmallAnnouncement;
    }
}