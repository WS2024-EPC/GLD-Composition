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

    private bool isPickedUp = false;

    void Start()
    {
        pickupText.gameObject.SetActive(false);
        putText.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckObjectInViewAndRange();
        HandlePickup();
    }

    void CheckObjectInViewAndRange()
    {
        if (pickupObject == null) return;

        Vector3 pickUpPos = Camera.main.WorldToViewportPoint(pickupObject.transform.position);
        if (pickUpPos.x >= 0f && pickUpPos.x <= 1f && pickUpPos.y >= 0f && pickUpPos.y <= 1f && pickUpPos.z > 0f && !isPickedUp)
        {
            float distance = Vector3.Distance(cameraTransform.position, pickupObject.transform.position);
            bool isInRange = distance <= pickupRange;
            if (isInRange && !isPickedUp)
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
            if (isInRange && isPickedUp)
            {
                putText.gameObject.SetActive(true);
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
    }

    void PickupObject(GameObject obj)
    {
        Debug.Log("Object picked up: " + obj.name);
        obj.SetActive(false);
    }
}
