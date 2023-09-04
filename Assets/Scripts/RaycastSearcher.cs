using UnityEngine;

public class RaycastSearcher : MonoBehaviour
{
	private Camera _camera;

	private void Start()
	{
		_camera = Camera.main;
	}

	public RaycastHit SendRayTo(Vector3 screenPosition, LayerMask layerMask = default)
	{
		var ray = _camera.ScreenPointToRay(screenPosition);

		if(Physics.Raycast(ray, out RaycastHit hit, 20, layerMask))
		{
			return hit;
		}
		else
		{
			return default;
		}
	}
}