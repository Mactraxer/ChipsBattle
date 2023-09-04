using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private FallenChipsCounter _fallenChipsCounter;

    private void Start() 
    {
        _fallenChipsCounter.OnReachOpponentFallenChipGoal += ReachOpponentFalleChipGoal;
        _fallenChipsCounter.OnReachPlayerFallenChipGoal += ReachPlayerFallenChipGoal;
    }

    private void ReachPlayerFallenChipGoal()
    {
        print("You lose");
    }

    private void ReachOpponentFalleChipGoal()
    {
        print("You win");
    }
}
