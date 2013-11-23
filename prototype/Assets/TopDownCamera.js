var target : Transform;
var distance : float;
var height : float;

function Update(){
	if(!target.active)
	return;
    transform.position.z = target.position.z - distance;
    transform.position.y = target.position.y + height;
    transform.position.x = target.position.x;
    transform.LookAt(target.position);
 
}