using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Services : MonoBehaviour
{
	public void HandleGravity(int curX)
	{
		for(int yInd = 0; yInd < GameSettings.Y_AXIS_POSITIONS_COUNT; yInd++)
		{
			int xOrigin = GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[0];
			int xTarget = xOrigin;
			int yTarget = FindHighestBlockadeInLanes(GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords)+1;
			int[] dim = GetDimOfForm(GameEngine.Instance.Positions [xOrigin] [yInd].content.BackgroundtileForm);
			int width = dim [0];
			int height = dim [1];
			BackgroundObject tile = GameEngine.Instance.Positions [xOrigin] [yInd].content;
			tile.YDestination = yTarget;
			for(int index = 0; index < width; index++)
			{
				for(int index2 = 0; index2 < height; index2++)
				{
					if(index2 + xOrigin < GameSettings.X_AXIS_POSITIONS_COUNT && index + yInd < GameSettings.Y_AXIS_POSITIONS_COUNT)
						GameEngine.Instance.Positions[index2+xOrigin][index+yInd].content = null;
				}	
			}
			for(int index3 = 0; index3 < width; index3++)
			{
				for(int index4 = 0; index4 < height; index4++)
				{
					if(index4 + xTarget < GameSettings.X_AXIS_POSITIONS_COUNT && index3 + yTarget < GameSettings.Y_AXIS_POSITIONS_COUNT)
						GameEngine.Instance.Positions[index4+xTarget][index3+yTarget].content = tile;
				}	
			}
		}
	}

	public void ManagePosition()
	{
		Vector3 screenMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		int curX = (int)screenMousePos.x/GameSettings.X_AXIS_OBJECTS_LENGTH;
		int curY = (int)screenMousePos.y/GameSettings.Y_AXIS_OBJECTS_LENGTH;
		if(curX < 0 
		   || curX >= GameSettings.X_AXIS_POSITIONS_COUNT 
		   || curY < 0 
		   || curY >= GameSettings.Y_AXIS_POSITIONS_COUNT) 
			return;
		
		if (GameEngine.Instance.Positions[curX][curY].content != null 
		    && !GameEngine.Instance.Positions[curX][curY].content.IsMoving) 
			DestroyContent(GameEngine.Instance.Positions[curX][curY]);
	}
	
	public void HandleCreationOfNewTile(int currentXKoord)
	{
		for(int i = 0; i < GameSettings.Y_AXIS_POSITIONS_COUNT; i++)
			RegisterNewTile(currentXKoord, FittingForm(currentXKoord));
		Debug.Log("HandleCreationOfNewTile being used in XCoord "+currentXKoord+". Putting out Positions:\n"+GameEngine.Instance.DebugPositionsString());
	}

	private void DestroyContent(Position target)
	{
		DestroyImmediate (target.content.gameObject);
		GameEngine.Instance.CheckHoles ();
	}

	private GameSettings.Form FittingForm(int xPos)
	{
		GameSettings.Form tileForm = GameSettings.Form.giant;
		bool fitting = false;
		while(!fitting)
		{
			fitting = IsFormFitting(xPos, GetDimOfForm((GameSettings.Form)Random.Range(0, GameSettings.FORM_ARRAY_LENGTH))[0]);
		}
		return tileForm;
	}

	private bool IsFormFitting(int xPos, int width)
	{
		if (!(xPos < GameSettings.X_AXIS_POSITIONS_COUNT - width))
			return false;
		bool doesntFit = true;
		for(int index = 0; index < width; index++)
			doesntFit = doesntFit || GameEngine.Instance.Positions[xPos+index][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content != null;
		return !doesntFit;
	}

	private BackgroundObject SpawnObject(GameSettings.Form form, List<int> xKoords, int destination)
	{
		Object prefab = new Object ();
		switch (form) 
		{
		case GameSettings.Form.simple:
			prefab = Resources.Load (GameSettings.BACKGROUND_SIMPLE_PREFAB_NAME);
			break;
		case GameSettings.Form.horizontal:
			prefab = Resources.Load (GameSettings.BACKGROUND_HORIZONTAL_PREFAB_NAME);
			break;
		case GameSettings.Form.vertical:
			prefab = Resources.Load (GameSettings.BACKGROUND_VERTICAL_PREFAB_NAME);
			break;
		case GameSettings.Form.quad:
			prefab = Resources.Load (GameSettings.BACKGROUND_QUAD_PREFAB_NAME);
			break;
		case GameSettings.Form.giant:
			prefab = Resources.Load (GameSettings.BACKGROUND_GIANT_PREFAB_NAME);
			break;
		case GameSettings.Form.horizontalLong:
			prefab = Resources.Load (GameSettings.BACKGROUND_HORIZONTALLONG_PREFAB_NAME);
			break;
		case GameSettings.Form.verticalLong:
			prefab = Resources.Load (GameSettings.BACKGROUND_VERTICALLONG_PREFAB_NAME);
			break;
		case GameSettings.Form.immense:
			prefab = Resources.Load (GameSettings.BACKGROUND_IMMENSE_PREFAB_NAME);
			break;
		}
		int xKoord = xKoords [0] * GameSettings.X_AXIS_OBJECTS_LENGTH;
		GameObject output = (GameObject)Instantiate (prefab, new Vector3 (xKoord, GameEngine.Instance.SpawnHeight, 0f), Quaternion.identity);
		Transform newRoot;
		string newRootName = "Lane " + xKoords [0];
		if (GameEngine.Instance.BackgroundContainer.transform.FindChild (newRootName) == null) 
		{
			newRoot = new GameObject (newRootName).transform;
			newRoot.parent = GameEngine.Instance.BackgroundContainer.transform;
		} 
		else
		{
			newRoot = GameEngine.Instance.BackgroundContainer.transform.FindChild (newRootName);
		}
		output.transform.parent = newRoot;
		output.GetComponent<BackgroundObject> ().YDestination = destination;
		output.GetComponent<BackgroundObject> ().XAxisKoords = xKoords;
		return output.GetComponent<BackgroundObject> ();
	}

	private void RegisterNewTile(int curX, GameSettings.Form form)
	{
		int[] dimensions = GetDimOfForm(form);
		int width = dimensions [0];
		int height = dimensions [1];
		List<int> xKoords = new List<int>();
		for(int index = 0; index < width; index++) xKoords.Add(curX+index);
		int yDest = FindHighestBlockadeInLanes(xKoords)+1;
		SetNewTilePositions(width, height, curX, yDest, SpawnObject(form, xKoords, yDest));
	}
	
	private void SetNewTilePositions(int width, int height, int xCoord, int yDest, BackgroundObject newTile)
	{
		for(int index = 0; index < width; index++)
		{
			for(int index2 = 0; index2 < height; index2++)
			{
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-index2)
					GameEngine.Instance.Positions[xCoord+index][yDest+index2].content = newTile;
			}
		}
	}
	
	private int FindHighestBlockadeInLanes(List<int> xCoordinates)
	{
		List<int> counters = new List<int> ();
		for(int j = 0; j < xCoordinates.Count; j++) counters.Add(GameSettings.Y_AXIS_POSITIONS_COUNT-1);
		for(int index = 0; index < xCoordinates.Count; index++)
		{
			while(GameEngine.Instance.Positions[xCoordinates[index]][counters[index]].content == null)
			{
				counters[index]--;
				if(counters[index] < 0) break;
			}
		}
		int highestNumber = 0;
		foreach(int number in counters)
		{
			highestNumber = Mathf.Max(highestNumber, number);
		}
		return highestNumber; 
	}

	private int[] GetDimOfForm(GameSettings.Form form)
	{
		int[] dim = new int[]{};
		switch(form)
		{
		case GameSettings.Form.simple:
			dim = new int[]{1,1};
			break;
		case GameSettings.Form.horizontal:
			dim = new int[]{2,1};
			break;
		case GameSettings.Form.vertical:
			dim = new int[]{1,2};
			break;
		case GameSettings.Form.quad:
			dim = new int[]{2,2};
			break;
		case GameSettings.Form.giant:
			dim = new int[]{2,3};
			break;
		case GameSettings.Form.horizontalLong:
			dim = new int[]{3,1};
			break;
		case GameSettings.Form.verticalLong:
			dim = new int[]{1,3};
			break;
		case GameSettings.Form.immense:
			dim = new int[]{3,3};
			break;
		}
		return dim;
	}
}
