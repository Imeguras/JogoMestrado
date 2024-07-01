using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
namespace FateNotes{
	public class MenuPause : MonoBehaviour{
		[SerializeField]
		GameObject buttonMenu;
		[SerializeField]
		GameObject pauseMenu;
		CanvasGroup canvButtonMenu;
		CanvasGroup canvPauseMenu;
		[SerializeField]
		GameObject settingsMenu;
		[SerializeField]
		AudioMixer audioMixer;
		
		private void Awake(){
			canvButtonMenu = buttonMenu.GetComponent<CanvasGroup>();
			canvPauseMenu = pauseMenu.GetComponent<CanvasGroup>();
			
			canvButtonMenu.alpha = 1;
			canvPauseMenu.alpha = 0;
			//subscribe to OnChangeVolume
			GameState.Instance.ValueChanged += (sender, newValue) => setVolume(newValue);

		} 
		public void PauseGame(){
			if(!GameState.Instance.isGamePaused){
				StartCoroutine(SlowToState());
			}
		}
		public void ResumeGame(){
			
			if(GameState.Instance.isGamePaused){
				StartCoroutine(SlowToState());
			}
			
		}
		public void Settings(){
			//set transparency for pauseMenu to 0
			canvPauseMenu.alpha = 0;
			//enable settings menu
			settingsMenu.SetActive(true);

			
		}
		public void closeSettings(){
			//set transparency for pauseMenu to 1
			canvPauseMenu.alpha = 1;
			//disable settings menu
			settingsMenu.SetActive(false);
		}
		public void setVolume(float volume){
			audioMixer.SetFloat("Volume", volume);

		}
		
		IEnumerator SlowToState(){
			
			if(GameState.Instance.isGamePaused){
				settingsMenu.SetActive(false);

				buttonMenu.SetActive(true); 
				//set transparency for buttonMenu
				
				canvButtonMenu.alpha = 0;
				canvPauseMenu.alpha = 1;

				GameState.Instance.isGamePaused = false;
				for (int i = 2; i < 30; i++){
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
				settingsMenu.SetActive(false);
				pauseMenu.SetActive(true);
				GameState.Instance.isGamePaused = true;
				for (int i = 2; i < 30; i++){
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