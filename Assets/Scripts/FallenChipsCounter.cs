using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FallenChipsCounter : MonoBehaviour
{
	public event Action OnReachPlayerFallenChipGoal;

	public event Action OnReachOpponentFallenChipGoal;

	[SerializeField] private int _playerFallenChipGoal;
	[SerializeField] private int _opponentFallenChipGoal;

	private int _playerFallenChipCount;
	private int _opponentFallenChipCount;

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out PlayerChip playerChip))
		{
			_playerFallenChipCount++;
			if(_playerFallenChipCount >= _playerFallenChipGoal)
			{
				OnReachPlayerFallenChipGoal?.Invoke();
			}
		}
		else if(other.TryGetComponent(out OpponentChip opponentChip))
		{
			_opponentFallenChipCount++;
			if(_opponentFallenChipCount >= _opponentFallenChipGoal)
			{
				OnReachOpponentFallenChipGoal?.Invoke();
			}
		}
	}
}