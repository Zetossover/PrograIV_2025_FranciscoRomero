using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Movement movemove;

    public List<StatInfo> currentStats = new List<StatInfo>();
    public Color place_Light;
    public TankPieceScriptable place_Track;
    public TankPieceScriptable place_Hull;
    public TankPieceScriptable place_Tower;
    public TankPieceScriptable place_Gun;
    public TankPieceScriptable place_GunConnector;
    public TankPieceScriptable place_Projectile;

    private void Start()
    {
        UpdateControllersWithTankPieces();
    }
    public void OnTankPieceChange(TankPieceScriptable tankPiece)
    {
        //Debug.Log("Tank Fue cambiado: " + newPiece.pieceType);
        //Debug.Log("Tank Fue cogido: " + newPiece.id);
        switch (tankPiece.pieceType)
        {
            case TankPieceType.Light:
                Debug.Log("Nick Chupala");
                break;
            case TankPieceType.Track:
                place_Track = tankPiece;
                break;
            case TankPieceType.Hull:
                place_Hull = tankPiece;
                break;
            case TankPieceType.Tower:
                place_Tower = tankPiece;
                break;
            case TankPieceType.Gun:
                place_Gun = tankPiece;
                break;
            case TankPieceType.GunConnector:
                place_GunConnector = tankPiece;
                break;
            case TankPieceType.Projectile:
                place_Projectile = tankPiece;
                break;
            default:
                break;
        }
    }

    public void UpdateControllersWithTankPieces()
    {
        List<StatInfo> statsInfo = new List<StatInfo>();

        foreach (var item in place_Track.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in place_Hull.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in place_Tower.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in place_Gun.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in place_GunConnector.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in place_Projectile.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
    }


}
