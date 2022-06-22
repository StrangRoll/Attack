using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieTransition : Transition
{
    void Update()
    {
        if (Target == null)
            NeedTransit = true;
    }
}
