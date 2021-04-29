using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public SpriteRenderer background;
    public GameObject edgeWarning;

    float warningTime = 0f;

    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        edgeWarning.SetActive(false);
    }

    void Update()
    {
        warningTime -= Time.deltaTime;
        if (warningTime <= 0f)
        {
            edgeWarning.SetActive(false);
        }

        if (PlayerController.playerInstance)
        {
            Vector3 pos = PlayerController.playerInstance.transform.position;
            pos.z = -10;
            transform.position = pos;

            Bounds camBounds = OrthographicBounds(cam);
            Bounds BGBounds = background.bounds;

            background.transform.position = PlayerController.playerInstance.transform.position * 0.5f;
            BGBounds.center = background.transform.position;

            if (BGBounds.max.y < camBounds.max.y)
            {
                transform.position += Vector3.up * (BGBounds.max.y - camBounds.max.y);
            }
            if (BGBounds.max.x < camBounds.max.x)
            {
                transform.position += Vector3.right * (BGBounds.max.x - camBounds.max.x);
            }
            if (BGBounds.min.y > camBounds.min.y)
            {
                transform.position += Vector3.up * (BGBounds.min.y - camBounds.min.y);
            }
            if (BGBounds.min.x > camBounds.min.x)
            {
                transform.position += Vector3.right * (BGBounds.min.x - camBounds.min.x);
            }

            pos.z = background.transform.position.z;
            if (!BGBounds.Contains(pos))
            {
                var rb = PlayerController.playerInstance.GetComponent<Rigidbody2D>();
                rb.velocity = -rb.velocity;
                edgeWarning.SetActive(true);
                warningTime = 2f;
            }
        }
    }

    public Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * camera.aspect, cameraHeight, 0));
        return bounds;
    }

    private void OnDrawGizmosSelected()
    {
        if (cam)
        {
            Bounds camBounds = OrthographicBounds(cam);
            Bounds BGBounds = background.bounds;

            Gizmos.color = Color.red;
            Gizmos.DrawCube(BGBounds.center, BGBounds.size);

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(camBounds.center, camBounds.size);
        }
    }
}
