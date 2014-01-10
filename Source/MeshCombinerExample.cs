using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class MeshCombinerExample : MonoBehaviour
{
		Hydrogen.Threading.Jobs.MeshCombiner _meshCombiner;
		public Transform TargetMeshes;
		public Material defaultMaterial;
		// Update is called once per frame
		void LateUpdate ()
		{
				if (_meshCombiner != null) {
						_meshCombiner.Check ();
				}
		}

		public void ShowNewMeshes (int hash, Mesh[] newMeshes)
		{
				//				Debug.Log (newMeshes [0].triangles.Length);

				GameObject go = new GameObject ("Combined Meshes");
				go.transform.position = TargetMeshes.position;
				go.transform.rotation = TargetMeshes.rotation;

				foreach (Mesh m in newMeshes) {
						GameObject meshObject = new GameObject ();
						meshObject.name = m.name;
						meshObject.transform.parent = go.transform;
						meshObject.transform.position = Vector3.zero;
						meshObject.transform.rotation = Quaternion.identity;
						meshObject.AddComponent<MeshFilter> ().mesh = m;
						meshObject.AddComponent<MeshRenderer> ().material = defaultMaterial;
				}
				_meshCombiner = null;

		}

		void OnGUI ()
		{

				if (_meshCombiner == null) {

						if (GUI.Button (new Rect (5, 5, 200, 35), "Rock & || Possibly Roll!")) {
								// For the sake of the demo we are going to need to roll over the "Target" to find all the 
								// meshes that we need to look at, but in theory you could do this without having to load the
								// object by simply having raw mesh data, or any other means of accessing it.
	
								// Yes We Hate This - There Are Better Implementations
								MeshFilter[] meshFilters = TargetMeshes.GetComponentsInChildren<MeshFilter> ();

								// have static function taht determines if its on a different material ... loops one material at a time
								// Our data array
								var meshes = new Hydrogen.Threading.Jobs.MeshCombiner.Mesh[meshFilters.Length];

								// This is a hickup ... almost like this needs to be a coroutine in itself :)
								for (int x = 0; x < meshFilters.Length; x++) {

										if (meshFilters [x].gameObject.activeSelf) {
												meshes [x] = Hydrogen.Threading.Jobs.MeshCombiner.MeshFilterToMesh (meshFilters [x]);
										}
										meshFilters [x].gameObject.SetActive (false);
								}
								_meshCombiner = new Hydrogen.Threading.Jobs.MeshCombiner ();
								//_meshCombiner.CombineMeshes (meshes, ShowNewMeshes);

								Debug.Log (_meshCombiner.CombineMeshes (meshes, ShowNewMeshes));
						}

				}

				GUI.color = Color.black;
				GUI.Label (new Rect (210, 12, 100, 35), Time.time.ToString ());
		}
}
