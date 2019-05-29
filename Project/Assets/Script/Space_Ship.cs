using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Space_Ship : MonoBehaviour {

	Game_Manager mi_Manager;
	public float Lives;
	public GameObject[] Shields;
	GameObject mi_Shield;
	public float Max_Lives;
	public GameObject[] lifes;
	Vector3 Start_Position;
	public float Velocity_Rotation;
	public Animator mi_Anim;
	public GameObject Part_Exposion;
	public GameObject Part_ExtraLife;
	[HideInInspector]
	public int Score;

	//----------------------------------------------------------------------

	void Start () {
		mi_Manager = Game_Manager.instance;
		Start_Position = transform.position;
	}

	void Update () {
		if (mi_Manager.Partida_Empezada) {
			transform.RotateAround (transform.position, Vector3.down, Velocity_Rotation);
		}
		if (mi_Manager.In_Game) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D Hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (Hit.collider != null) {
					Transform Hited = Hit.transform;
					if (mi_Shield == null) {
						if (Hited.tag == "Ice") {
							mi_Shield = Instantiate (Shields [0], transform.position, Quaternion.Euler (-90f, 0, 0));
						}
						if (Hited.tag == "Fire") {
							mi_Shield = Instantiate (Shields [1], transform.position, Quaternion.Euler (-90f, 0, 0));
						}
						if (Hited.tag == "Steel") {
							mi_Shield = Instantiate (Shields [2], transform.position, Quaternion.Euler (-90f, 0, 0));
						}
					}
				}
			} else if (Input.GetMouseButtonUp (0)) {
				Destroy (mi_Shield);
			}
		} 
	}
		
	//----------------------------------------------------------------------

	public void  LifeInstance () {
		for (int i = 0; i < 3; i++) {
			if (lifes [i].activeSelf == false) {
				lifes [i].gameObject.SetActive (true);
			}
		}
	}

	void LifeCount () {
		for (int i = 0; i < lifes.Length; i++) {
			if (i + 1 > Lives) {
				lifes [i].gameObject.SetActive (false);
			}
		}
	}
		
	void ScorePlus () {
		Score += 5;
	}

	void ShipDown () {
		Instantiate (Part_Exposion, gameObject.transform.position, Quaternion.identity);
		mi_Manager.mi_Fx.PlayOneShot (mi_Manager.Fx_Ship_Destroy);
		transform.position = Start_Position;
		if (mi_Shield != null) {
			Destroy (mi_Shield);
		}
		mi_Manager.PlayerDead ();
	}

	public void ExtraLifeAdd () {
		if (Lives < Max_Lives) {
			Lives += 1;
			Instantiate (Part_ExtraLife, gameObject.transform.position, Quaternion.identity);
			for (int i = 0; i < Lives; i++) {
				if (i < Lives) {
					lifes [i].gameObject.SetActive (true);
				}
			}
		} else {
			Score += 20;
		}
	}

	//----------------------------------------------------------------------

	void OnCollisionEnter (Collision col) {
		if (col.transform.tag != "PowerUp") {
			if (Lives > 0) {
				Lives = Lives - 1f;
				if (Lives <= 0f) {
					ShipDown ();
				} else {
					mi_Manager.mi_Fx.PlayOneShot (mi_Manager.Fx_Ship_Damage);
				}
				LifeCount ();
			}
		}
	}
}
