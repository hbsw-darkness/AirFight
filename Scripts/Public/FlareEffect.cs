using UnityEngine;
using System.Collections;

public class FlareEffect : MonoBehaviour {

    public float destroyAfterSeconds = 2.0f;

    void Awake()
    {
        // Destroy gameobject after delay
        Destroy(gameObject, destroyAfterSeconds);
    }
}