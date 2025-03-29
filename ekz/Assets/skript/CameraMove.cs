using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _speedUpDown;
	[SerializeField] private KeyCode _up;
	[SerializeField] private KeyCode _down;
	[SerializeField] private float _maxUp;
	[SerializeField] private float _maxDown;

	private float _horizontal;
	private float _vertical;

	private void Update()
	{
		_horizontal = Input.GetAxis("Horizontal");
		_vertical = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(_horizontal * _speed * Time.deltaTime, 0, _vertical * _speed * Time.deltaTime), Space.World);

		if (Input.GetKey(_up) && transform.position.y <= _maxUp)
		{
			transform.Translate(new Vector3(0, 1 * _speedUpDown * Time.deltaTime, 0), Space.World);
		}
		else if (Input.GetKey(_down) && transform.position.y >= _maxDown)
		{
			transform.Translate(new Vector3(0, -1 * _speedUpDown * Time.deltaTime, 0), Space.World);
		}
	}
}
