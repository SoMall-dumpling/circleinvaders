using UnityEngine;

public class MoveWithKeyboard : MonoBehaviour {

    private float speedRadians;
    private float r;

    void Start()
    {
        speedRadians = 2;
        r = transform.position.magnitude;
    }

	void Update ()
    {
        // TODO add acceleration

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveAngle(speedRadians);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveAngle(-speedRadians);
        }
	}

    void MoveAngle(float radians)
    {
        float currentAngleRadian = Mathf.Atan2(transform.position.y, transform.position.x);
        float newAngleRadian = currentAngleRadian + radians * Time.deltaTime;

        float currentAngleDegree = currentAngleRadian * (180 / Mathf.PI);
        float newAngleDegree = newAngleRadian * (180 / Mathf.PI);

        Vector2 newPosition = transform.position;
        newPosition.x = r * Mathf.Cos(newAngleRadian);
        newPosition.y = r * Mathf.Sin(newAngleRadian);
        transform.position = newPosition;

        // transform.rotation = Quaternion.FromToRotation(new Vector3(0, 0, 0), transform.forward);
        transform.Rotate(new Vector3(0,0,1) * radians * 180 / Mathf.PI * Time.deltaTime);
    }
}
