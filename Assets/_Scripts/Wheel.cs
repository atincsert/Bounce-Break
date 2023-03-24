using UnityEngine;
using UnityEditor;

public sealed class Wheel : MonoBehaviour {
    private const float TAU = 6.28318530717959f;

    public Vector3 Axle { get { return transform.position; } }

    [SerializeField] private float discThickness;
    [Range( 0f, 1f ), SerializeField] private float turn;

    private float GetRadiusFromRadianAngle( float tt ) {
        int octant = GetOctant( tt );
        bool isOctantWhatever = octant % 2 == 0;

        tt = tt * 360f;
        tt = isOctantWhatever ? tt % 45f : 45f - ( tt % 45f );

        float tan = Mathf.Sin( tt * Mathf.Deg2Rad ) / Mathf.Cos( tt * Mathf.Deg2Rad );

        return Mathf.Sqrt( 1 + ( tan * tan ) );
    }

    private int GetOctant( float t ) {
        return Mathf.FloorToInt( t / 0.125f );
    }

    private float GetRoadXCoordinate( float t ) {
        return Mathf.Sqrt( 1 + ( t * t ) ) + Mathf.Log( t + Mathf.Sqrt( 1 + ( t * t ) ) );
        //return Mathf.Log( t + Mathf.Sqrt( 1 + ( t * t ) ) );
    }

    private void OnDrawGizmos() {
        Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
        Handles.DrawWireDisc( Vector3.zero, Vector3.back, radius: Mathf.Sqrt( 2f ), discThickness );
        Handles.DrawWireDisc( Vector3.zero, Vector3.back, radius: 1f, discThickness );

        // Draw radius
        Vector3 direction = new Vector3( Mathf.Cos( turn * TAU ), Mathf.Sin( turn * TAU ), 0f );
        Handles.DrawAAPolyLine( Vector3.zero, direction.normalized * GetRadiusFromRadianAngle( turn ) );

        for ( float i = 0f ; i < 5f ; i += 0.001f ) {
            Vector2 firstPoint = new Vector2( 8 * i/*GetRoadXCoordinate( i )*/, -GetRadiusFromRadianAngle( i ) );
            Vector2 lastPoint = new Vector2( 8 * ( i + 0.001f )/*GetRoadXCoordinate( i + 0.001f )*/, -GetRadiusFromRadianAngle( i + 0.001f ) );
            Handles.DrawAAPolyLine( firstPoint, lastPoint );
        }

        Handles.color = Color.red;
        Handles.DrawDottedLine( Vector3.left * 100f, Vector3.right * 100f, 5 );
        Handles.DrawSolidDisc( Axle, Vector3.back, 0.02f );

    }
}