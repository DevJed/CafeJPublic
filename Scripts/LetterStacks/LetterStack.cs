using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class LetterStack : MonoBehaviour
{
	[SerializeField] private List<Stack> _letterCollections = new List<Stack>();
	
	[Header("Refrences")]
	[SerializeField] private Transform _letterStackParent;
	[SerializeField] private Transform _leftBox;
	[SerializeField] private Transform _rightBox;
	
	[Header("Positions")]
	[SerializeField] private Vector3 _readPosition;
	[SerializeField] private Vector3 _readRotation;

	[Header("Labels")]
	[SerializeField] private TextMeshProUGUI _leftLabel;
	[SerializeField] private TextMeshProUGUI _rightLabel;
	
	private Queue<Transform> _letterQueue = new Queue<Transform>();

	private Transform _currentLetter;

	private List<Transform> _createdLetters = new List<Transform>();
	private List<Letter> _leftPile = new List<Letter>();
	private List<Letter> _rightPile = new List<Letter>();

	private bool correctPlacements;
	
	public bool GetCorrectPlacementStatus() => correctPlacements;

	public void StartDayEnvelopes()
	{
		ClearPreviousLetters();
		
		SpawnLetters();
	}

	private void ClearPreviousLetters()
	{
		for (int i = 0; i < _createdLetters.Count; i++)
		{
			Destroy(_createdLetters[i].gameObject);
		}
		
		_createdLetters.Clear();
		
		_leftPile.Clear();
		_rightPile.Clear();
		_letterQueue.Clear();

		_leftLabel.text = "Blue";
		_rightLabel.text = "Red";
	}
	
	private void SpawnLetters()
	{
		for (int i = 0; i < _letterCollections[DayManager.instance._dayIndex].Letters.Count; i++)
		{
			Transform createdLetter = Instantiate(_letterCollections[DayManager.instance._dayIndex].Letters[i].transform, 
				new Vector3(_letterStackParent.position.x, _letterStackParent.position.y + (0.001f * (_letterCollections[DayManager.instance._dayIndex].Letters.Count - 1  - i)), _letterStackParent.position.z), 
				Quaternion.Euler(90f, 0f,  Random.Range(75, 120)), 
				_letterStackParent);
			
			_createdLetters.Add(createdLetter);
			_letterQueue.Enqueue(createdLetter);
		}

		//Resets bool so it won't think its true if you did nothing
		correctPlacements = false;
	}

	public void GrabTopLetter(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled || _currentLetter != null || _letterQueue.Count == 0) return;
		
		Transform letter = _letterQueue.Dequeue();
		letter.DOLocalMove(_readPosition, 0.5f);
		letter.DORotate(_readRotation, 0.5f, RotateMode.Fast);
		_currentLetter = letter;
	}

	public void SlotLeft(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled || _currentLetter == null) return;
		
		_currentLetter.DOMove(_leftBox.position, 0.5f).OnComplete(CheckIfComplete);
		_currentLetter.DOLocalRotate(new Vector3(90f, -90f, 0f), 0.35f, RotateMode.Fast).OnComplete(() =>
		{
			AudioManager.Instance.PlaySFX("LettersPlacement");
		});
		
		_leftPile.Add(_currentLetter.GetComponent<Letter>());
	}
	
	public void SlotRight(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled ||  _currentLetter == null) return;
		
		_currentLetter.DOMove(_rightBox.position, 0.5f).OnComplete(CheckIfComplete);
		_currentLetter.DOLocalRotate(new Vector3(90f, -90f, 0f), 0.35f, RotateMode.Fast).OnComplete(() =>
		{
			AudioManager.Instance.PlaySFX("LettersPlacement");
		});
		
		_rightPile.Add(_currentLetter.GetComponent<Letter>());
	}

	private void CheckIfComplete()
	{
        if (_letterQueue.Count == 0)
		{
			CheckPlacements();
		}
		
		_currentLetter = null;
	}
	
	private void CheckPlacements()
	{
		correctPlacements = true;
		
		for (int l = 0; l < _leftPile.Count; l++)
		{
			if (_leftPile[l].RedLetter)
			{
				correctPlacements = false;
			}
		}

		for (int r = 0; r < _rightPile.Count; r++)
		{
			if (_rightPile[r].RedLetter == false)
			{
				correctPlacements = false;
			}
		}

		//If letters were not stack correctly
		if (!correctPlacements)
		{
			AudioManager.Instance.PlaySFX("LettersIncorrect");
			IncorrectPlacements();
		}
		else
		{
			AudioManager.Instance.PlaySFX("LettersCorrect");
			CorrectPlacements();
		}
	}

	private void CorrectPlacements()
	{
		_leftLabel.text = _rightLabel.text = "Completed";
	}

	private void IncorrectPlacements()
	{
		//Clear lists
		_leftPile.Clear();
		_rightPile.Clear();

		//Re-position letters
		for (int i = 0; i < _letterCollections[DayManager.instance._dayIndex].Letters.Count; i++)
		{
			Transform focusedLetter = _createdLetters[i].transform;

			focusedLetter.position = new Vector3(_letterStackParent.position.x,
				_letterStackParent.position.y + (0.001f * (_letterCollections[DayManager.instance._dayIndex].Letters.Count - 1 - i)),
				_letterStackParent.position.z);

			focusedLetter.rotation = Quaternion.Euler(90f, 0f, Random.Range(75, 120));

			_letterQueue.Enqueue(focusedLetter);
		}
	}
}
