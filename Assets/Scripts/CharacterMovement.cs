
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
	private PlayerInput playerInput;


    void Start(){
		
		
	}
    void Update(){
        
    }
	public void Move(InputAction.CallbackContext context){
		Debug.Log("Move");
		
		/*Vector2 moveInput = context.ReadValue<Vector2>();
		
		if(context.phase == InputActionPhase.Performed){
			rb.AddForce(new Vector3(moveInput.x*speed, 0, moveInput.y*speed), ForceMode.Impulse);
		}*/
		//animation
		
	}
	public void GyroscopeMove(InputAction.CallbackContext context){
		Debug.Log("GyroscopeMove"+context.ReadValue<Vector3>());
		//Vector3 moveInput = context.ReadValue<Vector3>();
		//rb.AddForce(new Vector3(moveInput.x*speed, 0, moveInput.z*speed), ForceMode.Impulse);
	}
}
}