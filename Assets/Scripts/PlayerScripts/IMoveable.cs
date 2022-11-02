using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void MoveForward();

    void MoveHorizontally(Vector2 direction);

    bool IsOutOfRange();
}
