using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Get angle on plane xoz, where (0, 0, 1) is 0, and (1, 0, 0) is 90, (-1, 0, 0) is 270.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static float GetHorizontalAngle(this Vector3 direction)
    {
        //Mathf.Atan2 return float Angle in radians between the (x,y) vector and the (1,0) unit vector, in the range [-Pi, Pi].
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360f;
        }
        return angle;
    }
}
