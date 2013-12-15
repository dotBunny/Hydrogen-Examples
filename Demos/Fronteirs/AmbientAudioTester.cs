using UnityEngine;
using System.Collections;

public class AmbientAudioTester : MonoBehaviour
{
		public AmbientAudioManager.ChunkAudioSettings chunk1;
		public AmbientAudioManager.ChunkAudioSettings chunk2;
		public AmbientAudioManager.ChunkAudioSettings chunk3;
		public AmbientAudioManager.ChunkAudioItem inside1;
		public AmbientAudioManager.ChunkAudioItem inside2;
		public Color TestColor;
		public float RainIntensity = 0f;
		public float WindIntensity = 0f;
		public float ThunderIntensity = 0f;
		AmbientAudioManager _manager;
		string _activeChunk;
		string _activeInside;
		// Use this for initialization
		void Start ()
		{
				_manager = GetComponent<AmbientAudioManager> ();

				_manager.ChunkSettings = chunk1;
				_activeChunk = "chunk1";
				_manager.StructureSettings = inside1;
				_activeInside = "inside1";
		}
		/*
		 * The normal operation of this would be something like the following, whenver you want to pass a simple color update
		 * 
		 *     _manager.UpdateStackVolumes(color);
		 * 
		 * Lets say you change chunks and want to update the settings
		 *    
		 *     _manager.ChunkAudioSettings = AmbientAudioManager.UpdateChunkAudioSettingsVolumes(yourNewSettingsChunk, yourColor);
		 * 
		 */
		void OnGUI ()
		{
				if (GUI.Button (new Rect (10, 10, 100, 30), "Daytime")) {
						_manager.IsDaytime = true;
				}
				if (GUI.Button (new Rect (120, 10, 100, 30), "Nightime")) {
						_manager.IsDaytime = false;
				}
				if (GUI.Button (new Rect (230, 10, 100, 30), "Underground")) {
						_manager.IsUnderground = true;
				}
				if (GUI.Button (new Rect (340, 10, 100, 30), "Aboveground")) {
						_manager.IsUnderground = false;
				}
				if (GUI.Button (new Rect (10, 50, 100, 30), "Inside")) {
						_manager.IsInsideStructure = true;
				}

				if (GUI.Button (new Rect (120, 50, 100, 30), "Outside")) {
						_manager.IsInsideStructure = false;
				}

				if (GUI.Button (new Rect (230, 50, 100, 30), "Push Color")) {
						_manager.UpdateStackVolumes (TestColor);
				}
				if (GUI.Button (new Rect (340, 50, 100, 30), "Push Rain")) {
						_manager.RainIntensity = RainIntensity;
				}

				if (GUI.Button (new Rect (10, Screen.height - 40, 100, 30), "Chunk 1")) {
						_manager.ChunkSettings = chunk1;
						_activeChunk = "chunk1";
				}
				if (GUI.Button (new Rect (120, Screen.height - 40, 100, 30), "Chunk 2")) {
						_manager.ChunkSettings = chunk2;
						_activeChunk = "chunk2";
				}
				if (GUI.Button (new Rect (230, Screen.height - 40, 100, 30), "Chunk 3")) {
						_manager.ChunkSettings = chunk3;
						_activeChunk = "chunk3";
				}
				if (GUI.Button (new Rect (340, Screen.height - 40, 100, 30), "Inside 1")) {
						_manager.StructureSettings = inside1;
						_activeChunk = "inside1";
				}
				if (GUI.Button (new Rect (450, Screen.height - 40, 100, 30), "Inside 2")) {
						_manager.StructureSettings = inside2;
						_activeChunk = "inside2";
				}


				GUI.Label (new Rect (10, 120, 200, 200), 
						"Active Sources: " + hAudioStack.Instance.SourcesCount +
						"\nIsDaytime: " + _manager.IsDaytime +
						"\nIsUnderground: " + _manager.IsUnderground +
						"\nIsInsideStructure: " + _manager.IsInsideStructure +
						"\nActive Chunk: " + _activeChunk +
						"\nActive Inside: " + _activeInside);
		}
}
