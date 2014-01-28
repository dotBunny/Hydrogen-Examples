#region Copyright Notice & License Information
//
// MeshCombinerExample.cs
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
/// An example of how to use the MeshCombiner compeonent provided with Hydrogen. 
/// </summary>
public class MeshCombinerExample : MonoBehaviour
{
		/// <summary>
		/// The transform that all outputted meshes should parent too.
		/// </summary>
		public Transform OutputParent;
		/// <summary>
		/// The parent object that holds all of the meshes we need to combine.
		/// </summary>
		public GameObject RootObject;

		/// <summary>
		/// Unity's Awake Event
		/// </summary>
		void Awake ()
		{
				// Initialize hDebug (cheat) and put it into stats mode.
				hDebug.Instance.Mode = hDebug.DisplayMode.Stats;


		}

		/// <summary>
		/// Unity's OnGUI Event
		/// </summary>
		void OnGUI ()
		{
				if (GUI.Button (new Rect ((Screen.width / 2) - 45, (Screen.height / 2) - 15, 90, 30), "Combine")) {
						hMeshCombiner.Instance.Combine (RootObject, OutputParent, true);
				}
		}

		void FixedUpdate ()
		{
				// This is expensive (never do this in your game) but necessary to demonstrate the effectiveness.
				MeshFilter[] before = RootObject.GetComponentsInChildren<MeshFilter> ();
				MeshFilter[] after = OutputParent.gameObject.GetComponentsInChildren<MeshFilter> ();

				// Update our Meshes watch.
				hDebug.Watch ("Meshes", before.Length + after.Length);
		}
}
