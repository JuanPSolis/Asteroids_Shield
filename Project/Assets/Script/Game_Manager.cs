using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance = null;
	public GameObject[] Menu_Objects;
	public GameObject mi_Launcher;
	GameObject[] menu_objects;
	GameObject Active_Launcher;
	public Space_Ship mi_Ship;
	public Text max_score;
	public Text score;
	public Text max_scoreGO;
	public Text scoreGO;
	public bool In_Game;
	public bool En_Tutorial;
	public bool Partida_Empezada;
	public bool Partida_Terminada;
	public bool En_Menu_Start;
	public bool En_Menu_GameOver;
	Animator animator;
	public AudioSource mi_Fx;
	public AudioSource mi_Music;
	public AudioClip Fx_Button;
	public AudioClip Fx_Ship_Damage;
	public AudioClip Fx_Ship_Destroy;
	public AudioClip Fx_Asteroid_Destroy;

	//----------------------------------------------------------------------

	int Max_Score {
		get {
			return PlayerPrefs.GetInt ("Max_Score",0);
		} 
		set {
			if (value > PlayerPrefs.GetInt ("Max_Score",0)) {   
				PlayerPrefs.SetInt ("Max_Score", value);
			}
		}
	}
	public bool Music {
		get {
			//     1=true
			//     0=false
			if (PlayerPrefs.GetInt ("Music",1)==1) {
				return true;
			} else {
				return false;
			}
		}
		set {
			//     1=true
			//     0=false
			if (value==true) {
				PlayerPrefs.SetInt ("Music",1);
			} else {
				PlayerPrefs.SetInt ("Music",0);
			}
		}
	}
	public bool FX {
		get {
			//     1=true
			//     0=false
			if (PlayerPrefs.GetInt ("FX",1)==1) {
				return true;
			} else {
				return false;
			}
		}
		set {
			//     1=true
			//     0=false
			if(value==true) {
				PlayerPrefs.SetInt ("FX",1);
			} else {
				PlayerPrefs.SetInt ("FX",0);
			}
		}
	}

	//----------------------------------------------------------------------

	void Awake () {
		animator = GetComponent<Animator> ();
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (instance);
	}

	void Start () {
		mi_Fx = gameObject.GetComponent <AudioSource> ();
		In_Game = false;
		En_Tutorial = false;
		En_Menu_Start = true;
		En_Menu_GameOver = false;
		Partida_Empezada = false;
		Partida_Terminada = false;
		RevisedAudio ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			Debug.Log (Max_Score);
		}
		if (Input.GetKey (KeyCode.R)) {
			PlayerPrefs.SetInt ("Max_Score", 0);
		}
		if (mi_Ship == null) {
			mi_Ship = FindObjectOfType <Space_Ship> ();
		}

		/*if (max_score == true) {
			max_score.text = "max score: " + Max_Score.ToString ();
		}
		if (max_scoreGO == true) {
			max_scoreGO.text = Max_Score.ToString ();
		}*/

		if (!In_Game && !En_Tutorial) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D Hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (Hit.collider != null) {
					Transform Hited = Hit.transform;
					if (Hited.tag == "Start") {
						mi_Fx.PlayOneShot (Fx_Button);
						if (En_Menu_Start == true) {
							animator.SetBool ("Abrir", true);
							Partida_Empezada = true;
							StartCoroutine ("Gameplay");
						}
					}
					if (Hited.tag == "Music") {
						mi_Fx.PlayOneShot (Fx_Button);
						if (Music == true) {
							Music = false;
						} else {
							Music = true;
						}
						RevisedAudio ();
					}
					if (Hited.tag == "Fx") {
						mi_Fx.PlayOneShot (Fx_Button);
						if (FX == true) {
							FX = false;
						} else {
							FX = true;
						}	
						RevisedAudio ();
					}
					if (Hited.tag == "Menu") {
						mi_Fx.PlayOneShot (Fx_Button);
						if (En_Menu_GameOver == true) {
							scoreGO.gameObject.SetActive (false);
							max_scoreGO.gameObject.SetActive (false);
							animator.SetBool ("GameOver", false);
							Partida_Terminada = false;
							En_Menu_GameOver = false;
							En_Menu_Start = true;
							if (max_score.gameObject == true) {
								StartCoroutine ("maxScoreOn");
							}
						}
					}
					if (Hited.tag == "Info") {
						mi_Fx.PlayOneShot (Fx_Button);
						En_Tutorial = true;
						SceneManager.LoadSceneAsync (1);
					}
					if (Hited.tag == "Exit") {
						mi_Fx.PlayOneShot (Fx_Button);
						Application.Quit ();
					}
				}
			} 
		} else {
			score.text = "score: " + mi_Ship.Score.ToString ();
		}
	}

	//----------------------------------------------------------------------

	IEnumerator maxScoreOn (){
		yield return new WaitForSeconds(2.0f);
		max_score.gameObject.SetActive (true);
	}

	IEnumerator maxScoreGameOverOn (){
		yield return new WaitForSeconds(3.0f);
		max_scoreGO.gameObject.SetActive (true);
		scoreGO.gameObject.SetActive (true);
	}

	//----------------------------------------------------------------------

	IEnumerator Gameplay () {
		for (int i = 0; i < menu_objects.Length; i++) {
			menu_objects [i].SetActive (false);
			yield return new WaitForSeconds (1/2);
		}
		max_score.gameObject.SetActive (false);
		if (Partida_Empezada == true) {
			mi_Ship.gameObject.SetActive (true);
			mi_Ship.Score = 0;
			mi_Ship.Lives = 3;
			mi_Ship.LifeInstance ();
			yield return new WaitForSeconds (3);
			En_Menu_Start = false;
			In_Game = true;
			Active_Launcher = Instantiate (mi_Launcher);
			score.gameObject.SetActive (true);
		}
	}

	//----------------------------------------------------------------------

	public void FirstTime () {
		if (mi_Ship.gameObject.activeInHierarchy) {
			mi_Ship.gameObject.SetActive (false);
		}
		menu_objects = new GameObject[Menu_Objects.Length];
		for (int i = 0; i < Menu_Objects.Length; i++) {
			menu_objects [i] = Instantiate (Menu_Objects [i]);
		}
		max_score.gameObject.SetActive (true);
		max_score.text = "max score: " + Max_Score.ToString ();
	}
		
	IEnumerator  RefurbishMenu() {
		yield return new WaitForSeconds (1/2);
		for (int i = 0; i < menu_objects.Length; i++) {
			menu_objects [i].SetActive (true);
			yield return new WaitForSeconds (1/2);
		}

	}

	public void PlayerDead () {
		mi_Ship.gameObject.SetActive (false);
		Asteroids[] Asteroids = FindObjectsOfType <Asteroids> ();
		for (int i = 0; i < Asteroids.Length; i++) {
			Asteroids [i].UfoDown ();
		}
		PowerUp_Bomb bomb = FindObjectOfType <PowerUp_Bomb> ();
		if (bomb != null) {
			bomb.UfoDown ();
		}
		PowerUP_ExtraLife life = FindObjectOfType <PowerUP_ExtraLife> ();
		if (life != null) {
			life.UfoDown ();
		}
		PowerUp_Fast fast = FindObjectOfType <PowerUp_Fast> ();
		if (fast != null) {
			fast.UfoDown ();
		}
		animator.SetBool ("Abrir", false);
		Destroy (Active_Launcher);
		score.gameObject.SetActive (false);
		In_Game = false;
		Partida_Empezada = false;
		Partida_Terminada = true;
		En_Menu_GameOver = true;
		Max_Score = mi_Ship.Score;
		scoreGO.text = mi_Ship.Score.ToString ();
		max_scoreGO.text = Max_Score.ToString ();
		max_score.text = "max score: " + Max_Score.ToString ();
		StartCoroutine ("RefurbishMenu");
		animator.SetBool ("GameOver", true);
		StartCoroutine ("maxScoreGameOverOn");
	}

	void RevisedAudio () {
		if (Music == true) {
			mi_Music.mute = false;
		} else {
			mi_Music.mute = true;
		}
		if (FX == true) {
			mi_Fx.mute = false;
		} else {
			mi_Fx.mute = true;
		}
	}
}
