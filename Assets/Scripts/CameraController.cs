using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform target;
    [SerializeField] private float smoothSpeed;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//MARKER dont forget to tag player as tag
    }

    private void Update()
    {
        //transform.position = new Vector3(target.position.x, target position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(target.position.x, target.position.y, transform.position.z), smoothSpeed * Time.deltaTime);
    }
}
