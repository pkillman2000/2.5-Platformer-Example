using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private int _value;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null )
        {
            Debug.LogWarning("Player is Null!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _player.CollectYellowSphere(_value);
        Destroy(this.gameObject);
    }
}
