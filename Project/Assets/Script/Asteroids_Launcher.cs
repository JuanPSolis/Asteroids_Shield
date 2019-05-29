using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids_Launcher : MonoBehaviour {

	public GameObject[] Asteroids;
	public GameObject[] PowerUps;
	public float Invoke_Asteroid_Time = 3f;
	public float Faster_Invoke_Asteroid_Time = 1f;
	public float Invoke_PowerUp_MinTime = 10f;
	public float Invoke_PowerUp_MaxTime = 50f;
	public float Distance_To_Ship = 7f; 
	public float Easy_Level_Finis_Time = 60f;
	public float Hard_Level_Start_Time = 180f;
	public bool easy_Level = false;
	public bool medium_Level = false;
	public bool hard_Level = false;
	float Timer;

	//----------------------------------------------------------------------

	void Start () {
		Invoke ("Appear", Invoke_Asteroid_Time);
		Invoke ("PowerUpAppear", Random.Range (Invoke_PowerUp_MinTime, Invoke_PowerUp_MaxTime));
		Timer = 0f;
	}

	void FixedUpdate () {
		Timer += Time.fixedDeltaTime;
	}

	//----------------------------------------------------------------------

	void Appear() {
		Vector2 Random_Position = Random.onUnitSphere;
		GameObject Instantiate_Asteroid = Instantiate (Asteroids [Random.Range (0, Asteroids.Length)]);
		Instantiate_Asteroid.transform.position = Random_Position.normalized * Distance_To_Ship;
		if (Timer < Easy_Level_Finis_Time) {
			Invoke("Appear", Invoke_Asteroid_Time *2);
			easy_Level = true;
			medium_Level = false;
			hard_Level = false;
		} else if (Timer > Hard_Level_Start_Time) {
			Invoke ("Appear", Invoke_Asteroid_Time /2);
			easy_Level = false;
			medium_Level = false;
			hard_Level = true;
		} else {
			Invoke("Appear", Invoke_Asteroid_Time);
			easy_Level = false;
			medium_Level = true;
			hard_Level = false;
		}
	}

	void PowerUpAppear () {
		Vector2 Random_Position = Random.onUnitSphere;
		GameObject Instantiate_PowerUp = Instantiate (PowerUps [Random.Range (0, PowerUps.Length)]);
		Instantiate_PowerUp.transform.position = Random_Position.normalized * Distance_To_Ship;
		Invoke ("PowerUpAppear", Random.Range (Invoke_PowerUp_MinTime, Invoke_PowerUp_MaxTime));
	}
		
	IEnumerator FasterTime () {
		float Fast_Time = Invoke_Asteroid_Time;
		Invoke_Asteroid_Time = Faster_Invoke_Asteroid_Time;
		yield return new WaitForSeconds (10);
		Invoke_Asteroid_Time = Fast_Time;
	}
}
