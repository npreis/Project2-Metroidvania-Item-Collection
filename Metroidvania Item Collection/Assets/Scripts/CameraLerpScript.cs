using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpScript : MonoBehaviour
{
    public GameObject objectToFollow;
    public float speed = 5f;

    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);

        this.transform.position = position;
    }
}
