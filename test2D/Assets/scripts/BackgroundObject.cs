using UnityEngine;
using System.Collections;

public class BackgroundObject : MonoBehaviour 
{
	public GameSettings.Form BackgroundtileForm;

	[HideInInspector]
	public int YDestination = GameSettings.Y_AXIS_POSITIONS_COUNT-1;
	[HideInInspector]
	public bool IsMoving = true;
	[HideInInspector]
	public int[] XAxisKoords;

	void Update () 
	{
		IsMoving = false;
		float targetYKoord = GameSettings.Y_AXIS_OBJECTS_LENGTH * YDestination;
		if (transform.position.y > targetYKoord) 
		{
			transform.position -= new Vector3 (0f, GameSettings.BACKGROUND_TILE_MOVEMENT_SPEED * Time.deltaTime, 0f);
			IsMoving = true;
		}
	}
}