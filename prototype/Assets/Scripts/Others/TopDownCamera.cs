using UnityEngine;
using System.Collections;

/*
Main mission of the script is to follow an object around as it moves.
The object of this component should NOT be a child object of the target.

Params
target - the transform it should be following
distance - the distance in z axis.
height  - the height of the Camera.

So the real distance would be (according to pythagoras) sqrt(distance^2+height^2).
*/

public class TopDownCamera : MonoBehaviour
{

    public Transform target;
    public float distance;
    public float height;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!target.gameObject.activeSelf)
            return;

        float z = target.position.z - distance;
        float y = target.position.y + height;
        float x = target.position.x;

        transform.position = new Vector3(x, y, z);

        transform.LookAt(target.position);
    }
}


