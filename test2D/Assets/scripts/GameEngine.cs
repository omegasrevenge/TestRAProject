using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour 
{
	[HideInInspector]
	public int SpawnHeight;

	public static GameEngine Instance;

	public List<List<Position>> Positions;

	public GameObject BackgroundContainer;

	void Start () 
	{
		Application.targetFrameRate = 30;
		Application.runInBackground = false;
		Instance = this;

		Positions = new List<List<Position>> ();
		
		for (int i = 0; i < GameSettings.X_AXIS_POSITIONS_COUNT; i++) 
		{
			Positions.Add(new List<Position>());
			for(int i2 = 0; i2 < GameSettings.Y_AXIS_POSITIONS_COUNT; i2++)
				Positions[i].Add(new Position());
		}

		SpawnHeight = GameSettings.Y_AXIS_POSITIONS_COUNT * GameSettings.Y_AXIS_OBJECTS_LENGTH + GameSettings.SPAWN_HEIGHT_OFFSET;
		for(int index = 0; index < GameSettings.X_AXIS_POSITIONS_COUNT; index++)
		{
			if(Positions[index][Positions[index].Count-1].content == null)
				Services.HandleCreationOfNewTile(index);
		}
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
			Services.ManagePosition();
	}

	public void CheckHoles()
	{
		for(int index = 0; index < GameSettings.X_AXIS_POSITIONS_COUNT; index++)
		{
			Services.HandleGravity(index);

			// if there is an empty space on the top a new tile should be created to fill it
			if(Positions[index][Positions[index].Count-1].content == null)
				Services.HandleCreationOfNewTile(index);
		}
	}
}
