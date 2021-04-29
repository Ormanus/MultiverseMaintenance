using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;

    private void Awake()
    {
        speed += Random.Range(-speed * 0.2f, speed * 0.2f);
        if (Random.value > 0.5f)
            speed = -speed;
    }

    void Update()
    {
        transform.localEulerAngles += new Vector3(0f, 0f, Time.deltaTime * speed);
    }
}
