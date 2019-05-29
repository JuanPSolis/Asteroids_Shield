using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Manager : MonoBehaviour {

	Game_Manager mi_Manager;
	public GameObject Creditos;
	Animator Creditos_Animator;

	void Start () {
		mi_Manager = Game_Manager.instance;
		Creditos_Animator = Creditos.GetComponent <Animator> ();
	}
	void Update () {
		if (mi_Manager.En_Tutorial) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D Hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (Hit.collider != null) {
					Transform Hited = Hit.transform;
					if (Hited.tag == "Tutorial") {
						mi_Manager.mi_Fx.PlayOneShot (mi_Manager.Fx_Button);
						Creditos_Animator.SetBool ("Credit_On", false);
					}
					if (Hited.tag == "Credits") {
						mi_Manager.mi_Fx.PlayOneShot (mi_Manager.Fx_Button);
						Creditos_Animator.SetBool ("Credit_On", true);
					}
					if (Hited.tag == "Back") {
						mi_Manager.mi_Fx.PlayOneShot (mi_Manager.Fx_Button);
						SceneManager.LoadSceneAsync (0);
						mi_Manager.En_Tutorial = false;
					} 
				}
			}
		}
	}
}
