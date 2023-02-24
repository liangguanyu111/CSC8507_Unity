using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponUp : PoweUp
{

    public override void Interct(GameObject obj)
    {
        PlayerController pc;
        if(obj.TryGetComponent<PlayerController>(out pc))
        {
            pc.WeaonUp();
            Destroy(this.gameObject);
        }
    }
}
