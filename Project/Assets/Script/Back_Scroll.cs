using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_Scroll : MonoBehaviour {

	public float Scroll_Speed;
	Renderer mi_Rend;

	//----------------------------------------------------------------------

	void Start () {
		mi_Rend = GetComponent <Renderer> ();
	}

	void FixedUpdate () {
		float Offset = Time.time * Scroll_Speed;
		mi_Rend.material.mainTextureOffset = new Vector2 (Offset, 0);
	}
}
