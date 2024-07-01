using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateNotes.LevelLoader;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
namespace FateNotes{

public class TileBehaviour : MonoBehaviour{
	
    LevelLoad loader_instance; 
	LevelLoad.TileLevel tileLevel;
	[SerializeField]
	private GameObject spawn_ref; 
	
	private List<GameObject> lanes;
	//workaround for android 
	
	[SerializeField]
	private float updateVisual;
	private List<Tuple<int, GameObject>> spawnedTiles;
	private Color[] colors =  {Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan, Color.white, Color.black, Color.grey, Color.gray};
	public static List<int> collisions;
	

	void Awake(){
	
		loader_instance = LevelLoad.CreateInstance<LevelLoad>();
		loader_instance.InjectFakeLevelsIntoPersistentDir(); 
		tileLevel= loader_instance.LoadTileLevel("level1", spawn_ref);
		
		//spawn_ref.GetComponent<AudioSource>().Play();
		spawnedTiles = new List<Tuple<int, GameObject>>();
		lanes = new List<GameObject>();
		collisions = new List<int>();
		var laneCount = tileLevel.laneCount;
		
		
		for (int i = 0; i < laneCount; i++){
			GameObject lane = GameObject.CreatePrimitive(PrimitiveType.Cube);
			lane.layer = 8;
			lane.AddComponent<Rigidbody>();
			Rigidbody t = lane.GetComponent<Rigidbody>();
			t.isKinematic = true;
			t.useGravity = false;
			t.constraints = RigidbodyConstraints.FreezeAll;
			//get camera width
			float width = Camera.main.orthographicSize * 2 * Camera.main.aspect;
			float widthRatio = width/laneCount;
			lane.transform.position = new Vector3(-width/2 + widthRatio*i + widthRatio/2, 0, 0);
			//fill in the screen
			lane.transform.localScale = new Vector3(widthRatio, 1, 1);
			lanes.Add(lane);

			
			
		}
		// set color for lanes

		foreach (GameObject lane in lanes){

			lane.GetComponent<MeshRenderer>().material.color = colors[lanes.IndexOf(lane)];
		}

	}
    void Start(){
		if(tileLevel != null){
			this.spawn_ref.GetComponent<AudioSource>().Play();
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
		spawnedTiles.Add(new Tuple<int, GameObject>(tile.tileLane, cube));
		var mech = cube.AddComponent<TileMechanics>();
		mech.lane = tile.tileLane;
	

		cube.transform.position = lanes[tile.tileLane].transform.position;

		//make a child of spawn_ref
		cube.transform.parent = spawn_ref.transform;
		var localpos = cube.transform.localPosition;
		localpos.y = 10; 
		cube.transform.localPosition = localpos;

		cube.transform.localScale = new Vector3(1, 1, 1);
		var checkTime = UnityEngine.Time.time; 
		
		

		yield return StartCoroutine(Fall(cube, tile.tileMiss));
		

	}
	IEnumerator Fall(GameObject target, float timeSeconds){
		
	
		
		Vector3 start = target.transform.position;
		Vector3 end = new Vector3(target.transform.position.x, 0, target.transform.position.z);
		for (int i = 0; i < updateVisual; i++){
			target.transform.position = Vector3.Lerp(start, end, i/(float)updateVisual);
			yield return new WaitForSeconds(timeSeconds/updateVisual);
		}
		
		spawnedTiles.Remove(spawnedTiles.Find(x => x.Item2 == target));

		//disable the cube
		target.SetActive(false);
		yield return null;


	}
	public void tapTile(InputAction.CallbackContext context){
		//only on performed
		if(context.phase != InputActionPhase.Performed){
			return;
		}
		Vector2 tapPosition = context.ReadValue<Vector2>();
		//check if the input comes from keyboard
		if(context.control.device is Keyboard){
			Debug.Log("Keyboard");
		}
		trigger(tapPosition);
		
	}
	void trigger(Vector2 tapPosition){
		
		Ray ray = Camera.main.ScreenPointToRay(tapPosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			//check if it hit any of the lanes
			int laneIndex =0; 
			foreach (GameObject lane in lanes){
				
				if(hit.collider.gameObject == lane){
					
					//check if the lane is colliding with a spawned tile
					if(collisions.Contains(laneIndex)){
						//FateNotes.GameState.Instance.score++;
						GameState.Instance.score+=100; 
						
					}else{
						//grab canvas write lost and then load scene 0 
						SceneManager.LoadSceneAsync(0);
					
					}	
				}
				laneIndex++; 
			}
		}
	}
}

}