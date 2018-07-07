using UnityEngine;

/// <summary>
/// Class to make objects always facing to the active camera.
/// </summary>
public class Billboard : MonoBehaviour
{
	[SerializeField] private Transform cameraTransform;

	private void Update()
	{
		DoBillboard();
	}

	/// <summary>
	/// Changes the rotation of this object to face to the camera.
	/// </summary>
	private void DoBillboard()
	{
		transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
	}
}