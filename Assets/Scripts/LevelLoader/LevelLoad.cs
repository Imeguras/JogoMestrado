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
			string path = Application.dataPath + "/Resources/Levels/"+output+".json";
			File.WriteAllText(path, jsonString);


			
			
		}
		//TODO Make this async
		public TileLevel LoadTileLevel(string levelName){
			string path = Application.dataPath + "/Resources/Levels/" + levelName + ".json";

			string jsonString = File.ReadAllText(path);
			TileLevel tileLevel = JsonSerializer.Deserialize<TileLevel>(jsonString);

			return tileLevel;
		}
	}
}