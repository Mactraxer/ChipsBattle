using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KickBehaviour : MonoBehaviour
{
	[SerializeField] private float _power;
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private RaycastSearcher _searcher;
	[SerializeField] private LayerMask _searchLayer;
	[Header("Line Renderer Settings")]
	[SerializeField] private Material _lineRendererMaterial;

	private Camera _camera;
	private LineRenderer _lineRenderer;
	private float _distance;

	private void Start()
	{
		_camera = Camera.main;
		SetupLineRenderer();
	}

	private void SetupLineRenderer()
	{
		_lineRenderer = gameObject.AddComponent<LineRenderer>();
		_lineRenderer.enabled = false;
		_lineRenderer.positionCount = 2;
		_lineRenderer.material = _lineRendererMaterial;
	}

	private void InputOnMove(Vector3 screenPosition)
	{
		var hitInfo = _searcher.SendRayTo(screenPosition, _searchLayer);
		var correctScreenPosition = hitInfo.point;
		_lineRenderer.SetPosition(1, correctScreenPosition);
	}

	public void StartSelectPowerKick(Vector3 screenPosition, float distance)
	{
		_distance = distance;
		var ray = _camera.ScreenPointToRay(screenPosition);
		var correctScreenPosition = ray.origin + ray.direction * distance;

		_lineRenderer.SetPosition(0, correctScreenPosition);
		_lineRenderer.SetPosition(1, correctScreenPosition);
		_lineRenderer.enabled = true;

		InputManager.OnMove += InputOnMove;
	}

	public void Kick()
	{
		var avarageYPosition = (_lineRenderer.GetPosition(0).y + _lineRenderer.GetPosition(1).y) / 2;
		var startPosition = new Vector3(_rigidbody.position.x, _rigidbody.position.y, _rigidbody.position.z);
		var endPosition = new Vector3(_lineRenderer.GetPosition(1).x, _rigidbody.position.y, _lineRenderer.GetPosition(1).z);
		var kickForce = startPosition - endPosition;
		_rigidbody.AddForce(kickForce * _power, ForceMode.Impulse);

		_lineRenderer.enabled = false;
		InputManager.OnMove -= InputOnMove;
	}
}