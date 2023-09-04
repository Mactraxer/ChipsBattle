using System.Runtime.CompilerServices;
using UnityEngine;

public class LookToByY : MonoBehaviour
{
	[SerializeField] private Transform _target;

	[ContextMenu("DoRotate")]
	public void DoRotate()
	{
		Vector3 direction = _target.position - transform.position;
		Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, direction);
		transform.rotation = toRotation;
	}

	private void Update()
	{
		DoRotate();
	}
}