using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//new Input System
using UnityEngine.InputSystem;


public class CharacterMovement : MonoBehaviour{
	[SerializeField]
	private Rigidbody rb;  
	[SerializeField]
	private float speed = 5; 
	
    void Start(){
		
	}
    void Update(){
        
    }
	public void Move(InputAction.CallbackContext context){
		Vector2 moveInput = context.ReadValue<Vector2>();
		
		if(context.phase == InputActionPhase.Performed){
			rb.AddForce(new Vector3(moveInput.x*speed, 0, moveInput.y*speed), ForceMode.Impulse);
		}
		
	}
}
