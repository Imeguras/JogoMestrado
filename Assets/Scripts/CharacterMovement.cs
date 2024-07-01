
using System;
using Unity.VisualScripting;
using UnityEngine;
//new Input System
using UnityEngine.InputSystem;
//InputDevices Sensors
using UnityEngine.InputSystem.Android;
using UnityEngine.InputSystem.LowLevel;

namespace FateNotes{
public class CharacterMovement : MonoBehaviour{
	[SerializeField]
	private Rigidbody rb;  
	[SerializeField]
	private float speed = 5; 
	[SerializeField]
	private float jumpForce = 20;
	private PlayerInput playerInput;
//	private Vector2 lastMovement;
	public static bool isTouchingObelisk;
	public Vector2 TargetPosition;

	private float target_angle=0;

	void onEnable(){
		
		
		playerInput.ActivateInput();
	}
    void Start(){
		
	}
    void Update(){
        
    }
	
	void FixedUpdate(){
		//lerp angle.Y towards TargetPosition
		/*var actualAngleTarget = Mathf.Atan2(TargetPosition.x, TargetPosition.y) * Mathf.Rad2Deg;
		var angle = Mathf.LerpAngle(transform.eulerAngles.y, actualAngleTarget, 0.1f);
		transform.rotation = Quaternion.Euler(0, angle, 0);*/
		//lerp angle.Y towards target_angle
		var angle = Mathf.LerpAngle(transform.eulerAngles.y, target_angle, 0.05f);
		transform.rotation = Quaternion.Euler(0, angle, 0);
		//if y position is bellow -10, reset position
		if(transform.position.y < -10){
			transform.position = new Vector3(50, 15,50);
			rb.velocity = Vector3.zero;
		}

		
		
	}
	void onDisable(){
	
	}
	
	public void Move(InputAction.CallbackContext context){
		//Debug.Log("Move");

		//apply a red tint
		
		
		Vector2 moveInput = context.ReadValue<Vector2>();
		
		if(context.phase == InputActionPhase.Performed){
			float angleYRadians = this.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

			// Calculate the forward and right direction vectors based on the angleY
			Vector3 forward = new Vector3(Mathf.Sin(angleYRadians), 0, Mathf.Cos(angleYRadians));
			Vector3 right = new Vector3(Mathf.Cos(angleYRadians), 0, -Mathf.Sin(angleYRadians));

			// Calculate the force based on the input vector and the facing direction
			float forceX = moveInput.x * right.x + moveInput.y * forward.x;
			float forceZ = moveInput.x * right.z + moveInput.y * forward.z;
			rb.AddForce(new Vector3(forceX*speed, 0, forceZ*speed), ForceMode.Impulse);
		}

		
		/*
		var lerpX = Mathf.Lerp(lastMovement.x, moveInput.x, 0.6f);
		var lerpZ = Mathf.Lerp(lastMovement.y, moveInput.y, 0.6f);
		lastMovement = new Vector2(lerpX, lerpZ);
		lastMovement.Normalize();
		*/
		

		//animation
		
	}
	public void GyroscopeMove(InputAction.CallbackContext context){
		Debug.Log("GyroscopeMove"+context.ReadValue<Vector3>());
		//Vector3 moveInput = context.ReadValue<Vector3>();
		//rb.AddForce(new Vector3(moveInput.x*speed, 0, moveInput.z*speed), ForceMode.Impulse);
	}
	public void jump(InputAction.CallbackContext context){
		if(context.phase == InputActionPhase.Performed){
			Debug.Log("Jump");
			
			
			//if(!Physics.Raycast(transform.position, Vector3.down, 2f)){
				rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
			//}
			
			
		}
	}
	public void look(InputAction.CallbackContext context){
		
		Vector2 lookInput = context.ReadValue<Vector2>();
		//atan2 for look
		var angle = Mathf.Atan2(lookInput.x, lookInput.y) * Mathf.Rad2Deg;
		Debug.Log("Look"+angle);
		//sum the angle to the current angle
		
		if(angle!=0){
			angle = transform.eulerAngles.y + angle;
			target_angle = angle;
		}
		

	}


	public void interact(InputAction.CallbackContext context){
		if(context.phase == InputActionPhase.Performed){
			Debug.Log("Interact");
			if(isTouchingObelisk){
				//send a notification to all obelisk
				GameObject[] k = GameObject.FindGameObjectsWithTag("Obylisk");
				Debug.Log("Obelisk found "+k.Length);
				foreach(GameObject obelisk in k){
					obelisk.SendMessage("LoadLevel");
				}
				

			}
			
			
		}

	}
}
}