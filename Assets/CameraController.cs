using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraFollow;
    public Vector3 cameraOffset;
    public float followTightness;
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(
            cameraFollow.position.x + cameraOffset.x,
            cameraFollow.position.y + cameraOffset.y,
            cameraFollow.position.z + cameraOffset.z),
            transform.rotation);
            transform.LookAt(cameraFollow);
    }

    // Update is called once per frame
    void Update()
    {

        transform.SetPositionAndRotation(new Vector3(
            Mathf.Lerp(transform.position.x, cameraFollow.position.x + cameraOffset.x, followTightness * Time.deltaTime),
            Mathf.Lerp(transform.position.y, cameraFollow.position.y + cameraOffset.y, followTightness * Time.deltaTime),
            Mathf.Lerp(transform.position.z, cameraFollow.position.z + cameraOffset.z, followTightness * Time.deltaTime)),
            transform.rotation);

    }
}
