using UnityEngine;
using System.Collections;

public class MoveWithTouch : MonoBehaviour
{
    private float speedRadians;

    void Start()
    {
        // TODO share speed with MoveWithKeyboard
        speedRadians = 2;
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    MoveTowardsPosition(touch.position);
                    return;
            }
        }

        // for testing with mouse
        if (Input.GetMouseButton(0))
        {
            MoveTowardsPosition(Input.mousePosition);
        }
    }

    void MoveTowardsPosition(Vector3 pixelPosition)
    {
        // TODO add acceleration
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pixelPosition);
        worldPosition.z = 0;
        float r = transform.position.magnitude;
        Vector3 circlePosition = r * worldPosition.normalized;
        float targetRadians = Mathf.Atan2(circlePosition.x, circlePosition.y);
        float playerRadians = Mathf.Atan2(transform.position.x, transform.position.y);
        float diff = targetRadians - playerRadians;
        if (diff < -Mathf.PI) diff = Mathf.PI;
        if (diff > Mathf.PI) diff = -Mathf.PI;
        if (diff < 0) MovePlayerHandler.MoveAngle(gameObject, speedRadians);
        if (diff > 0) MovePlayerHandler.MoveAngle(gameObject, -speedRadians);
    }
}
