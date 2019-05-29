using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Auxiliar : MonoBehaviour {

	public Text max_score;
	public Text score;
	public Text max_scoreGO;
	public Text scoreGO;
	Game_Manager mi_manager;

	void Start () {
		mi_manager = Game_Manager.instance;
		mi_manager.max_score = max_score;
		mi_manager.score = score;
		mi_manager.max_scoreGO = max_scoreGO;
		mi_manager.scoreGO = scoreGO;
		mi_manager.FirstTime ();
	}
}
