using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _floors; // 0 = top, 1 = bottom
    [SerializeField]
    private float _elevatorSpeed;
    public bool _elevatorCalled = false;
    private int _targetFloor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform; // Stops elevator jitter
            CallElevatorTo(0); // Top floor
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;// Stops elevator jitter
        }
    }

    public void CallElevatorTo(int direction)// 0 = top, 1 = bottom
    {
        _targetFloor = direction;
        _elevatorCalled = true;
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = this.transform.position;
        if (_elevatorCalled)
        { 
            if(currentPosition != _floors[_targetFloor]) 
            {
                transform.position = Vector3.MoveTowards(transform.position, _floors[_targetFloor], _elevatorSpeed * Time.deltaTime);
            }
            else
            {
                _elevatorCalled = false;
            }
        }
    }
}
