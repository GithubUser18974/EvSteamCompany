using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectabelObjects : MonoBehaviour
{
 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.IncrementScore();
            this.gameObject.SetActive(false);
        }
    }
}
