#region Copyright Notice & License Information
//
// InputExample.cs
//
// Author:
//       Matthew Davey <matthew.davey@dotbunny.com>
//       Robin Southern <betajaen@ihoed.com>
//
// Copyright (c) 2014 dotBunny Inc. (http://www.dotbunny.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

using UnityEngine;

/// <summary>
/// An example of how to use the Hydrogen.Peripherals.Input system. Additionally showing off how to integrate
/// with the Serialization classes to save and load a config from a file/string.
/// </summary>
[AddComponentMenu ("")]
public class InputExample : MonoBehaviour
{
		/// <summary>
		/// Display the GUI part of the example.
		/// </summary>
		public bool DisplayGUI = true;
		/// <summary>
		/// The ammo used by our examples tank.
		/// </summary>
		public GameObject[] Ammo;
		/// <summary>
		/// Our example tank's point of firing, where all ammo spawns.
		/// </summary>
		public GameObject SpawnPoint;
		/// <summary>
		/// Our example tank's Tower, acts as the pivot point for horizontal rotation.
		/// </summary>
		public GameObject TowerObject;
		/// <summary>
		/// Fake file storage for the examples INI serialization. 
		/// </summary>
		/// <remarks>
		/// Normally you would write/read this string to/from a file. 
		/// </remarks>
		string _fileHolder;
		/// <summary>
		/// A local reference of the Object Pools IDs for the added GameObjects.
		/// </summary>
		/// <summary>
		/// This matches the order of the Ammo array so its an easily matched reference.
		/// </summary>
		int[] _prefabIDs;
		/// <summary>
		/// Has the config been saved?
		/// </summary>
		bool _saved;
		/// <summary>
		/// Do we have controls loaded?
		/// </summary>
		bool _haveControls = true;

		/// <summary>
		/// Unity's Awake Event
		/// </summary>
		void Awake ()
		{
				// Dirty create our developer system, and get it into stats mode.
				hDebug.Instance.Mode = hDebug.DisplayMode.Stats;
		}

		/// <summary>
		/// Unity's OnGUI Event.
		/// </summary>
		void OnGUI ()
		{

				// If we dont want to display GUI, don't!
				if (!DisplayGUI)
						return;

				int verticalSpacing = (Screen.height / 2) - 15;
				int horizontalSpacing = (Screen.width / 2) - 75;
				// Serialize the current config, and save it to our fake file
				if (!_saved && GUI.Button (new Rect (horizontalSpacing - 160, verticalSpacing, 150, 30), "Save Config")) {
						_saved = true;
						_fileHolder = Hydrogen.Serialization.INI.Serialize (hInput.Instance.GetControls ());
						hDebug.Log (_fileHolder);
				}

				// Clear out all mapped controls from our fancy Input Manager
				if (_saved && GUI.Button (new Rect (horizontalSpacing, verticalSpacing, 150, 30), "Clear Controls")) {
						hInput.Instance.ClearControls ();
						_haveControls = false;
						hDebug.Log ("Cleared Controls");
				}

				// Remap our controls by deserializing our controls and passing that data to the Input Manager
				if (_saved && GUI.Button (new Rect (horizontalSpacing + 160, verticalSpacing, 150, 30), "Set Controls")) {
						hInput.Instance.SetControls (Hydrogen.Serialization.INI.Deserialize (_fileHolder, '='));
						_haveControls = true;
						hDebug.Log ("Set Controls");
				}

				hDebug.Watch ("Config Saved", _saved);
				hDebug.Watch ("Have Controls", _haveControls);
		}

		/// <summary>
		/// Horizontal Movement (Mouse X) Callback.
		/// </summary>
		/// <param name="inputEvent">Event Type</param>
		/// <param name="inputValue">The value associated with the event.</param>
		/// <param name="inputTime">The duration value associated with the event.</param>
		void OnRotate (Hydrogen.Peripherals.InputEvent inputEvent, float inputValue, float inputTime)
		{
				// Mouse X Axes are relative movements only. 
				// So we only turn the turret - never directly set the rotation.
				TowerObject.transform.localRotation *= Quaternion.AngleAxis (inputValue * 180.0f * Time.deltaTime, Vector3.up);
		}

		/// <summary>
		/// Shoot (Left Click / Space) Callback.
		/// </summary>
		/// <param name="inputEvent">Event Type.</param>
		/// <param name="inputValue">The value associated with the event.</param>
		/// <param name="inputTime">The duration value associated with the event.</param>
		void OnShoot (Hydrogen.Peripherals.InputEvent inputEvent, float inputValue, float inputTime)
		{
				// Only fire on release
				if (inputEvent == Hydrogen.Peripherals.InputEvent.Released) {

						// Create our shell from the ObjectPool
						GameObject shell = hObjectPool.Instance.Spawn (
								                   _prefabIDs [Random.Range (0, _prefabIDs.Length)], 
								                   SpawnPoint.transform.position, Quaternion.identity);

						// Add some punch to that spawned object
						shell.rigidbody.velocity = SpawnPoint.transform.rotation * Vector3.forward * 250.0f * (inputTime * inputTime + 0.1f);
				}
		}

		/// <summary>
		/// Vertical Movement (Up / Down) Callback.
		/// </summary>
		/// <param name="inputEvent">Event Type.</param>
		/// <param name="inputValue">The value associated with the event.</param>
		/// <param name="inputTime">The duration value associated with the event.</param>
		void OnMove (Hydrogen.Peripherals.InputEvent inputEvent, float inputValue, float inputTime)
		{
				// Vertical Axes give it's state of absolute values of -1.0, 1.0 only.
				// So this is used to move the box along it's looking direction (horizontal). This will handle forward 
				// and backward movement in one go.
				transform.transform.position += (transform.TransformDirection (Vector3.left) * inputValue * 10.0f) * Time.deltaTime;
		}

		/// <summary>
		/// Horizontal Movement (Left / Right) Callback.
		/// </summary>
		/// <param name="inputEvent">Event Type.</param>
		/// <param name="inputValue">The value associated with the event.</param>
		/// <param name="inputTime">The duration value associated with the event.</param>
		void OnTurn (Hydrogen.Peripherals.InputEvent inputEvent, float inputValue, float inputTime)
		{
				// Horizontal Axes give it's state of absolute values of -1.0, 1.0 only.
				// We normalised this to a steering range, then use this to turn the tank.
				transform.transform.localRotation *= Quaternion.AngleAxis (inputValue * 90.0f * Time.deltaTime, Vector3.up);
		}

		/// <summary>
		/// Unity's Start Event.
		/// </summary>
		void Start ()
		{
				// Make sure array is sized properly to hold the reference IDs
				_prefabIDs = new int[Ammo.Length];

				// Add Objects to Pool
				for (int x = 0; x < Ammo.Length; x++) {
						_prefabIDs [x] = hObjectPool.Instance.Add (Ammo [x]);
				}
						
				// Add our Actions for linking with controls
				hInput.Instance.AddAction ("Move", OnMove);
				hInput.Instance.AddAction ("Turn", OnTurn);
				hInput.Instance.AddAction ("Rotate", OnRotate);
				hInput.Instance.AddAction ("Shoot", OnShoot);

				// Create some controls
				hInput.Instance.AddControl ("Mouse X", "Rotate");
				hInput.Instance.AddControl ("Horizontal", "Turn");
				hInput.Instance.AddControl ("Vertical", "Move");
				hInput.Instance.AddControl ("Left", "Shoot");
				hInput.Instance.AddControl ("Space", "Shoot");
		}
}
