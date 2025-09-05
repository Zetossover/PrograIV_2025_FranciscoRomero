using UnityEngine;

public class LoadResources
{
    public TankPieceScriptable GetTankPieceScriptable(TankPieceType type, string id)
    {
        string path = type.ToString() + "/" + id;
        Debug.Log(path);

        TankPieceScriptable piece = Resources.Load<TankPieceScriptable>(path);
        return piece;
    }
}
