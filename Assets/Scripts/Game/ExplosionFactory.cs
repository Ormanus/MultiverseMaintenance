using UnityEngine;
using System.Collections;

public class ExplosionFactory : MonoBehaviour
{
    public static ExplosionFactory Instance;

    public GameObject explosionPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateExplosion(Vector2 pos, float size = 1f)
    {
        Instantiate(explosionPrefab, pos, Quaternion.identity, null).transform.localScale = new Vector3(size, size, size);
    }
}
