using UnityEngine;
using System.Collections;

public class SetMaterial : MonoBehaviour
{
		public Material defaultMaterial;

		public void Start ()
		{
				UpdateMaterial (defaultMaterial);
		}

		public void UpdateMaterial (Material material)
		{
				renderer.material = material;
		}
}
