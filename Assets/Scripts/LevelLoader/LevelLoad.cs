using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
//Load JSON file
using System.IO;

namespace FateNotes.LevelLoader{
	
	public class LevelLoad : ScriptableObject{
		public class TileLevel{
			public List<Tile> tiles {get; set;}
			public int laneCount {get; set;}
			public string songTrack {get; set;}
			public TileLevel(){
				tiles = new List<Tile>();
			}
		}
		public class Tile{
			public int tileLane {get; set;}
			public float tileSpawn{get; set;}
			public float tileHit{get; set;}
			public float tileMiss{get; set;}
			public Tile(){
				tileLane = 0;
				tileSpawn = 0;
				tileHit = 0;
				tileMiss = 0;
			}

		}

		public void InjectFakeLevelsIntoPersistentDir(){
			//TODO remove this 

			string _level1= "{\"tiles\":[{\"tileLane\": 0,\"tileSpawn\":0.50,\"tileHit\":0.20,\"tileMiss\":1.30}, {\"tileLane\": 0,\"tileSpawn\":1.50,\"tileHit\":0.20,\"tileMiss\":1.30},{\"tileLane\": 3,\"tileSpawn\":1.60,\"tileHit\":0.20,\"tileMiss\":1.30},{\"tileLane\": 1,\"tileSpawn\":1.70,\"tileHit\":0.20,\"tileMiss\":5}],\"laneCount\": 4, \"songTrack\": \"level1.wav\"}";

			//write it to PersistentDataPath	
			string path = Application.persistentDataPath + "/Resources/Levels/level1.json";
			//check if folders till the file exist
			if(!Directory.Exists(Application.persistentDataPath + "/Resources/Levels")){
				Directory.CreateDirectory(Application.persistentDataPath + "/Resources/Levels");
			}
			
			File.WriteAllText(path, _level1);
		}
		public void DebugTileLevelJson(string output){
			//write a blank level
			TileLevel tileLevel = new TileLevel();
			Tile tile = new Tile();
			tile.tileLane = 0;
			tile.tileSpawn = 0;
			tile.tileHit = 0;
			tile.tileMiss = 0;
			tileLevel.tiles.Add(tile);
			string jsonString = JsonSerializer.Serialize(tileLevel);
			//write to file
			string path = Application.persistentDataPath + "/Resources/Levels/"+output+".json";
			File.WriteAllText(path, jsonString);


			
			
		}
		//TODO Make this async
		public TileLevel LoadTileLevel(string levelName, GameObject _game){
			//Aparently you can't use paths like bellow.

			string path = Application.persistentDataPath + "/Resources/Levels/" + levelName + ".json";
			//Like this neither
			//var k = Resources.Load("Levels/"+levelName+".json") as TextAsset;
			//read all text from the file
			string jsonString = File.ReadAllText(path);
			TileLevel tileLevel = JsonSerializer.Deserialize<TileLevel>(jsonString);
			//string audioPath = string.Format("file://{0}", Application.persistentDataPath + "/Resources/Levels/"+);
			//load the audio
			int _samplerate = 48000;
			//var clip = AudioClip.Create(audioPath,_samplerate*60, 2, _samplerate, true);
			//clip.LoadAudioData();
			//clip.LoadAudioData();
			var k = _game.GetComponent<AudioSource>();
			
			Debug.Log("Loaded song track: "+tileLevel.songTrack);
			var clip = Resources.Load("Levels/"+tileLevel.songTrack) as AudioClip;

			k.clip = clip;
			
			return tileLevel;
		}
	}
}