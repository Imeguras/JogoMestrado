using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateNotes.LevelLoader;

namespace FateNotes{

public class TileBehaviour : MonoBehaviour{
	
    LevelLoad loader_instance; 
	LevelLoad.TileLevel tileLevel;
	[SerializeField]
	private GameObject spawn_ref; 
	[SerializeField]
	private List<GameObject> lanes;
	void Awake(){
		loader_instance = LevelLoad.CreateInstance<LevelLoad>();
		tileLevel= loader_instance.LoadTileLevel("level1");
	}
    void Start(){
		if(tileLevel != null){
			StartCoroutine(BehaviourManager());
		}
		
    }

   
	IEnumerator BehaviourManager(){
		//curent time 
		
		foreach (LevelLoad.Tile tile in tileLevel.tiles){
			//spawn tile
			float timeTillSpawn = tile.tileSpawn;
			
			yield return StartCoroutine(SpawnTile(tile.tileSpawn, tile));
		}
	}
    IEnumerator SpawnTile(float timeSeconds, LevelLoad.Tile tile){
		yield return new WaitForSeconds(timeSeconds);
		//spawn a Cube 
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = lanes[tile.tileLane].transform.position;
		//make a child of spawn_ref
		cube.transform.parent = spawn_ref.transform;
		
		cube.transform.localScale = new Vector3(1, 1, 1);
		cube.AddComponent<Rigidbody>();
		cube.GetComponent<Rigidbody>().useGravity = false;

	}

}

}