using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class DrawRoad : MonoBehaviour {
    private const float E = 2.71828182846f;

    private List<Vector2> _points = new();

    private void Start() {
        for ( float x = 0 ; x < 10f ; x += 0.1f ) {

            float t = Sinh( x );
            _points.Add( new Vector2( x, -GetYCoordinate( t ) ) );
        }
    }

    private void OnDrawGizmos() {
        if ( _points != null && _points.Count > 0 ) {
            for ( int i = 0 ; i < _points.Count - 1 ; i++ ) {
                UnityEditor.Handles.DrawAAPolyLine( _points[ i ], _points[ i + 1 ] );
            }
        }
    }

    private float GetXCoordinate( float t ) {

        return Mathf.Log( t + Mathf.Sqrt( 1 + ( t * t ) ) );
    }

    private float GetYCoordinate( float t ) {
        t %= 0.250f;
        t *= 8f;
        t -= 1f;
        return Mathf.Sqrt( 1 + ( t * t ) );
    }

    private Vector2 GetXYCoordinate( float t ) {
        float x = GetXCoordinate( t );
        float y = -Cosh( x );

        return new Vector2( x, y );
    }

    private float Sinh( float x ) {
        return ( Mathf.Pow( E, x ) - Mathf.Pow( E, -x ) ) / 2f;
    }

    private float Cosh( float x ) {
        return ( Mathf.Pow( E, x ) + Mathf.Pow( E, -x ) ) / 2f;
    }
}