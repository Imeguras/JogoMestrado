
using System;

namespace FateNotes{
	//singleton
	public class GameState{
		private static GameState instance;
		public static GameState Instance{
			get{
				if(instance == null){
					instance = new GameState();
				}
				return instance;
			}
		}
		public event EventHandler<float> ValueChanged;
		
		
		public int score;
		public int[] levelsUnlocked;
		public int[] levelsCompleted;
		public int highScore;
		public bool isGamePaused;
		

		public float Volume{get{
			return Volume;
		}
		set{
			Volume = value;
			OnValueChanged(Volume);
		}}
		protected virtual void OnValueChanged(float newValue){
			ValueChanged?.Invoke(this, newValue);
		}


		private GameState(){
			score = 0;
			levelsUnlocked = new int[5];
			levelsCompleted = new int[5];
			highScore = 0;
			isGamePaused = false;
			
		}
	}	
}