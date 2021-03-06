﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hover.Core.Utils {

	/*================================================================================================*/
	[ExecuteInEditMode]
	public class HoverSceneLoader : MonoBehaviour {

		public string SceneFolderPath = "Hover/InputModules/Example/Scenes/";
		public string SceneName = "HoverInputModule-Example";
		public bool ClickToReloadScene = false;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void Awake() {
			if ( Application.isEditor ) {
				ClickToReloadScene = true;
			}
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public void Start() {
			if ( !Application.isEditor ) {
				LoadScene();
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update() {
			if ( ClickToReloadScene ) {
				ClickToReloadScene = false;
				LoadScene();
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void LoadScene() {
			if ( LoadSceneForEditor() ) {
				return;
			}

			LoadSceneForRuntime();
		}

		/*--------------------------------------------------------------------------------------------*/
		private bool LoadSceneForEditor() {
#if UNITY_EDITOR
			if ( Application.isPlaying ) {
				return false;
			}

			string fullPath = Application.dataPath+"/"+SceneFolderPath+SceneName+".unity";

			UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
				fullPath, UnityEditor.SceneManagement.OpenSceneMode.Additive);

			Debug.Log("Loaded scene for editor: "+fullPath, this);
			return true;
#else
			return false;
#endif
		}

		/*--------------------------------------------------------------------------------------------*/
		private void LoadSceneForRuntime() {
			if ( SceneManager.GetSceneByName(SceneName).IsValid() ) {
				Debug.Log("Scene already loaded: "+SceneName, this);
				return;
			}

			string scenePathAndName = SceneFolderPath+SceneName;

			if ( SceneManager.GetSceneByName(scenePathAndName).IsValid() ) {
				Debug.Log("Scene already loaded: "+scenePathAndName, this);
				return;
			}

			SceneManager.LoadScene(scenePathAndName, LoadSceneMode.Additive);
			Debug.Log("Loaded scene: "+scenePathAndName, this);
		}

	}

}
