using UnityEngine;
using System.Collections;
/// <summary>
/// Health bar logic.
/// Has the responsibility to correctly display the amount of lives to the user.
/// Should be added to its own gameobject, as a component
/// 
/// </summary>

public class HealthBarLogic : MonoBehaviour
{	
	//the image that will be shown
	public Texture2D lifeImage;
	private int lives = 0;
	
	//can be modified to display the life in a special fashion. 
	public int startingX;
	public int startingY;
	public int widthPadding;
		
	//used for internal purposes.
	private int lifeImageWidth;
	private int lifeImageHeight;
	


	// Use this for initialization
	void Start ()
	{
		lifeImageWidth = lifeImage.width;
		lifeImageHeight = lifeImage.height;
		//Debug.Log ("life image width and height:: " + lifeImageWidth + "    " + lifeImageHeight);
	}
	
	void OnGUI ()
	{
		//we have to store the default value. otherwise all gui elements will be rendered
		//with transparant backgrounds.
		Color defaultColor = GUI.backgroundColor;
		GUI.backgroundColor = Color.clear;
		
		//simply draw the same amount of boxes as we have lives.
		for (int i = 0; i < lives; i++) {
			int x = startingX + i * (lifeImageWidth + widthPadding);
			int y = startingY;
			GUI.Box (new Rect (x, y, lifeImageWidth, lifeImageHeight), lifeImage);	
		}
		
		GUI.backgroundColor = defaultColor;
		
	}
	
	/// <summary>
	/// Set the total amount of life that should be shown.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void setLife (int amount)
	{
		lives = Mathf.Max (0, amount);
	}
}
