using UnityEngine;

public class InteractiveHandler : MonoBehaviour
{
	[SerializeField] private RaycastSearcher _physicsRaycaster;
	[SerializeField] private LayerMask _searchMask;

	private Chip _selectedChip;

	private void Awake()
	{
		InputManager.OnTap += InputOnTap;
		InputManager.OnUntap += InputOnUntap;
	}

	private void InputOnUntap(Vector3 screenPosition)
	{
		if(_selectedChip == default)
		{
			return;
		}

		_selectedChip.Kick();
	}

	private void InputOnTap(Vector3 screenPosition)
	{
		var hitInfo = _physicsRaycaster.SendRayTo(screenPosition, _searchMask);

		if(hitInfo.collider == default)
		{
			return;
		}

		if(hitInfo.collider.gameObject.TryGetComponent(out PlayerChip chip))
		{
			_selectedChip = chip;
			chip.StartSelectPowerKick(screenPosition, hitInfo.distance);
		}
	}
}