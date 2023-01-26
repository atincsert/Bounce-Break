using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMover
{
    void BounceConditions();

    void RestrictMaxHeight();

    void SpeedUp();
}
