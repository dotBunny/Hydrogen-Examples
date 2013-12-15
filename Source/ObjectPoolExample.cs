#region Copyright Notice & License Information
//
// ObjectPoolExample.cs
//
// Author:
//       Matthew Davey <matthew.davey@dotbunny.com>
//
// Copyright (c) 2013 dotBunny Inc. (http://www.dotbunny.com)
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
/// An example of how to use the Object Pool in a quick drop in manner. There are many ways to use the object pool
/// some more effiencient then others.
/// </summary>
[AddComponentMenu ("")]
public class ObjectPoolExample : MonoBehaviour
{
		/// <summary>
		/// Prefab array to use with the spawner.
		/// </summary>
		public GameObject[] Prefabs;
		/// <summary>
		/// An internal reference to keep track of relevant pool IDs.
		/// </summary>
		int[] _poolIDs;

		/// <summary>
		/// Unity's Start Event.
		/// </summary>
		public void Start ()
		{
				// Add all of our prefabs to the Object Pool
				_poolIDs = hObjectPool.Instance.Add (Prefabs);
		}

		/// <summary>
		/// Unity's Update Event.
		/// </summary>
		public void Update ()
		{
				// Spawn a GameObject (randomly) from the reference array. We could have passed a GameObject instead,
				// but this method is faster. This will also return a reference to the newly spawned GameObject.
				hObjectPool.Instance.Spawn (
						Random.Range (0, _poolIDs.Length),
						gameObject.transform.position, Random.rotation);
		}
}
