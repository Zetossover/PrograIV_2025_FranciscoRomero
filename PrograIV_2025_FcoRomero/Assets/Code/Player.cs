using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("PlayerInfo")]
    [SerializeField] TankMovement movemove;
    [SerializeField] HullMovement moveBullet;
    public List<StatInfo> currentStats = new List<StatInfo>();

    public Color place_Light;

    public int currentDmg;
    public int score;

    [Header("TankPieceSO")]
    public TankPieceScriptable place_Track;
    public TankPieceScriptable place_Hull;
    public TankPieceScriptable place_Tower;
    public TankPieceScriptable place_Gun;
    public TankPieceScriptable place_GunConnector;
    public TankPieceScriptable place_Projectile;

    [Header("Texts")]
    [SerializeField] TMP_InputField inputField;
    public TextMeshProUGUI tankTextName;
    public string playerName;

    public TankSpriteModifier modifier;
    

    private void Awake()
    {
        inputField.onValueChanged.AddListener(ChangeName);
    }
    private void Start()
    {
        UpdateControllersWithTankPieces();
        LoadData();
    }
    public GameObject GetCurrentProjectilePrefab()
    {
        return place_Projectile.projectilePrefab;
    }
    public void Load()
    {
        LoadData();
    }
    public void SaveInfo()
    {
        SaveData();
    }
    public void SaveAndReload()
    {
        SaveData();    // Guarda primero
        LoadData();    // Luego carga inmediatamente
    }
    void ChangeName(string val)
    {
        tankTextName.text = val;
        playerName = val;
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
        UpdateControllersWithTankPieces();
        UpdateControllersWithStats();
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
        currentStats = statsInfo;
        UpdateControllersWithStats();
    }

    void LoadData()
    {
        LoadSaveSystem loadSave = new LoadSaveSystem();
        PlayerDataInfo playerData = loadSave.LoadPlayerInfo(onEndLoadData);
    }
    public void onEndLoadData(PlayerDataInfo playerData)
    {
        if (playerData == null) return;

        ChangeName(playerData.playerName);
        currentDmg = playerData.currentDmg;
        score = playerData.score;

        LoadResources loadResources = new LoadResources();

        place_Track = loadResources.GetTankPieceScriptable(TankPieceType.Track, playerData.piecesName[0]);
        place_Hull = loadResources.GetTankPieceScriptable(TankPieceType.Hull, playerData.piecesName[1]);
        place_Tower = loadResources.GetTankPieceScriptable(TankPieceType.Tower, playerData.piecesName[2]);
        place_Gun = loadResources.GetTankPieceScriptable(TankPieceType.Gun, playerData.piecesName[3]);
        place_GunConnector = loadResources.GetTankPieceScriptable(TankPieceType.GunConnector, playerData.piecesName[4]);
        place_Projectile = loadResources.GetTankPieceScriptable(TankPieceType.Projectile, playerData.piecesName[5]);

        modifier.ChangeSprite(place_Track.pieceType, place_Track.pieceSprite);
        modifier.ChangeSprite(place_Hull.pieceType, place_Hull.pieceSprite);
        modifier.ChangeSprite(place_Tower.pieceType, place_Tower.pieceSprite);
        modifier.ChangeSprite(place_Gun.pieceType, place_Gun.pieceSprite);
        modifier.ChangeSprite(place_GunConnector.pieceType, place_GunConnector.pieceSprite);
        modifier.ChangeSprite(place_Projectile.pieceType, place_Projectile.pieceSprite);

        UpdateControllersWithTankPieces();
        UpdateControllersWithStats();
    }
    void SaveData()
    {
        PlayerDataInfo playerData = new PlayerDataInfo();

        playerData.playerName = playerName;
        playerData.currentDmg = currentDmg;
        playerData.score = score;

        playerData.piecesName = new List<string>();
        playerData.piecesName.Add(place_Track.id);
        playerData.piecesName.Add(place_Hull.id);
        playerData.piecesName.Add(place_Tower.id);
        playerData.piecesName.Add(place_Gun.id);
        playerData.piecesName.Add(place_GunConnector.id);
        playerData.piecesName.Add(place_Projectile.id);

        Debug.Log("GUARDANDO TANQUE Enviando evento");
        AnalyticsManager.Instance.TankIDEvent(this);

        LoadSaveSystem loadSave = new LoadSaveSystem();
        loadSave.SavePlayerInfo(playerData);
    }
    public void UpdateControllersWithStats()
    {
        if (currentStats == null || currentStats.Count == 0) return;

        foreach (StatInfo stat in currentStats)
        {
            switch (stat.type)
            {
                case StatType.Spd:
                    if (movemove != null)
                        movemove.speed = stat.value;
                    break;

                case StatType.RootSpd:
                    if (movemove != null)
                        movemove.speedRotate = stat.value;
                    break;

                case StatType.Attack:
                    if (moveBullet != null)
                        moveBullet.powerBullet = stat.value;
                    break;

                case StatType.Defense:
                    // Aquí puedes guardar en una variable de defensa en Player o PlayerStats
                    break;

                case StatType.Life:
                    // Aquí puedes guardar en una variable de vida en Player o PlayerStats
                    break;

                case StatType.BulletSpd:
                    if (moveBullet != null)
                        moveBullet.tiempoEntreDisparos = 1f / stat.value;
                    break;
            }
        }
    }
}
