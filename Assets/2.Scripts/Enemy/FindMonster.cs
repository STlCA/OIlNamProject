using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMonster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy monster = collision.GetComponent<Enemy>();

        if (monster != null)
        {
            monster.SpecialAttacked();
        }
    }
}
