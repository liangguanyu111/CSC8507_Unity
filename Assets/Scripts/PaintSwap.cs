using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSwap : MonoBehaviour
{
    public Color color;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.R))
        {
            PlayerInput pi;
            other.TryGetComponent<PlayerInput>(out pi);

            pi.ColorSwap(color);
        }
    }
}
