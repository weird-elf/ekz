using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
	[SerializeField] private Text _text;
	[SerializeField] private CommandCenter _commandCenter;
	[SerializeField] private Button _button;
	[SerializeField] private Button _buttonBuild;
	[SerializeField] private int _price;
	[SerializeField] private int _priceBuild;

	private int _count = 0;

	private void OnEnable()
	{
		_commandCenter._event += PointGive;
	}

	private void OnDisable()
	{
		_commandCenter._event -= PointGive;
	}

	private void PointGive()
	{
		_count++;
		ButtonActivate();
		_text.text = _count.ToString();
	}

	private void ButtonActivate()
	{
		if (_count >= _price)
		{
			_button.gameObject.SetActive(true);
		}
		else
		{
			_button.gameObject.SetActive(false);
		}

		if (_count >= _priceBuild) 
		{
			_buttonBuild.gameObject.SetActive(true);
		}
		else
		{
			_buttonBuild.gameObject.SetActive(false);
		}
	}

	public void SpawnDrone()
	{
		_count -= _price;
		_text.text = _count.ToString();
		ButtonActivate();
		_commandCenter.SpawnDrones();
	}
}
