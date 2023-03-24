using UnityEngine;

public sealed class Motion : MonoBehaviour {
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float angularSpeed;

    private void Update() {

        if ( Input.GetKey( KeyCode.W ) ) {
            transform.Translate( Time.deltaTime * horizontalSpeed * Vector3.right, Space.World );
            transform.rotation *= Quaternion.AngleAxis( Time.deltaTime * angularSpeed, Vector3.back );
        }
        else if ( Input.GetKey( KeyCode.S ) ) {
            transform.Translate( Time.deltaTime * horizontalSpeed * Vector3.left, Space.World );
            transform.rotation *= Quaternion.AngleAxis( Time.deltaTime * angularSpeed, Vector3.forward );
        }
    }
}