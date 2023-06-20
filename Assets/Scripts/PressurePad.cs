using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField]
    private float _triggerDistance;
    private MeshRenderer _displayMeshRenderer;


    private void Start()
    {
        _displayMeshRenderer = GetComponentInChildren<MeshRenderer>();
        if ( _displayMeshRenderer == null )
        {
            Debug.LogWarning("My Mesh Renderer is Null!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Movable")
        {
            float distance = Vector3.Distance(other.transform.position, this.transform.position);
            if (distance < _triggerDistance)
            {
                Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
                if(otherRigidbody == null)
                {
                    Debug.LogWarning("Rigidbody is null!");
                    return;
                }

                MeshRenderer otherMeshRenderer = other.gameObject.GetComponent<MeshRenderer>();
                if(otherMeshRenderer == null)
                {
                    Debug.LogWarning("Mesh Renderer is Null!");
                    return;
                }

                otherRigidbody.isKinematic = true;
                otherMeshRenderer.material.color = Color.red;
                _displayMeshRenderer.material.color = Color.red;
                Destroy(this);
            }
        }
    }
}
