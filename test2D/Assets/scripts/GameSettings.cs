using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour 
{
	public const string BACKGROUND_SIMPLE_PREFAB_NAME = "BackgroundTileSimple";
	public const string BACKGROUND_HORIZONTAL_PREFAB_NAME = "BackgroundTileHorizontal";
	public const string BACKGROUND_VERTICAL_PREFAB_NAME = "BackgroundTileVertical";
	public const string BACKGROUND_QUAD_PREFAB_NAME = "BackgroundTileQuad";
	public const string BACKGROUND_GIANT_PREFAB_NAME = "BackgroundTileGiant";
	public const string BACKGROUND_HORIZONTALLONG_PREFAB_NAME = "BackgroundTileHorizontalLong";
	public const string BACKGROUND_VERTICALLONG_PREFAB_NAME = "BackgroundTileVerticalLong";
	public const string BACKGROUND_IMMENSE_PREFAB_NAME = "BackgroundTileImmense";
	public const float BACKGROUND_TILE_MOVEMENT_SPEED = 500;
	
	public const int X_AXIS_POSITIONS_COUNT = 10;
	public const int Y_AXIS_POSITIONS_COUNT = 10;
	
	public const int X_AXIS_OBJECTS_LENGTH = 100;
	public const int Y_AXIS_OBJECTS_LENGTH = 100;
	
	public const int SPAWN_HEIGHT_OFFSET = 200;

	//simple 1x1, horizonal 2x1, vertical 1x2, quad 2x2, giant 2x3, horizontalLong 3x1, verticalLong, 1x3, immense 3x3
	public enum Form{simple, horizontal, vertical, quad, giant, horizontalLong, verticalLong, immense}
	public const int FORM_ARRAY_LENGTH = 8;
}
