using UnityEngine;

public static class GridHelper
{

    /**
     * Maps an xy coordinate to the position a tile shoudl be placed in
     * unity. Method also takes into accoutn elevation of the tile
     * 
     */
    public static Vector2 gridToGamePostion(int _x, int _y)
    {
        return new Vector2(_x, _y);
    }

    /**
     * Assigns the sorting order tiles shoudl have so the sprites 
     * are displayed in the correct order
     * 
     */
    public static int distanceBetweenGridPositions(int _x1, int _y1, int _x2, int _y2)
    {
        int _distanceX = Mathf.Abs(_x1 - _x2);
        int _distanceY = Mathf.Abs(_y1 - _y2);
        return _distanceX + _distanceY;
    }

}

