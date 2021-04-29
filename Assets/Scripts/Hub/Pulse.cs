using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    void Update()
    {
        float s = 1f + Mathf.Sin(Time.time * 2f) * 0.2f;
        transform.localScale = new Vector3(s, s, s);
    }
}
