using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDamageable 
{
    void TakeDamage(float amount); //dont care about what kind of object deal damage, just apply it
}
