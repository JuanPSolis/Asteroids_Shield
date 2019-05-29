using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Sprite : MonoBehaviour {

	Game_Manager mi_Manager;
	SpriteRenderer mi_Rend;
	public Sprite On;
	public Sprite Off;

	//----------------------------------------------------------------------

	void Start () {
		mi_Manager = Game_Manager.instance;
		mi_Rend = GetComponent <SpriteRenderer> ();
	}

	void Update () {
		if (mi_Manager.Music == true) {
			mi_Rend.sprite = On;
		} else {
			mi_Rend.sprite = Off;
		}
	}
}
