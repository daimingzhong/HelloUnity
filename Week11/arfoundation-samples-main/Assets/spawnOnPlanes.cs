using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class spawnOnPlanes : MonoBehaviour
{
    [SerializeField] 
    GameObject PlacedPrefab;

    GameObject spawnObject;

    // There will be more than 1 item that is trackable.
    // The closest to us physically will be the 1st in the Array.
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    ARRaycastManager m_raycastManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        m_raycastManager = GetComponent<ARRaycastManager>();
    }
    
    bool GetTouch(out Vector2 touch_pos)
    {
        // When we have a touch (fist touch), we will store the position.
        if (Input.touchCount > 0)
        {
            touch_pos = Input.GetTouch(0).position;
            return true;
        }
        touch_pos = default;
        return false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GetTouch(out Vector2 touch_pos) == false)
        {
            return;
        }

        if (m_raycastManager.Raycast(touch_pos, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            // If we don't have a spawn object, we create one. Otherwise
            if (spawnObject == null)
            {
                spawnObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnObject.transform.position = hitPose.position;
            }
        }
    }
    
}
