using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _collectiblesText;
    
    private void Start()
    {
        
    }

    
    private void Update()
    {
        
    }

    public void UpdateCollectibleValue(int value)
    {
        _collectiblesText.text = value.ToString();
    }
}
