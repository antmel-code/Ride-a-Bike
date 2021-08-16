using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    Vector3 offset;

    [SerializeField, Range(0, 10)]
    float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // Save offet
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = Vector2.Lerp(transform.position, target.position - offset, speed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
