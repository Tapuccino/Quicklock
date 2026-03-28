using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _centre;

    public int speed = 0;

    public bool clockwise = true;


    private void Update()
    {
        MoveAroundCentre();
    }


    private void MoveAroundCentre()
    {
        Vector3 direction = clockwise ? Vector3.back : Vector3.forward;

        transform.RotateAround(_centre.position, direction, (speed + 10) * Time.deltaTime * 10);
    }
}
