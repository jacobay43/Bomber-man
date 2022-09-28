using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    [SerializeField] float xOffset = 0;
    [SerializeField] float yOffset = 6;
    [SerializeField] float zOffset = 0;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + xOffset, target.position.y+yOffset, target.position.z + zOffset);
    }
}
