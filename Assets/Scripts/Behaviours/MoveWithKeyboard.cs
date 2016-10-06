using UnityEngine;

public class MoveWithKeyboard : MonoBehaviour
{

    private float speedRadians;

    void Start()
    {
        // TODO share speed with MoveWithTouch
        speedRadians = 2;
    }

    void Update()
    {
        // TODO add acceleration

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayerHandler.MoveAngle(gameObject, speedRadians);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayerHandler.MoveAngle(gameObject,- speedRadians);
        }
    }
}
