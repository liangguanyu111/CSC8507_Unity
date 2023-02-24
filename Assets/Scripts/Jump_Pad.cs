using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Pad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&Input.GetKeyDown(KeyCode.Space))
        {
 
            PlayerInput pi;
            other.TryGetComponent<PlayerInput>(out pi);

            pi.enhanceJump = true;
        }
    }

}
