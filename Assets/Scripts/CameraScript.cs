using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        // Calculate the offset of the player to the camera
        offset = transform.position - player.transform.position;
    }

    // Set the camera position to the player position + the offset
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
