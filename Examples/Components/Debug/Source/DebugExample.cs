#region Copyright Notice & License Information
//
// DebugExample.cs
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

/// <summary>
/// An example of how to use the hDebug component used throughout Hydrogen.
/// </summary>
[AddComponentMenu ("")]
public class DebugExample : MonoBehaviour
{
		/// <summary>
		/// An internal demonstration counter.
		/// </summary>
		int _counter;

		/// <summary>
		/// Unity's Awake Event.
		/// </summary>
		void Awake ()
		{
				// There is many ways to initialize the debug system, however this is the simplest.
				hDebug.Initialize ();

				// Turn on Stats mode
				hDebug.Instance.Mode = hDebug.DisplayMode.Stats;
		}

		/// <summary>
		/// Unity's Start Event.
		/// </summary>
		void Start ()
		{
				hDebug.Log (gameObject.name + "'s Start Function Was Called");
				hDebug.Warn ("This is a warning message.");
				hDebug.EchoToUnityLog = false;
				hDebug.Error ("While this is an error message. This won't show up in Unity's log as we've turned off that feature.");

				// Turn it back on to prove we dont double up messages
				hDebug.EchoToUnityLog = true;

				// These will still show up in the Unity log
				Debug.Log ("You don't have to call hDebug to populate its messages.");
				Debug.LogWarning ("It handles all the basics for you.");

		}

		/// <summary>
		/// Unity's Update Event.
		/// </summary>
		void Update ()
		{
				// Our message to show in the middle of the screen
				string message = "Press Tilde (~) To Cycle Modes. [" + hDebug.Instance.Mode + "]";

				// Print to the screen ...
				hDebug.Print (
						(Screen.width / 2) - ((message.Length * hDebug.GlyphWidth) / 2), 
						((Screen.height / 2) - (hDebug.GlyphHeight / 2)), 
						Color.gray, message);

				// Increment our counter
				_counter++;

				// Report its findings
				hDebug.Watch ("Counter", _counter);
		}
}
