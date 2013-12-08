using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

	public float speed = 2000;

	// Update is called once per frame
	void FixedUpdate()
	{
        if (networkView.isMine)
        {
            //move handling
            float dx = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            float dy = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

            Vector3 walkAcceleration = new Vector3(dx, 0, dy);

            rigidbody.AddForce(walkAcceleration, ForceMode.Acceleration);

            //rotation / aim handling
            Vector2 lookDir = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
            float rotationY = Vector2.Angle(Vector2.up, lookDir);

            if (lookDir.x < 0) //negating the angle if the mouse is in the left half of the screen
                rotationY = -rotationY;

            transform.eulerAngles = new Vector3(0, rotationY, 0);
            rigidbody.angularVelocity = Vector3.zero;
        }
	}
}
