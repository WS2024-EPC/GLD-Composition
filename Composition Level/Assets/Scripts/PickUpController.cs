using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 5f;
    public Transform cameraTransform;
    public Text pickupText;
    public Text putText;

    public GameObject pickupObject;
    public GameObject putObject;
    public GameObject positionToPutObject;
    public GameObject placedObject;
    public Transform gate;

    public float gateTargetY = -4f;
    public float gateSpeed = 1.5f;

    private bool isPickedUp = false;
    private bool isPlaceable = false;
    private bool isPlaced = false;
    private bool moveGate = false;

    void Start()
    {
        pickupText.gameObject.SetActive(false);
        putText.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckObjectInViewAndRange();
        HandlePickup();
        if (moveGate)
        {
            Vector3 currentPosition = gate.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, gateTargetY, currentPosition.z);
            gate.position = Vector3.MoveTowards(currentPosition, targetPosition, gateSpeed * Time.deltaTime);
        }
    }

    void CheckObjectInViewAndRange()
    {
        if (pickupObject == null) return;

        Vector3 pickUpPos = Camera.main.WorldToViewportPoint(pickupObject.transform.position);
        if (pickUpPos.x >= 0f && pickUpPos.x <= 1f && pickUpPos.y >= 0f && pickUpPos.y <= 1f && pickUpPos.z > 0f && !isPickedUp)
        {
            float distance = Vector3.Distance(cameraTransform.position, pickupObject.transform.position);
            bool isInRange = distance <= pickupRange;
            if (isInRange)
            {
                pickupText.gameObject.SetActive(true);
                isPickedUp = true;
            }
        }

        Vector3 putPos = Camera.main.WorldToViewportPoint(positionToPutObject.transform.position);
        if (putPos.x >= 0f && putPos.x <= 1f && putPos.y >= 0f && putPos.y <= 1f && putPos.z > 0f && isPickedUp)
        {
            float distance = Vector3.Distance(cameraTransform.position, pickupObject.transform.position);
            bool isInRange = distance <= pickupRange;
            if (isInRange && !isPlaced)
            {
                putText.gameObject.SetActive(true);
                isPlaceable = true;
            }
        }
    }

    void HandlePickup()
    {
        if (isPickedUp && Input.GetKeyDown(KeyCode.F))
        {
            PickupObject(pickupObject);
            pickupText.gameObject.SetActive(false);
        }
        if(isPlaceable && Input.GetKeyDown(KeyCode.F))
        {
            placedObject.SetActive(true);
            putText.gameObject.SetActive(false);
            moveGate = true;
            isPlaced = true;
        }
    }

    void PickupObject(GameObject obj)
    {
        Debug.Log("Object picked up: " + obj.name);
        obj.SetActive(false);
    }
}
