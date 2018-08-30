using UnityEngine;

public static class IsometricHelper
{
    public const float XDELTA = .5f;
    public const float YDELTA = .25f;

    /**
     * Maps an xy coordinate to the position a tile shoudl be placed in
     * unity. Method also takes into accoutn elevation of the tile
     * 
     */
    public static Vector2 gridToGamePostion(int x, int y)
    {
        float posX = -(y * XDELTA) + (x * XDELTA);
        float posY = (y * YDELTA) + (x * YDELTA);
        //posY += pos.elevation * YDELTA;
        return new Vector2(posX, posY);
    }

    public static Vector2 gridToGamePostionTile(int x, int y)
    {
        float posX = -(y * XDELTA) + (x * XDELTA);
        float posY = (y * YDELTA) + (x * YDELTA);
        //posY += pos.elevation * YDELTA;
        return new Vector2(posX, posY);
    }

    public static Vector2 gridToGamePostionUnit(int x, int y)
    {
        float posX = -(y * XDELTA) + (x * XDELTA);
        float posY = (y * YDELTA) + (x * YDELTA);
        //posY += pos.elevation * YDELTA;
        return new Vector2(posX, posY) + new Vector2(0,2*YDELTA);
    }

    public static Vector3 getMovementVector(Facing facing)
    {
        switch(facing)
        {
            case Facing.Down:
                return new Vector2(XDELTA, -YDELTA);
                
            case Facing.Up:
                return new Vector2(-XDELTA, YDELTA);
                
            case Facing.Left:
                return new Vector2(-XDELTA, -YDELTA);
                
            case Facing.Right:
                return new Vector2(XDELTA, YDELTA);
                
        }
        return new Vector2(0, 0);

    }

    /**
     * Assigns the sorting order tiles shoudl have so the sprites 
     * are displayed in the correct order
     * 
     */
    public static int getTileSortingOrder(int x, int y)
    {
        int sortingOrder = -x + -y;
        return sortingOrder;
    }

    /**
     * Assigns the sorting order tiles shoudl have so the sprites 
     * are displayed in the correct order
     * 
     */
    /*public static int distanceBetweenGridPositions(GridPosition one, GridPosition two)
    {
        int distanceX = Mathf.Abs(one.x - two.x);
        int distanceY = Mathf.Abs(one.y - two.y);
        return distanceX+distanceY;
    }*/

}

