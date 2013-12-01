/*
Main mission of the script is to follow an object around as it moves.
The object of this component should NOT be a child object of the target.

Params
target - the transform it should be following
distance - the distance in z axis.
height  - the height of the Camera.

So the real distance would be (according to pythagoras) sqrt(distance^2+height^2).


*/

var target : Transform;
var distance : float;
var height : float;

function Update(){
	if(!target.gameObject.activeSelf)
		return;
    transform.position.z = target.position.z - distance;
    transform.position.y = target.position.y + height;
    transform.position.x = target.position.x;
    transform.LookAt(target.position);
}