using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FateNotes;
public class ScoreBinder : MonoBehaviour{
    // Start is called before the first frame update
	
	TextMeshProUGUI score;
	void Awake(){
		//get TextComponent on TMP
		var score = gameObject.GetComponent<TextMeshProUGUI>();
		score.text = "Score: " + GameState.Instance.score;
		GameState.Instance.ScoreChanged += (sender, newValue) => updateScore(newValue);
	}
	void updateScore(int newValue){
		score.text = "Score: "+ newValue;
	}

}
