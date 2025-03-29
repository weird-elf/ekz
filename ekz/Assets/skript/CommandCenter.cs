using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CommandCenter : MonoBehaviour
{
	[SerializeField] private Scaning _scaning;
	[SerializeField] private Unit[] _units;
	[SerializeField] private Unit _prefab;
	[SerializeField] private Transform _pointSpawnDrones;
	[SerializeField] private Transform[] _pointsToMove;
	[SerializeField] private BuildCommandCenter _buildCommandCenter;

	private Queue<Unit> _queueOfUnits = new Queue<Unit>();
	private Queue<Resource> _resources = new Queue<Resource>();
	private Transform _tempTargetPosition;
	private Unit _tempUnit;
	private Resource _tempResource;
	public event Action _event;
	private Queue<Unit> _tempQueueOfUnits = new Queue<Unit>();

	private void Awake()
	{
		_buildCommandCenter = FindAnyObjectByType<BuildCommandCenter>();
	}

	private void Start()
	{
		foreach (Unit unit in _units)
		{
			_queueOfUnits.Enqueue(unit);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_resources = _scaning.Scaner(_resources);
		}

		if (_queueOfUnits.Count > 0)
		{
			_tempTargetPosition = _resources.Count > 0 ? _resources.Dequeue().transform : null;

			if (_tempTargetPosition != null)
			{
				SendDrone(_queueOfUnits.Dequeue(), _tempTargetPosition);
			}
		}
	}

	public Transform[] Points()
	{
		return _pointsToMove;
	}

	private void SendDrone(Unit drone, Transform targetPosition)
	{
		drone.TakeTarget(targetPosition);
		drone.PullToTarget(targetPosition);
	}

	public void ReturnUnitToQueue(Unit unit)
	{
		_queueOfUnits.Enqueue(unit);
	}

	public void GiveScore()
	{
		_event?.Invoke();
	}

	public void SpawnDrones()
	{
		_tempUnit = Instantiate(_prefab, _pointSpawnDrones.position, Quaternion.identity);
		_queueOfUnits.Enqueue(_tempUnit);
		_tempUnit.SetCommandCenter(this);
	}

	public void BuildingCommandCenter()
	{
		Unit unit;

		if (_queueOfUnits.Count > 1)
		{
			Debug.Log("1");

			unit = _queueOfUnits.Dequeue();
			unit.MoveToBase(FlagPos());

			Debug.Log("2");

			if (unit.transform.position == _buildCommandCenter.PositionFlag()) 
			{
				Debug.Log("3");

				_buildCommandCenter.StartBuilding(this);
				_tempQueueOfUnits.Enqueue(unit);
				if (_queueOfUnits.Count == 0)
				{
					_queueOfUnits = _tempQueueOfUnits;
				}
			}
		}
	}

	public Vector3 FlagPos()
	{
		return _buildCommandCenter.PositionFlag();
	}
}
