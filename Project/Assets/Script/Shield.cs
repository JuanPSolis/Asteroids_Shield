using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public float Velocity_Rotation;
	AudioSource mi_Audio;

	void Start () {
		mi_Audio = GetComponent <AudioSource> ();
		if (Game_Manager.instance.mi_Fx.mute == true) {
			mi_Audio.mute = true;
		} else {
			mi_Audio.mute = false;
		}
	}

	void Update () {
		transform.RotateAround (transform.position, Vector3.up, Velocity_Rotation);
	}
}
