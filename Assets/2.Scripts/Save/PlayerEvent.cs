using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : Manager
{
    public bool useStartCoupon { get; private set; } = false;

    public void UseStartCoupon()
    {
        useStartCoupon = true;
    }
}
