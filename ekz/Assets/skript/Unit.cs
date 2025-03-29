using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private CommandCenter _commandCenter;
	[SerializeField] private Transform _cargo;

	private Transform[] _pointsToMove;
	private int _counter;
	private bool _isBusy = false;
	private bool _isHaveResource = false;
	private Transform _resourcePosition;
	private bool _buildBase = false;

	private void FixedUpdate()
	{
		if (!_isBusy && !_isHaveResource)
		{
			_pointsToMove = _commandCenter.Points();
			FreeMove();
		}
		else
		{
			if (_isHaveResource)
			{
				PullToTarget(_commandCenter.transform);
			}
			else if (!_isHaveResource)
			{
				PullToTarget(_resourcePosition);
			}
			else if (_buildBase)
			{
				MoveToBase(_commandCenter.FlagPos());
			}
		}

	}

	private void FreeMove()
	{
		transform.position = Vector3.MoveTowards(transform.position, _pointsToMove[_counter].position, _speed * Time.deltaTime);
		transform.LookAt(_pointsToMove[_counter]);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out MovePoints points))
		{
			_counter = ++_counter % _pointsToMove.Length;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out CommandCenter commandCenter))
		{
			foreach (Transform chield in transform)
			{
				if (chield.gameObject.TryGetComponent(out Resource resource))
				{
					_isBusy = false;
					_isHaveResource = false;
					resource.transform.SetParent(null);
					resource.Destroed();
					_commandCenter.GiveScore();
					_commandCenter.ReturnUnitToQueue(this);
				}
			}
		}
		else if (collision.gameObject.TryGetComponent(out Resource resource))
		{
			if (resource.transform.position == _resourcePosition.position)
			{
				resource.gameObject.transform.SetParent(transform);
				resource.transform.position = _cargo.position;
				_isHaveResource = true;
			}
		}
	}

	public void PullToTarget(Transform targetPos)
	{
		_isBusy = true;
		transform.position = Vector3.MoveTowards(transform.position, targetPos.position, _speed * Time.deltaTime);
		transform.LookAt(targetPos);
	}

	public void TakeTarget(Transform target)
	{
		_resourcePosition = target;
	}

	public void MoveToBase(Vector3 pos)
	{
		_buildBase = true;
		transform.position = Vector3.MoveTowards(transform.position, pos, _speed * Time.deltaTime);
		transform.LookAt(pos);
	}

	public void SetCommandCenter(CommandCenter commandCenter)
	{
		_commandCenter = commandCenter;
	}
}
