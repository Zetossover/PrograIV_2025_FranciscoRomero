using System.Collections.Generic;
using UnityEngine;

public class PlayerPieceModifierMain : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] PanelPieceSelection panelPieceSelection;
    [SerializeField] PanelPartTypeSelection panelPieceTypeSelection;
    [SerializeField] TankSpriteModifier tankSpriteModifier;
    
    [Header("Pieces Info")]
    [SerializeField] List<TankPieceScriptable> tpiece_GunConnector;
    [SerializeField] List<TankPieceScriptable> tpiece_Guns;
    [SerializeField] List<TankPieceScriptable> tpiece_Hulls;
    [SerializeField] List<TankPieceScriptable> tpiece_Towers;
    [SerializeField] List<TankPieceScriptable> tpiece_Tracks;
    [SerializeField] List<TankPieceScriptable> tpiece_Projectiles;

    private void Start()
    {
        panelPieceTypeSelection.OnButtonSelectPartType(TankPieceType.Hull);
    }

    public void OnPieceTypeSelected(TankPieceType pieceType)
    {
        panelPieceSelection.SetPanelSelection(GetPiecesByType(pieceType));
    }

    public void OnPieceSelected(TankPieceType pieceType, string id)
    {
        ModifyPiece(GetScriptableTankPiece(pieceType, id));
    }

    void ModifyPiece(TankPieceScriptable tankPiece)
    {
        panelPieceTypeSelection.SetButtonSelectPartType(tankPiece.pieceType, tankPiece.pieceSprite);
        tankSpriteModifier.ChangeSprite(tankPiece.pieceType, tankPiece.pieceSprite);
    }

    private List<TankPieceScriptable> GetPiecesByType(TankPieceType pieceType)
    {
        List<TankPieceScriptable> selectedPieces = new List<TankPieceScriptable>();
        switch (pieceType)
        {
            case TankPieceType.Light:
                break;
            case TankPieceType.Track:
                selectedPieces = tpiece_Tracks;
                break;
            case TankPieceType.Hull:
                selectedPieces = tpiece_Hulls;
                break;
            case TankPieceType.Tower:
                selectedPieces = tpiece_Towers;
                break;
            case TankPieceType.Gun:
                selectedPieces = tpiece_Guns;
                break;
            case TankPieceType.GunConnector:
                selectedPieces = tpiece_GunConnector;
                break;
            case TankPieceType.Projectile:
                selectedPieces = tpiece_Projectiles;
                break;
        }
        return selectedPieces;
    }

    private TankPieceScriptable GetScriptableTankPiece(TankPieceType pieceType, string pieceID)
    {
        return GetPiecesByType(pieceType).Find(x => x.id == pieceID);
    }
}
