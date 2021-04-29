using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Vector2 velocityVector;

    void Start()
    {
        Destroy(gameObject, 10f);
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(velocityVector.y, velocityVector.x) * Mathf.Rad2Deg);
    }

    void Update()
    {
        transform.position += (Vector3)velocityVector * Time.deltaTime;
    }
}
