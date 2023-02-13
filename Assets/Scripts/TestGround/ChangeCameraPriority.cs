using UnityEngine;
using Cinemachine;

public class ChangeCameraPriority : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Camera;
    //public CinemachineVirtualCamera ballCamera;
    //public CinemachineVirtualCamera arrowCamera;
    //public CinemachineVirtualCamera hammerCamera;

    public GameObject ball;
    public GameObject arrow;
    public GameObject hammer;

    private void Start()
    {
        var weapon = SaveManager.ChoosenWeaponAsEnum; 

        // Set the camera priorities based on which object is on screen
        if (weapon == SaveManager.Weapon.Ball || weapon == SaveManager.Weapon.None)
        {
            Camera.Follow = ball.transform;
            //ballCamera.Priority = 10;
            //arrowCamera.Priority = 0;
            //hammerCamera.Priority = 0;
        }
        else if (weapon == SaveManager.Weapon.Arrow)
        {
            Camera.Follow = arrow.transform;
            //ballCamera.Priority = 0;
            //arrowCamera.Priority = 10;
            //hammerCamera.Priority = 0;
        }
        else if (weapon == SaveManager.Weapon.Hammer)
        {
            Camera.Follow = hammer.transform;
            //ballCamera.Priority = 0;
            //arrowCamera.Priority = 0;
            //hammerCamera.Priority = 10;
        }
    }
}

