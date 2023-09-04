using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static event Action<Vector3> OnTap;

	public static event Action<Vector3> OnMove;

	public static event Action<Vector3> OnUntap;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			OnTap?.Invoke(Input.mousePosition);
		}

		if(Input.GetMouseButtonUp(0))
		{
			OnUntap?.Invoke(Input.mousePosition);
		}

		if(Input.GetMouseButton(0))
		{
			OnMove?.Invoke(Input.mousePosition);
		}
	}
}