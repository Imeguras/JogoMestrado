using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class CameraFollow : MonoBehaviour{
	private Vector3[] averageHeading;
	private Vector3 average;
	[SerializeField]
	private double offsetX;
	[SerializeField]
	private double offsetY; 
	GameObject playerPos;
	[SerializeField]
	float speed =5f;
    // Start is called before the first frame update
    private int index = 0;
	void Awake(){
		averageHeading = new Vector3[60];
        playerPos = GameObject.Find("Player");
    }

	void FixedUpdate(){
		
    


		//rotation is where player is heading to
	}
	
    
}
