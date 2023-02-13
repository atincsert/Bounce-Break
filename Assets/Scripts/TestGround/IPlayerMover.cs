using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMover
{
    void Bounce();

    void RestrictMaxHeight();

    void SpeedUp();
}
