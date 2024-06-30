using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace FateNotes{
	public class MenuPause : MonoBehaviour{
		[SerializeField]
		GameObject buttonMenu;
		[SerializeField]
		GameObject pauseMenu;
		CanvasGroup canvButtonMenu;
		CanvasGroup canvPauseMenu;
		private void Awake(){
			canvButtonMenu = buttonMenu.GetComponent<CanvasGroup>();
			canvPauseMenu = pauseMenu.GetComponent<CanvasGroup>();
			canvButtonMenu.alpha = 1;
			canvPauseMenu.alpha = 0;
			
		} 
		public void PauseGame(){
			StartCoroutine(SlowToState());
		}
		
		IEnumerator SlowToState(){
			
			if(GameState.Instance.isGamePaused){
				buttonMenu.SetActive(true); 
				//set transparency for buttonMenu
				
				canvButtonMenu.alpha = 0;
				canvPauseMenu.alpha = 1;

				GameState.Instance.isGamePaused = false;
				for (int i = 1; i < 10; i++){
					Time.timeScale = 1-(1/i);
					canvButtonMenu.alpha = 1-(1/i);
					canvPauseMenu.alpha = (1/i);

					yield return new WaitForSeconds(1/i);
					
				}
				Time.timeScale = 1;
				canvPauseMenu.alpha = 0;
				canvButtonMenu.alpha = 1;

				pauseMenu.SetActive(false);
			}else{
				pauseMenu.SetActive(true);
				GameState.Instance.isGamePaused = true;
				for (int i = 1; i < 10; i++){
					Time.timeScale =(1/i);
					canvButtonMenu.alpha =(1/i);
					canvPauseMenu.alpha = 1-(1/i);
					yield return new WaitForSeconds(1/i);
				}
				Time.timeScale = 0;
				buttonMenu.SetActive(false);
			}
		
		}
		
	}
	
}