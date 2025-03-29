using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommandCenter : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] private CommandCenter _commandCenterPrefab;

	private Building _tempBuilding;
	private Plane _plane;
	private Ray _ray;
	private float _intersection;
	private Vector3 _worldPosition;
	private int _positionX;
	private int _positionZ;
	private Vector3 _positionNew;
	private bool _isAvailbale = false;

	private void Update()
	{
		if (_tempBuilding != null)
		{
			_plane = new Plane(Vector3.up, Vector3.zero);
			_ray = _camera.ScreenPointToRay(Input.mousePosition);

			if (_plane.Raycast(_ray, out _intersection))
			{
				_worldPosition = _ray.GetPoint(_intersection);

				_positionX = Mathf.RoundToInt(_worldPosition.x);
				_positionZ = Mathf.RoundToInt(_worldPosition.z);

				_positionNew = new Vector3(_positionX, 0, _positionZ);

				_tempBuilding.transform.position = _positionNew;

				if (Input.GetMouseButtonDown(0))
				{
					_tempBuilding = null;
				}
			}
		}
	}
	
	public void PlaceFlag(Building building)
	{
		if (_tempBuilding != null)
		{
			Destroy(_tempBuilding);
		}

		Destroy(GameObject.FindWithTag("Flag"));

		_tempBuilding = Instantiate(building);
	}

	public void StartBuilding(CommandCenter commandCenter)
	{
		Debug.Log(commandCenter.gameObject.name);
		Debug.Log(_positionNew);
		
		commandCenter = Instantiate(_commandCenterPrefab, _positionNew, Quaternion.identity);
	}

	public Vector3 PositionFlag()
	{
		return _positionNew;
	}
}
