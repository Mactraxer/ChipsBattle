using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
	[SerializeField] private int _fromIndex;
	[SerializeField] private int _toIndex;
	[SerializeField] private GameObject _cellEmptyPrefab;
	[SerializeField] private GameObject _dublicatePrefab;
	[SerializeField] private RectTransform _content;
	[SerializeField] private ScrollRect _scrollRect;

	private void OnEnable()
	{
		Animate();
	}

	[ContextMenu("Play Animation")]
	public void Animate()
	{
		var targetCell = _content.GetChild(_fromIndex);
		var targetCellAnchoredRelativePosition = (Vector2)_scrollRect.transform.InverseTransformPoint(_content.GetChild(_fromIndex - 1).transform.position);
		targetCell.gameObject.SetActive(false);

		var destinationCell = _content.GetChild(_toIndex);
		destinationCell.gameObject.SetActive(false);

		var fromEmptyCell = Instantiate(_cellEmptyPrefab);
		var toEmptyCell = Instantiate(_cellEmptyPrefab);

		toEmptyCell.transform.SetParent(_content);
		fromEmptyCell.transform.SetParent(_content);
		toEmptyCell.transform.SetSiblingIndex(_toIndex);
		fromEmptyCell.transform.SetSiblingIndex(_fromIndex);


		var dublicateTargetCell = Instantiate(_dublicatePrefab);
		dublicateTargetCell.transform.localScale = Vector3.one * 1.1f;

		var rectTransformDublicateCell = dublicateTargetCell.GetComponent<RectTransform>();
		rectTransformDublicateCell.SetParent(_scrollRect.transform);
		rectTransformDublicateCell.anchoredPosition = targetCellAnchoredRelativePosition;
		rectTransformDublicateCell.offsetMax = new Vector2(0, rectTransformDublicateCell.offsetMax.y);
		rectTransformDublicateCell.offsetMin = new Vector2(0, rectTransformDublicateCell.offsetMin.y);

		var targetDestinationCell = _content.GetChild(_toIndex);
		StartCoroutine(MoveCellToDestination(dublicateTargetCell.GetComponent<RectTransform>(), destinationCell));
		/*var layoutComponent = targetCell.AddComponent<LayoutElement>();
		layoutComponent.ignoreLayout = true;*/

		//StartCoroutine(AnimationLoop(targetCell, targetDestinationCell));
	}

	private IEnumerator MoveCellToDestination(RectTransform dublicateTargetCell, Transform destinationCell)
	{
		yield return new WaitForSeconds(0.8f);
		var destinationAnchoredPosition = (Vector2)_scrollRect.transform.InverseTransformPoint(destinationCell.position);
		while(destinationAnchoredPosition != dublicateTargetCell.anchoredPosition)
		{
			dublicateTargetCell.anchoredPosition = Vector2.Lerp(dublicateTargetCell.anchoredPosition, destinationAnchoredPosition, Time.deltaTime);
			yield return null;
		}
	}

	private IEnumerator AnimationLoop(Transform targetCell, Transform targetDestinationCell)
	{
		var destinationAnchoredPosition = GetScrollToElement(targetDestinationCell.GetComponent<RectTransform>());
		while (destinationAnchoredPosition != _content.anchoredPosition)
		{
			_content.anchoredPosition = Vector2.Lerp(destinationAnchoredPosition, _content.anchoredPosition, Time.deltaTime);
			yield return null;
		}
	}

	public Vector2 GetScrollToElement(RectTransform targetElement)
	{
		RectTransform contentRect = _scrollRect.content;

		return
			(Vector2)_scrollRect.transform.InverseTransformPoint(contentRect.position)
			- (Vector2)_scrollRect.transform.InverseTransformPoint(targetElement.position);
	}
}