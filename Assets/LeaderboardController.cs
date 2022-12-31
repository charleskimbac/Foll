using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;
using System.Collections;


//comment = not implemented 

public class LeaderboardController : MonoBehaviour
{
	public InputField memberID;
	public int ID;
	int maxScores = 20;
	public Text[] entries;
	private float score;
	private int timeAndSpeed;
	//public Text playerScore;
	public Canvas canvas;
	public Text text;
	public GameManager manager;
	public Button submitScoreButton;
	//public Player player;
	private int speed;

	private bool highScoreBool;

	public void Start(){
		LootLockerSDKManager.StartGuestSession((response) => {
			if (response.success){
				Debug.Log("success");
			}
			else {
				Debug.Log("failed");
			}
		});
	}

	public void showScores(){
		LootLockerSDKManager.GetScoreList(ID, maxScores, (response) => {
			if (response.success){
				LootLockerLeaderboardMember[] scores = response.items;

				for(int i = 0; i < scores.Length; i++){
					entries[i].text = ("<b>" + scores[i].rank + ")</b> " + scores[i].member_id + ":\n" + (scores[i].score / 1000) / 1000.0 + "s | " + scores[i].score % 1000 + " speed");
				}

				if (scores.Length < maxScores){
					for (int i = scores.Length; i < maxScores; i++){
						entries[i].text = "<b>" + (i + 1).ToString() + ".</b>  none";
					}
				}
			}
		});
	}

	public void SubmitScore(){
		//score = manager.getTime();
		//speed = player.getSpeed();
		timeAndSpeed = int.Parse("" + score * 1000 + speed); //TIMEANDSPEED -> DIVIDE BY 1000 TO GET SPEED, CAST INT TO /100 THEN DIVIDE BY 1000 FOR SPEED
		Debug.Log(timeAndSpeed);
		LootLockerSDKManager.SubmitScore(memberID.text, timeAndSpeed, ID, (response) => {
			if (response.success){
				Debug.Log("success");
			}
			else {
				Debug.Log("failed");
			}
		});
		submitScoreButton.interactable = false;
	}
}