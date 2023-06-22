using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeRoll : MonoBehaviour {


	public Transform cubeMesh;        //reference the cubeMesh
	public bool rollForever = false;   //Roll Forvever
	private float rollSpeed = 400;
	[SerializeField] private bool isMoving = false;
	private RaycastHit hit;            //Raycast for collision detection

	public LayerMask playerLayerMask;

	public bool canMove;


	public Vector3 pivot;        
	private float cubeSize = 1; // Block cube size
	public static int steps;
	
	public enum CubeDirection {none, left, up, right, down}; //Cube dir enum enums are limited to the values you assigned it
	public CubeDirection direction = CubeDirection.none;     //Sets default dir as none

	public Quaternion lastRotation;

	
	void Start() {


		steps = 500; //Sets the max steps
		lastRotation = Quaternion.identity;
	}

	void Update() {

        if (Input.GetKeyDown(KeyCode.E))
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	



		if(direction == CubeDirection.none) {          //Simple sets the direction based on keyboard input
			if (Input.GetKeyDown(KeyCode.D)) {
				direction = CubeDirection.right;
				//DeductStepCount();
			}
			if (Input.GetKeyDown(KeyCode.A)) {
				direction = CubeDirection.left;
				//DeductStepCount();
			}
			if (Input.GetKeyDown(KeyCode.W)) {
				direction = CubeDirection.up;
				//DeductStepCount();
			}
			if (Input.GetKeyDown(KeyCode.S)) {
				direction = CubeDirection.down;       
				//DeductStepCount();

				//Input goes to a switch case
			}
		}
		//Calls else when cube direction != none, which happens after pressing any directional input
		else { 
			//this part is nran when we are not moving, checks if there is any collider infront of us. if yes dont move, else move
			if(!isMoving) { 
				if(CheckCollision(direction)) {
					isMoving = false;
					direction = CubeDirection.none;
					//
					if (hit.collider.gameObject.GetComponent<PushBlock>())
					{
						PushBlock PushBlockRef = hit.collider.gameObject.GetComponent<PushBlock>();
						//if collider with pushblock, send the direction vector3 to determine direction of cube pushed
						if (PushBlockRef.IsMoving()) return;
						PushBlockRef.Move((transform.position - hit.collider.transform.position).normalized, 1);
						
					}


				} else { 
					CalculatePivot();
					DeductStepCount();
					isMoving = true;
				}
			}

			//Rotates the cube mesh based on dir, if dir more than 90 resetPos()
			switch(direction) {
				case CubeDirection.right:
					cubeMesh.transform.RotateAround(pivot, -Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation,cubeMesh.transform.rotation)>90)
						ResetPosition();
					break;
				case CubeDirection.left:
					cubeMesh.transform.RotateAround(pivot, Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.up:
					cubeMesh.transform.RotateAround(pivot, Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.down:
					cubeMesh.transform.RotateAround(pivot, -Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90)
					{
						ResetPosition();
					}
					break;
			}
		}
		//Lose
		if(transform.position.y <= -10) {
			SceneManager.LoadScene("Lose");
		}
	}

	void ResetPosition() {
		//resets the rotation of the cube mesh and moves the playerCube
		//Obj to the position of the cubeMesh. Finally it centres the cube mesh back on the playerCube
		//cubeMesh.transform.rotation = Quaternion.Euler(Vector3.zero);
		lastRotation = cubeMesh.transform.rotation = Quaternion.Euler(
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.x / 90) * 90,
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.y / 90) * 90,
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.z / 90) * 90);

		transform.position = new Vector3(Mathf.Ceil(cubeMesh.transform.position.x) - 0.5f, 
			transform.position.y, Mathf.Ceil(cubeMesh.transform.position.z) - 0.5f);
		cubeMesh.transform.localPosition = Vector3.zero;
		isMoving = false;

		if(CheckCollision(direction) && hit.collider != null) {
			if(hit.collider.gameObject.GetComponent<PushBlock>()) {
				//Debug.Log("Hit a push");
				//hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position - hit.collider.transform.position).normalized, 1);
			}
		}

		if (!rollForever)
			direction = CubeDirection.none;
	}

	void CalculatePivot() {

		switch(direction) {
			case CubeDirection.right:
				pivot = new Vector3(1, -1, 0);
				break;
			case CubeDirection.left:
				pivot = new Vector3(-1, -1, 0);
				break;
			case CubeDirection.up:
				pivot = new Vector3(0, -1, 1);
				break;
			case CubeDirection.down:
				pivot = new Vector3(0, -1, -1);
				break;
		}

		// Calculates the point around which the block will flop
		pivot = transform.position + (pivot * cubeSize * 0.5f);
		if(GetComponent<AudioSource>())
			GetComponent<AudioSource>().Play(); // Play the flop sound 
	}

	bool CheckCollision(CubeDirection direction) {
		switch(direction) {
			case CubeDirection.right:
				Physics.Linecast(transform.position, transform.position + transform.right* 1, out hit, ~playerLayerMask);
				Debug.DrawLine(transform.position, transform.position + transform.right* 1, Color.black);
				break;
			case CubeDirection.left:
				Physics.Linecast(transform.position, transform.position + transform.right* -1, out hit, ~playerLayerMask);
				Debug.DrawLine(transform.position, transform.position + transform.right* -1, Color.black);
				break;
			case CubeDirection.up:
				Physics.Linecast(transform.position, transform.position + transform.forward* 1, out hit, ~playerLayerMask);
				Debug.DrawLine(transform.position, transform.position + transform.forward* 1, Color.black);
				break;
			case CubeDirection.down:
				Physics.Linecast(transform.position, transform.position + transform.forward* -1, out hit, ~playerLayerMask);
				Debug.DrawLine(transform.position, transform.position + transform.forward* -1, Color.black);
				break;
		}
		//this statement is false when i turn on convex collider trigger
		if(hit.collider == null || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player"))) {
			return false;
		} else {
			Debug.Log(hit.collider.gameObject);
			return true;
		}
	}

	void DeductStepCount() {
		steps -= 1;

		if(steps <= 0) {
			steps = 0;
		}
	}
}