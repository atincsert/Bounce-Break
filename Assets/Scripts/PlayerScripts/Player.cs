using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BallMovement ballMovement;
    [SerializeField] private ArrowMovement arrowMovement;
    [SerializeField] private HammerMovement hammerMover;

    public void SetPlayer(int i)
    {
        switch (i)
        {
            case 1:
                {
                    ballMovement.gameObject.SetActive(true);
                    arrowMovement.gameObject.SetActive(false);
                    hammerMover.gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    ballMovement.gameObject.SetActive(false);
                    arrowMovement.gameObject.SetActive(true);
                    hammerMover.gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    ballMovement.gameObject.SetActive(false);
                    arrowMovement.gameObject.SetActive(false);
                    hammerMover.gameObject.SetActive(true);
                    break;
                }
            default:
                {
                    ballMovement.gameObject.SetActive(true);
                    arrowMovement.gameObject.SetActive(false);
                    hammerMover.gameObject.SetActive(false);
                    break;
                }
        }
    }
}