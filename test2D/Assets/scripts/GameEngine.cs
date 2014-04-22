using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour 
{
	[HideInInspector]
	public int SpawnHeight;

	public static GameEngine Instance;
	public static Services MyServices;

	public List<List<Position>> Positions;

	public GameObject BackgroundContainer;

	void Start () 
	{
		Application.targetFrameRate = 30;
		Application.runInBackground = false;
		Instance = this;
		MyServices = new Services ();

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
				MyServices.HandleCreationOfNewTile(index);
		}
		DrawDebugLines (Color.red, Mathf.Infinity);
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
			MyServices.ManagePosition();
	}

	public void CheckHoles()
	{
		for(int index = 0; index < GameSettings.X_AXIS_POSITIONS_COUNT; index++)
		{
			MyServices.HandleGravity(index);

			// if there is an empty space on the top a new tile should be created to fill it
			if(Positions[index][Positions[index].Count-1].content == null)
				MyServices.HandleCreationOfNewTile(index);
		}
	}

	public void DrawDebugLines(Color color, float duration)
	{
		for (int index1 = 0; index1 < GameSettings.X_AXIS_POSITIONS_COUNT+1; index1++) 
		{
			Debug.DrawLine(
				new Vector3(index1*GameSettings.X_AXIS_OBJECTS_LENGTH, 0f, 0f), 
				new Vector3(index1*GameSettings.X_AXIS_OBJECTS_LENGTH, 
			            GameSettings.X_AXIS_POSITIONS_COUNT*GameSettings.X_AXIS_OBJECTS_LENGTH, 0f), 
				color, 
				duration);
			Debug.DrawLine(
				new Vector3(0f, index1*GameSettings.X_AXIS_OBJECTS_LENGTH, 0f), 
				new Vector3(GameSettings.X_AXIS_POSITIONS_COUNT*GameSettings.X_AXIS_OBJECTS_LENGTH, 
			            index1*GameSettings.X_AXIS_OBJECTS_LENGTH, 0f), 
				color, 
				duration);
		}
	}
}
