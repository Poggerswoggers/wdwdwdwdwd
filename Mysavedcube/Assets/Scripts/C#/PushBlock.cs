using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour {
	private Vector3 targetPosition;
	[SerializeField] private bool isMoving = false;
	[SerializeField] private float yOrigin;

	public Transform anchor;

	public LayerMask pushBlockFaceMask;

	public Transform pushBlockBubeMesh;
	Quaternion lastRotation;

	public Vector3 respawnPos;


	private float _rollSpeed = 400f;
	[SerializeField] private Vector3 _pivot;
	[SerializeField] private Vector3 _axis;

	void Start() {
		respawnPos = transform.position;
		targetPosition = transform.position;
		yOrigin = 0;
		lastRotation = Quaternion.identity;
	}

	void Update()	
	{

		if (anchor != null)
		{
			anchor.position = _pivot;
		}

        if (Mathf.RoundToInt(transform.position.y)!=0)
        {
			yOrigin = Mathf.RoundToInt(transform.position.y);

		}



		Vector2 pos = new Vector2(transform.position.x, transform.position.z),  //Stores its transform vectors
				tar = new Vector2(targetPosition.x, targetPosition.z);          //Stores target transform vectors

		if (Vector2.Distance(pos, tar) < 0.01)
		{
			isMoving = false;  //If the cube is very close to its target postion, it shouldn't be moving anymore
		}
		else if (isMoving)
		{
			// Fix position with lerp
			//transform.position = Vector3.Lerp(transform.position, targetPosition, 8 * Time.deltaTime);

			pushBlockBubeMesh.RotateAround(_pivot, _axis, _rollSpeed * Time.deltaTime);
			if (Quaternion.Angle(lastRotation, pushBlockBubeMesh.transform.rotation) > 90)
			{
				ResetPosition();
			}
		}

	}

	
	void ResetPosition()
	{
		//resets the rotation of the cube mesh and moves the playerCube
		//Obj to the position of the cubeMesh. Finally it centres the cube mesh back on the playerCube
		//cubeMesh.transform.rotation = Quaternion.Euler(Vector3.zero);
		lastRotation = pushBlockBubeMesh.transform.rotation = Quaternion.Euler(
			Mathf.Round(pushBlockBubeMesh.transform.rotation.eulerAngles.x / 90) * 90,
			Mathf.Round(pushBlockBubeMesh.transform.rotation.eulerAngles.y / 90) * 90,
			Mathf.Round(pushBlockBubeMesh.transform.rotation.eulerAngles.z / 90) * 90);

		transform.position = new Vector3(Mathf.Ceil(pushBlockBubeMesh.transform.position.x) - 0.5f,
			transform.position.y, Mathf.Ceil(pushBlockBubeMesh.transform.position.z) - 0.5f);
		pushBlockBubeMesh.transform.localPosition = Vector3.zero;
		isMoving = false;


	}

	public void Move(Vector3 direction, int distance) 
	{
		direction = new Vector3(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y), Mathf.RoundToInt(direction.z));
		Debug.Log(direction + " Move function ran");
		RaycastHit hit;
		Physics.Linecast(transform.position, transform.position + direction * -1, out hit, ~pushBlockFaceMask);
		Debug.DrawLine(transform.position, transform.position + direction * -1, Color.red);
		if (hit.collider != null && !hit.collider.isTrigger) {
			Debug.Log(hit.collider.name);
		} 
		else 
		{
			transform.position = new Vector3(Mathf.Ceil(transform.position.x) - 0.5f, yOrigin, Mathf.Ceil(transform.position.z) - 0.5f);
			targetPosition = transform.position + (direction * -distance);
			isMoving = true;
			targetPosition = new Vector3(targetPosition.x, yOrigin, targetPosition.z);
		}
		Debug.Log("Messi");
		if(direction == Vector3.right) //Go left
        {
			_pivot = new Vector3(-1, -1, 0);
			_axis = Vector3.forward;
        }
		if(direction == -Vector3.right)  //Go right
        {
			Debug.Log("yes");
			_pivot = new Vector3(1, -1, 0);  
			_axis = -Vector3.forward;
        }
		if(direction == -Vector3.forward)  //Go up
        {
			_pivot = new Vector3(0, -1, 1);
			_axis = Vector3.right;
        }
		if(direction == Vector3.forward)  //Go down
        {
			_pivot = new Vector3(0, -1, -1);
			_axis = -Vector3.right;
        }
		Debug.Log(_axis);
		_pivot = transform.position + (_pivot * 0.5f);
	}

	public bool IsMoving()
    {
		return isMoving;
    }

}