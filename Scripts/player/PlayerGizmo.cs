using UnityEngine;
using System.Collections;

public class PlayerGizmo : MonoBehaviour
{

    public Color _color = Color.green;
    public float _radius = 0.1f;

    void OnDrowGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
