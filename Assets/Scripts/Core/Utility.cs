using UnityEngine;

public class Utility
{
    public static bool ContainsLayer(LayerMask? mask, int layer)
    {        
        return mask.HasValue && (((1 << layer) & mask) != 0);
    }

    public static bool IsGroundLayer(int layer)
    {
        return ContainsLayer(LayerMask.GetMask("Ground"), layer);
    }
}
