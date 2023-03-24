using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class SquareWheel : MonoBehaviour {
    private const float TAU = 6.28318530717959f;

    [Range( 0f, 10f ), SerializeField] private float t;

    public float GetRadius( float t ) {
        t %= 0.250f;
        t *= 8f;
        t -= 1f;
        return Mathf.Sqrt( 1 + ( t * t ) );
    }

    /// <returns>Angle in Radian</returns>
    public float GetAngle( float t ) {

        return Mathf.Atan( t );
    }

    private void OnDrawGizmos() {
#if UNITY_EDITOR
        Handles.color = Color.blue;
        Handles.DrawSolidDisc( transform.position.Y( -GetRadius( t ) ), Vector3.back, 0.025f );
#endif

        transform.rotation = Quaternion.AngleAxis( TAU * Mathf.Rad2Deg * ( t + -0.125f ), Vector3.back );
    }
}

public static class ExtensionMethods {
    public static Vector3 Y( this Vector3 v, float y ) {
        return new Vector3( v.x, y, v.z );
    }
}