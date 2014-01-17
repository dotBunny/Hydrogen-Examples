#region Copyright Notice & License Information
//
// AudioStackExample.cs
//
// Author:
//       Matthew Davey <matthew.davey@dotbunny.com>
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
using System.Collections;

public class AudioStackExample : MonoBehaviour
{
		[System.Serializable]
		public class TestItem
		{
				public int Priority = 128;
				public AudioClip Clip;
		}

		/// <summary>
		/// Our array of test clips.
		/// </summary>
		public TestItem[] clips;
		/// <summary>
		/// Our button click sound.
		/// </summary>
		public AudioClip buttonClick;

		public void OnGUI ()
		{
				if (GUI.Button (new Rect (5, 5, 175, 35), "Play (No Dups)")) {
						hAudioStack.Instance.Add (
								new Hydrogen.Core.AudioStackItem (
										clips [Random.Range (0, clips.Length)].Clip));
				}

				// Play a clip through the stack allowing for duplicates to be added.
				// This will create unique keys based on some settings.
				if (GUI.Button (new Rect (185, 5, 175, 35), "Play (Yes Dups)")) {
						hAudioStack.Instance.Add (
								new Hydrogen.Core.AudioStackItem (
										clips [Random.Range (0, clips.Length)].Clip), true);
				}

				// Play a clip through the stack overriding lower priority clips as needed.
				if (GUI.Button (new Rect (365, 5, 175, 35), "Play (Use Priorities)")) {

						var randomID = Random.Range (0, clips.Length);

						var newItem = new Hydrogen.Core.AudioStackItem (clips [randomID].Clip) {
								Priority = clips [randomID].Priority
						};

						hAudioStack.Instance.Add (newItem);
				}


				if (GUI.Button (new Rect (545, 5, 100, 35), "Button")) {

						// Check if the button sound is loaded using its name as the key, if so, we want to restart it!
						if (hAudioStack.Instance.IsLoaded (buttonClick)) {
								hAudioStack.Instance.LoadedItems [buttonClick.name].Restart ();
						} else {
								// If its not there were going to have to add it to the stack.
								var newItem = new Hydrogen.Core.AudioStackItem (buttonClick, false);
								// Make sure that priority is going to outweight the competition
								newItem.Priority = 5000;
								// Add it to the stack
								hAudioStack.Instance.Add (newItem, false);
						}
								

				}

				// Some debug output to see how many sources are actually active.
				GUI.color = Color.black;
				GUI.Label (new Rect (10, 50, 100, 35), "Active Sources: " + hAudioStack.Instance.SourcesCount);
		}
}
