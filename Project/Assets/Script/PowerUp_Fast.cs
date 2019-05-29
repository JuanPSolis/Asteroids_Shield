using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Fast : MonoBehaviour {

	Vector3 Position;
	GameObject Objetive;
	Vector3 Direction;
	public float Velocity;
	public float Velocity_Rotation;
	public GameObject Explo_shield;
	Asteroids_Launcher Launcher;

	//----------------------------------------------------------------------

	void Start () {
		Objetive = GameObject.FindGameObjectWithTag ("Player");
	}	

	void Update () {
		if (Objetive.transform.position.x > transform.position.x) {
			transform.RotateAround (transform.position, Vector3.down, Velocity_Rotation);
		} else if (Objetive.transform.position.x < transform.position.x) {
			transform.RotateAround (transform.position, Vector3.up, Velocity_Rotation);
		}
		if (Objetive.transform.position.y > transform.position.y) {
			transform.RotateAround (transform.position, Vector3.right, Velocity_Rotation);
		} else if (Objetive.transform.position.y < transform.position.y) {
			transform.RotateAround (transform.position, Vector3.left, Velocity_Rotation);
		}
		if (Objetive != null) {
			Position = transform.position;
			Direction = (Objetive.transform.position - Position).normalized;
			transform.position += Direction * Time.deltaTime * Velocity;
		} else {
			Destroy (this.gameObject);
		}
	}

	//----------------------------------------------------------------------

	public void UfoDown () {
		Destroy (this.gameObject);
	}
	//----------------------------------------------------------------------

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			Launcher = FindObjectOfType <Asteroids_Launcher> ();
			Launcher.StartCoroutine ("FasterTime");
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider col) {
		Instantiate (Explo_shield, gameObject.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
		Game_Manager.instance.mi_Fx.PlayOneShot (Game_Manager.instance.Fx_Asteroid_Destroy);
	}
}
