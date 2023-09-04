using UnityEngine;

public abstract class Chip : MonoBehaviour
{
	[SerializeField] private KickBehaviour _kickBehaviour;

	public void Kick()
	{
		_kickBehaviour.Kick();
	}

	public void StartSelectPowerKick(Vector3 screenPosition, float distance)
	{
		_kickBehaviour.StartSelectPowerKick(screenPosition, distance);
	}
}