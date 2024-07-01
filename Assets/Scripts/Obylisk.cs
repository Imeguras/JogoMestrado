using System.Collections;
using System.Collections.Generic;
using FateNotes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obylisk : MonoBehaviour
{

	[SerializeField]
	private int level; 
	private bool target=false; 
    // Start is called before the first frame update
    void Start(){
		//on receive message gameobject
		//load level
		
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Player"){

			Debug.Log("Player is touching obelisk "+level);
			CharacterMovement.isTouchingObelisk = true;
			target=true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.tag=="Player"){
			CharacterMovement.isTouchingObelisk = false;
			target=false;
		}
	}

	void LoadLevel(){
		
		Debug.Log("Loading Level "+level);
		SceneManager.LoadScene(level);
	}
}
