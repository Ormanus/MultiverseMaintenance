using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Vector3 center = Vector3.zero;
    public float range = 10f;
    public float year = 60f;
    float time = 0f; //Rad

    private void Awake()
    {
        Vector3 delta = transform.position - center;
        delta = delta.normalized * range;
        delta.z = 0f;

        transform.position = center + delta;

        time = Mathf.Atan2(delta.y, delta.x);
    }

    private void Update()
    {
        time += Time.deltaTime * Mathf.PI * 2f / year;
        transform.position = center + new Vector3(Mathf.Cos(time), Mathf.Sin(time)) * range;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(center, range);
    }
}
