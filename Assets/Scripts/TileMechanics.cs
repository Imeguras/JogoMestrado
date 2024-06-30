using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateNotes.LevelLoader;
using System;
using UnityEngine.InputSystem;

namespace FateNotes{
	public class TileMechanics : MonoBehaviour {
		[SerializeField]
		private BoxCollider boxCollider;
		[SerializeField]
		private Rigidbody rb;
		public int lane=0;
		void onEnable(){
			
			


		}
		void onDisable(){
			TileBehaviour.collisions.Remove(lane);

		}
		void Awake(){
			if(!gameObject.TryGetComponent<BoxCollider>(out boxCollider)){
				boxCollider = gameObject.AddComponent<BoxCollider>();
			}
			boxCollider.isTrigger = true;
			//add Rigidbody
			if(!gameObject.TryGetComponent<Rigidbody>(out rb)){
				rb = gameObject.AddComponent<Rigidbody>();
			}
			rb.isKinematic = true;
			rb.useGravity = false;
			//layer
			gameObject.layer = 8;
		}
		void OnTriggerEnter(Collider other){
			TileBehaviour.collisions.Add(lane);

		}
		
	}
}
