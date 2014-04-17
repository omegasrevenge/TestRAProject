using UnityEngine;
using System.Collections;

public class Services : MonoBehaviour 
{
	public static BackgroundObject SpawnObject(GameSettings.Form form, int[] xKoords, int destination)
	{
		Object prefab = new Object();
		switch(form)
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
		int xKoord = xKoords[0] * GameSettings.X_AXIS_OBJECTS_LENGTH;
		GameObject output = (GameObject)Instantiate (prefab, new Vector3 (xKoord, GameEngine.Instance.SpawnHeight, 0f), Quaternion.identity);
		output.transform.parent = GameEngine.Instance.BackgroundContainer.transform;
		output.GetComponent<BackgroundObject> ().YDestination = destination;
		return output.GetComponent<BackgroundObject> ();
	}

	public static void HandleGravity(int curX)
	{
		for(int yInd = 0; yInd < GameSettings.Y_AXIS_POSITIONS_COUNT; yInd++)
		{
			BackgroundObject curObj = GameEngine.Instance.Positions[curX][yInd].content;
			if(yInd != 0 && curObj != null && GameEngine.Instance.Positions[curX][yInd-1].content == null)
			{
				int count1 = GameSettings.Y_AXIS_POSITIONS_COUNT-1;
				int count2 = GameSettings.Y_AXIS_POSITIONS_COUNT-1;
				int count3 = GameSettings.Y_AXIS_POSITIONS_COUNT-1;
				switch(curObj.BackgroundtileForm)
				{
				case GameSettings.Form.simple:
					while(GameEngine.Instance.Positions[curX][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					MoveContent(curX, yInd, curX, count1+1);
					break;
				case GameSettings.Form.horizontal:
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[0]][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[1]][count2].content == null)
					{
						count2--;
						if(count2 < 1) break;
					}
					MoveContent(curX, yInd, curX, Mathf.Max(count2, count1)+1);
					break;
				case GameSettings.Form.vertical:
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[0]][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					MoveContent(curX, yInd, curX, count1+1);
					break;
				case GameSettings.Form.quad:
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[0]][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[1]][count2].content == null)
					{
						count2--;
						if(count2 < 1) break;
					}
					MoveContent(curX, yInd, curX, Mathf.Max(count2, count1)+1);
					break;
				case GameSettings.Form.giant:
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[0]][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[1]][count2].content == null)
					{
						count2--;
						if(count2 < 1) break;
					}
					MoveContent(curX, yInd, curX, Mathf.Max(count2, count1)+1);
					break;
				case GameSettings.Form.horizontalLong:
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[0]][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[1]][count2].content == null)
					{
						count2--;
						if(count2 < 1) break;
					}
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[2]][count3].content == null)
					{
						count3--;
						if(count3 < 1) break;
					}
					MoveContent(curX, yInd, curX, Mathf.Max(Mathf.Max(count2, count1), count3)+1);
					break;
				case GameSettings.Form.verticalLong:
					while(GameEngine.Instance.Positions[curX][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					MoveContent(curX, yInd, curX, count1+1);
					break;
				case GameSettings.Form.immense:
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[0]][count1].content == null)
					{
						count1--;
						if(count1 < 1) break;
					}
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[1]][count2].content == null)
					{
						count2--;
						if(count2 < 1) break;
					}
					while(GameEngine.Instance.Positions[GameEngine.Instance.Positions[curX][yInd].content.XAxisKoords[2]][count3].content == null)
					{
						count3--;
						if(count3 < 1) break;
					}
					MoveContent(curX, yInd, curX, Mathf.Max(Mathf.Max(count2, count1), count3)+1);
					break;
				}
			}
		}
	}
	
	
	public static void DestroyContent(Position target)
	{
		DestroyImmediate (target.content);
		GameEngine.Instance.CheckHoles ();
	}

	public static void ManagePosition()
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
	
	public static void MoveContent(int curX, int yOrigin, int newX, int yTarget)
	{
		int xOrigin = GameEngine.Instance.Positions[curX][yOrigin].content.XAxisKoords[0];
		int xTarget = xOrigin;
		BackgroundObject curContent = GameEngine.Instance.Positions [xOrigin] [yOrigin].content;
		switch(curContent.BackgroundtileForm)
		{
		case GameSettings.Form.simple:
			//current.content.XAxisKoords = new int[]{xTarget};
			curContent.YDestination = yTarget;
			GameEngine.Instance.Positions[xTarget][yTarget].content = curContent;
			GameEngine.Instance.Positions[xOrigin][yOrigin].content = null;
			break;
		case GameSettings.Form.horizontal:
			//current.content.XAxisKoords = new int[]{xTarget, xTarget+1};
			curContent.YDestination = yTarget;
			GameEngine.Instance.Positions[xTarget][yTarget].content = curContent;
			GameEngine.Instance.Positions[xTarget+1][yTarget].content = curContent;
			GameEngine.Instance.Positions[xOrigin][yOrigin].content = null;
			GameEngine.Instance.Positions[xOrigin+1][yOrigin].content = null;
			break;
		case GameSettings.Form.vertical:
			//current.content.XAxisKoords = new int[]{xTarget};
			curContent.YDestination = yTarget;
			GameEngine.Instance.Positions[xTarget][yTarget].content = curContent;
			GameEngine.Instance.Positions[xTarget][yTarget+1].content = curContent;
			if(curContent != GameEngine.Instance.Positions[xOrigin][yOrigin].content)
				GameEngine.Instance.Positions[xOrigin][yOrigin].content = null;
			if(yOrigin < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
				GameEngine.Instance.Positions[xOrigin][yOrigin+1].content = null;
			break;
		case GameSettings.Form.quad:
			//current.content.XAxisKoords = new int[]{xTarget, xTarget+1};
			curContent.YDestination = yTarget;
			GameEngine.Instance.Positions[xTarget][yTarget].content = curContent;
			GameEngine.Instance.Positions[xTarget][yTarget+1].content = curContent;
			if(curContent != GameEngine.Instance.Positions[xOrigin][yOrigin].content)
				GameEngine.Instance.Positions[xOrigin][yOrigin].content = null;
			GameEngine.Instance.Positions[xTarget+1][yTarget].content = curContent;
			GameEngine.Instance.Positions[xTarget+1][yTarget+1].content = curContent;
			if(curContent != GameEngine.Instance.Positions[xOrigin][yOrigin].content)
				GameEngine.Instance.Positions[xOrigin+1][yOrigin].content = null;
			if(yOrigin < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
			{
				GameEngine.Instance.Positions[xOrigin+1][yOrigin+1].content = null;
				GameEngine.Instance.Positions[xOrigin][yOrigin+1].content = null;
			}
			break;
		case GameSettings.Form.giant:
			curContent.YDestination = yTarget;
			GameEngine.Instance.Positions[xTarget][yTarget].content = curContent;
			GameEngine.Instance.Positions[xTarget][yTarget+1].content = curContent;
			if(yTarget+2 < GameSettings.Y_AXIS_POSITIONS_COUNT)
			{
				GameEngine.Instance.Positions[xTarget][yTarget+2].content = curContent;
				GameEngine.Instance.Positions[xTarget+1][yTarget+2].content = curContent;
			}
			for(int gIndex = 0; gIndex < 3; gIndex++)
			{
				if(yOrigin < GameSettings.Y_AXIS_POSITIONS_COUNT-gIndex && curContent != GameEngine.Instance.Positions[xOrigin][yOrigin+gIndex].content)
				{
					GameEngine.Instance.Positions[xOrigin][yOrigin+gIndex].content = null;
					GameEngine.Instance.Positions[xOrigin+1][yOrigin+gIndex].content = null;
				}
			}
			break;
		case GameSettings.Form.horizontalLong:
			curContent.YDestination = yTarget;
			for(int hLindex = 0; hLindex < 3; hLindex++)
			{
				GameEngine.Instance.Positions[xTarget+hLindex][yTarget].content = curContent;
				GameEngine.Instance.Positions[xOrigin+hLindex][yOrigin].content = null;
			}
			break;
		case GameSettings.Form.verticalLong:
			curContent.YDestination = yTarget;
			GameEngine.Instance.Positions[xTarget][yTarget].content = curContent;
			GameEngine.Instance.Positions[xTarget][yTarget+1].content = curContent;
			if(yTarget+2 < GameSettings.Y_AXIS_POSITIONS_COUNT)
				GameEngine.Instance.Positions[xTarget][yTarget+2].content = curContent;
			if(curContent != GameEngine.Instance.Positions[xOrigin][yOrigin].content)
				GameEngine.Instance.Positions[xOrigin][yOrigin].content = null;
			if(yOrigin < GameSettings.Y_AXIS_POSITIONS_COUNT-1 && curContent != GameEngine.Instance.Positions[xOrigin][yOrigin+1].content)
				GameEngine.Instance.Positions[xOrigin][yOrigin+1].content = null;
			if(yOrigin < GameSettings.Y_AXIS_POSITIONS_COUNT-2 && curContent != GameEngine.Instance.Positions[xOrigin][yOrigin+2].content)
				GameEngine.Instance.Positions[xOrigin][yOrigin+2].content = null;
			break;
		case GameSettings.Form.immense:
			curContent.YDestination = yTarget;
			GameEngine.Instance.Positions[xTarget][yTarget].content = curContent;
			GameEngine.Instance.Positions[xTarget][yTarget+1].content = curContent;
			if(yTarget+2 < GameSettings.Y_AXIS_POSITIONS_COUNT)
			{
				for(int iIndex = 0; iIndex < 3; iIndex++)
					GameEngine.Instance.Positions[xTarget+iIndex][yTarget+2].content = curContent;
			}
			for(int iIndex2 = 0; iIndex2 < 3; iIndex2++)
			{
				if(yOrigin < GameSettings.Y_AXIS_POSITIONS_COUNT-iIndex2 && curContent != GameEngine.Instance.Positions[xOrigin][yOrigin+iIndex2].content)
				{
					for(int iIndex3 = 0; iIndex3 < 3; iIndex3++)
						GameEngine.Instance.Positions[xOrigin+iIndex3][yOrigin+iIndex2].content = null;
				}
			}
			break;
		}
	}
	
	public static int FittingForm(int xPos)
	{
		if (GameEngine.Instance.Positions [xPos] [GameSettings.Y_AXIS_POSITIONS_COUNT - 1].content != null)
			return -1;
		GameSettings.Form tileForm = GameSettings.Form.giant;
		bool fitting = false;
		while(!fitting)
		{
			tileForm = (GameSettings.Form)Random.Range(0, GameSettings.FORM_ARRAY_LENGTH);
			switch(tileForm)
			{
			case GameSettings.Form.simple:
				fitting = true;
				break;
			case GameSettings.Form.horizontal:
				fitting = 
					(xPos < GameSettings.X_AXIS_POSITIONS_COUNT-1 
					 && GameEngine.Instance.Positions[xPos][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+1][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null);
				break;
			case GameSettings.Form.vertical:
				fitting = true;
				break;
			case GameSettings.Form.quad:
				fitting = 
					(xPos < GameSettings.X_AXIS_POSITIONS_COUNT-1 
					 && GameEngine.Instance.Positions[xPos][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+1][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null);
				break;
			case GameSettings.Form.giant:
				fitting = 
					(xPos < GameSettings.X_AXIS_POSITIONS_COUNT-1 
					 && GameEngine.Instance.Positions[xPos][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+1][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null);
				break;
			case GameSettings.Form.horizontalLong:
				fitting = 
					(xPos < GameSettings.X_AXIS_POSITIONS_COUNT-2 
					 && GameEngine.Instance.Positions[xPos][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+1][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+2][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null);
				break;
			case GameSettings.Form.verticalLong:
				fitting = 
					(xPos < GameSettings.X_AXIS_POSITIONS_COUNT-2 
					 && GameEngine.Instance.Positions[xPos][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+1][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+2][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null);
				break;
			case GameSettings.Form.immense:
				fitting = 
					(xPos < GameSettings.X_AXIS_POSITIONS_COUNT-2 
					 && GameEngine.Instance.Positions[xPos][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+1][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null
					 && GameEngine.Instance.Positions[xPos+2][GameSettings.Y_AXIS_POSITIONS_COUNT-1].content == null);
				break;
			}
		}
		return (int)tileForm;
	}
	
	public static void HandleCreationOfNewTile(int currentXKoord)
	{
		for(int i = 0; i < GameSettings.Y_AXIS_POSITIONS_COUNT; i++)
		{
			int fitting = FittingForm(currentXKoord);
			if(fitting < 0) return;
			GameSettings.Form newTileForm = (GameSettings.Form)fitting;
			int[] xKoords;
			int yDest;
			BackgroundObject newObj;
			int count1 = GameSettings.Y_AXIS_POSITIONS_COUNT-1;
			int count2 = GameSettings.Y_AXIS_POSITIONS_COUNT-1;
			int count3 = GameSettings.Y_AXIS_POSITIONS_COUNT-1;
			switch(newTileForm)
			{
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.simple:
				xKoords = new int[]{currentXKoord};
				newObj = SpawnObject(newTileForm, xKoords, i);
				GameEngine.Instance.Positions[currentXKoord][i].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.horizontal:
				while(GameEngine.Instance.Positions[currentXKoord][count1].content == null)
				{
					count1--;
					if(count1 < 1) break;
				}
				while(GameEngine.Instance.Positions[currentXKoord+1][count2].content == null)
				{
					count2--;
					if(count2 < 1) break;
				}
				xKoords = new int[]{currentXKoord, currentXKoord+1};
				yDest = Mathf.Max(count1, count2)+1;
				newObj = SpawnObject(newTileForm, xKoords, yDest);
				GameEngine.Instance.Positions[currentXKoord][yDest].content = newObj;
				GameEngine.Instance.Positions[currentXKoord+1][yDest].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.vertical:
				xKoords = new int[]{currentXKoord};
				newObj = SpawnObject(newTileForm, xKoords, i);
				if(i < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord][i+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord][i].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.quad:
				while(GameEngine.Instance.Positions[currentXKoord][count1].content == null)
				{
					count1--;
					if(count1 < 1) break;
				}
				while(GameEngine.Instance.Positions[currentXKoord+1][count2].content == null)
				{
					count2--;
					if(count2 < 1) break;
				}
				xKoords = new int[]{currentXKoord, currentXKoord+1};
				yDest = Mathf.Max(count1, count2)+1;
				newObj = SpawnObject(newTileForm, xKoords, yDest);
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord][yDest+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord][yDest].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord+1][yDest+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord+1][yDest].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.giant:
				while(GameEngine.Instance.Positions[currentXKoord][count1].content == null)
				{
					count1--;
					if(count1 < 1) break;
				}
				while(GameEngine.Instance.Positions[currentXKoord+1][count2].content == null)
				{
					count2--;
					if(count2 < 1) break;
				}
				xKoords = new int[]{currentXKoord, currentXKoord+1};
				yDest = Mathf.Max(count1, count2)+1;
				newObj = SpawnObject(newTileForm, xKoords, yDest);
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-2)
					GameEngine.Instance.Positions[currentXKoord][yDest+2].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord][yDest+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord][yDest].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-2)
					GameEngine.Instance.Positions[currentXKoord+1][yDest+2].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord+1][yDest+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord+1][yDest].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.horizontalLong:
				while(GameEngine.Instance.Positions[currentXKoord][count1].content == null)
				{
					count1--;
					if(count1 < 1) break;
				}
				while(GameEngine.Instance.Positions[currentXKoord+1][count2].content == null)
				{
					count2--;
					if(count2 < 1) break;
				}
				while(GameEngine.Instance.Positions[currentXKoord+2][count3].content == null)
				{
					count3--;
					if(count3 < 1) break;
				}
				xKoords = new int[]{currentXKoord, currentXKoord+1, currentXKoord+2};
				yDest = Mathf.Max(Mathf.Max(count1, count2), count3)+1;
				newObj = SpawnObject(newTileForm, xKoords, yDest);
				GameEngine.Instance.Positions[currentXKoord][yDest].content = newObj;
				GameEngine.Instance.Positions[currentXKoord+1][yDest].content = newObj;
				GameEngine.Instance.Positions[currentXKoord+2][yDest].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.verticalLong:
				xKoords = new int[]{currentXKoord};
				newObj = SpawnObject(newTileForm, xKoords, i);
				if(i < GameSettings.Y_AXIS_POSITIONS_COUNT-2)
					GameEngine.Instance.Positions[currentXKoord][i+2].content = newObj;
				if(i < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord][i+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord][i].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			case GameSettings.Form.immense:
				while(GameEngine.Instance.Positions[currentXKoord][count1].content == null)
				{
					count1--;
					if(count1 < 1) break;
				}
				while(GameEngine.Instance.Positions[currentXKoord+1][count2].content == null)
				{
					count2--;
					if(count2 < 1) break;
				}
				while(GameEngine.Instance.Positions[currentXKoord+2][count3].content == null)
				{
					count3--;
					if(count3 < 1) break;
				}
				xKoords = new int[]{currentXKoord, currentXKoord+1, currentXKoord+2};
				yDest = Mathf.Max(Mathf.Max(count1, count2), count3)+1;
				newObj = SpawnObject(newTileForm, xKoords, yDest);
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-2)
					GameEngine.Instance.Positions[currentXKoord][yDest+2].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord][yDest+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord][yDest].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-2)
					GameEngine.Instance.Positions[currentXKoord+1][yDest+2].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord+1][yDest+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord+1][yDest].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-2)
					GameEngine.Instance.Positions[currentXKoord+2][yDest+2].content = newObj;
				if(yDest < GameSettings.Y_AXIS_POSITIONS_COUNT-1)
					GameEngine.Instance.Positions[currentXKoord+2][yDest+1].content = newObj;
				GameEngine.Instance.Positions[currentXKoord+2][yDest].content = newObj;
				break;
				/////////////////////////
				/////////////////////////
				/////////////////////////
			}
		}
	}
}
