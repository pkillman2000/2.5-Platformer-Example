using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _callButton;
    [SerializeField]
    private int _coinsNeeded;
    [SerializeField]
    Elevator _elevator;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            int numberOfCoins = other.GetComponent<Player>().GetNumberOfCoins();
            if (Input.GetKeyDown(KeyCode.E) && numberOfCoins >= _coinsNeeded)
            {
                _callButton.material.color = Color.green;
                _elevator.CallElevatorTo(1); // bottom floor
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Elevator")
        {
            _callButton.material.color = Color.red;
        }
    }
}
