using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class MeshCombinerExample : MonoBehaviour
{
		Hydrogen.Threading.Jobs.MeshCombiner _meshCombiner;
		public Transform TargetMeshes;
		//public Material defaultMaterial;
		// Update is called once per frame
		void LateUpdate ()
		{
				if (_meshCombiner != null) {
						_meshCombiner.Check ();
				}
		}

		public IEnumerator ProcessMeshFilters ()
		{
				// For the sake of the demo we are going to need to roll over the "Target" to find all the 
				// meshes that we need to look at, but in theory you could do this without having to load the
				// object by simply having raw mesh data, or any other means of accessing it.

				// Create a new MeshCombiner (we dont want any old data kicking around)
				_meshCombiner = new Hydrogen.Threading.Jobs.MeshCombiner ();

				// Yes We Hate This - There Are Better Implementations
				MeshFilter[] meshFilters = TargetMeshes.GetComponentsInChildren<MeshFilter> ();
				yield return new WaitForEndOfFrame ();

				// have static function taht determines if its on a different material ... loops one material at a time
				// Our data array
				var meshes = new Hydrogen.Threading.Jobs.MeshCombiner.MeshDescription[meshFilters.Length];

				// Loop through all of our mesh filters and add them to the combiner to be combined.
				for (int x = 0; x < meshFilters.Length; x++) {

						if (meshFilters [x].gameObject.activeSelf) {

								_meshCombiner.AddMesh (meshFilters [x].mesh, meshFilters [x].transform);
						}
						meshFilters [x].gameObject.SetActive (false);
						yield return new WaitForEndOfFrame ();
				}

				// Start the threaded love
				_meshCombiner.Combine (System.Threading.ThreadPriority.Normal, null, ProcessMeshesInsideUnity);
		}

		public IEnumerator ProcessMeshes (int hash, 
		                                  Hydrogen.Threading.Jobs.MeshCombiner.MeshDescription[] meshDescriptions, 
		                                  Material[] materials)
		{
				var meshes = new List<Mesh> ();


				for (int x = 0; x <= meshDescriptions.Length; x++) {
						var newMesh = Hydrogen.Threading.Jobs.MeshCombiner.CreateMesh (meshDescriptions [x]);

						// Add to list
						meshes.Add (newMesh);

						// Fake Unity Threading
						yield return new WaitForEndOfFrame ();
				}

				GameObject go = new GameObject ("Combined Meshes");
				go.transform.position = TargetMeshes.position;
				go.transform.rotation = TargetMeshes.rotation;

				// Show Them
				for (int y = 0; y <= meshes.Count; y++) {
						GameObject meshObject = new GameObject ();
						meshObject.name = meshes [y].name;
						meshObject.transform.parent = go.transform;
						meshObject.transform.position = Vector3.zero;
						meshObject.transform.rotation = Quaternion.identity;
						meshObject.AddComponent<MeshFilter> ().mesh = meshes [y];
						//meshObject.AddComponent<MeshRenderer> ().material = defaultMaterial;
				}

				// Destroy the mesh combiner (forcing data wipe);
				yield return new WaitForEndOfFrame ();
				_meshCombiner = null;
		}

		public void ProcessMeshesInsideUnity (int hash, Hydrogen.Threading.Jobs.MeshCombiner.MeshDescription[] meshDescriptions, Material[] materials)
		{
				// This is just a dirty way to see if we can squeeze jsut a bit more performance out of Unity when 
				// making all of the meshes for us (instead of it being done in one call, we use a coroutine with a loop.
				StartCoroutine (ProcessMeshes (hash, meshDescriptions, materials));
		}

		void OnGUI ()
		{

				if (_meshCombiner == null) {

						if (GUI.Button (new Rect (5, 5, 200, 35), "Rock & || Possibly Roll!")) {
								StartCoroutine (ProcessMeshFilters ());
						}

				}

				GUI.color = Color.black;
				GUI.Label (new Rect (210, 12, 100, 35), Time.time.ToString ());
		}
}
