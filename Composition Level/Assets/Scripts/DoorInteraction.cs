using TMPro;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
	public Transform leftDoor;
	public Transform rightDoor;
	public TextMeshProUGUI interactionText; // Drag the TextMeshProUGUI element here
	public float openAngle = 90f; // Angle to open doors
	public float openSpeed = 2f; // Speed of door opening
	public float interactionRange = 3f; // Range within which player can open the door
	private bool isPlayerInRange = false;
	private bool isDoorOpen = false;

	private Quaternion leftDoorClosedRotation;
	private Quaternion rightDoorClosedRotation;
	private Quaternion leftDoorOpenRotation;
	private Quaternion rightDoorOpenRotation;

	private void Start()
	{
		// Set the original rotations of the doors as the "closed" rotations
		leftDoorClosedRotation = leftDoor.rotation;
		rightDoorClosedRotation = rightDoor.rotation;

		// Calculate the open rotation by rotating around the Y-axis
		leftDoorOpenRotation = leftDoor.rotation * Quaternion.Euler(0, openAngle, 0);
		rightDoorOpenRotation = rightDoor.rotation * Quaternion.Euler(0, -openAngle, 0);

		// Hide the interaction text at the start
		interactionText.gameObject.SetActive(false);
	}

	private void Update()
	{
		// Check for player input to open or close the door
		if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
		{
			isDoorOpen = !isDoorOpen;
			interactionText.gameObject.SetActive(false); // Hide text after interaction
		}

		// Smoothly open or close the doors
		if (isDoorOpen)
		{
			leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorOpenRotation, Time.deltaTime * openSpeed);
			rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorOpenRotation, Time.deltaTime * openSpeed);
		}
		else
		{
			leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorClosedRotation, Time.deltaTime * openSpeed);
			rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorClosedRotation, Time.deltaTime * openSpeed);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// Check if the player entered the trigger range
		if (other.CompareTag("Player"))
		{
			isPlayerInRange = true;
			interactionText.gameObject.SetActive(true); // Show text when player is in range
		}
	}

	private void OnTriggerExit(Collider other)
	{
		// Check if the player left the trigger range
		if (other.CompareTag("Player"))
		{
			isPlayerInRange = false;
			interactionText.gameObject.SetActive(false); // Hide text when player leaves range
		}
	}
}
