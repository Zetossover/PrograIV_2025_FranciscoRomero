using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.EventsModels;
using UnityEngine.SocialPlatforms.Impl;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    // Tamaño máximo antes de enviar telemetría
    [SerializeField] private int maxBufferSize = 10;

    // Tiempo entre autosend
    [SerializeField] private float autoFlushInterval = 10f;

    private List<EventContents> buffer = new List<EventContents>();
    private float timer = 0f;
    bool isFlushing = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isFlushing) return;
        timer += Time.deltaTime;
        if (timer >= autoFlushInterval)
        {
            Flush();
        }
    }

    // -----------------------------
    //   API PÚBLICA PARA ENVIAR EVENTOS
    // -----------------------------

    public void LogEvent(string eventName, Dictionary<string, object> data = null, string eventNamespace = "custom")
    {
        if (data == null) data = new Dictionary<string, object>();

        var evt = new EventContents
        {
            Name = eventName,
            EventNamespace = "com.playfab.events.custom",
            Payload = data,
            OriginalTimestamp = DateTime.UtcNow
        };

        buffer.Add(evt);

        if (buffer.Count >= maxBufferSize)
            Flush();
    }

    // -----------------------------
    //   FLUSH (ENVÍA LOS EVENTOS A PLAYFAB)
    // -----------------------------
    public void Flush()
    {
        if (buffer.Count == 0) return;

        isFlushing = true;
        var request = new WriteEventsRequest
        {
            Events = new List<EventContents>(buffer)
        };

        PlayFabEventsAPI.WriteEvents(request,
            result =>
            {
                isFlushing = false;
                buffer.Clear();
                timer = 0f;
                Debug.Log($"Telemetry enviado ({result.AssignedEventIds.Count} eventos)");
            },
            error =>
            {
                isFlushing = false;
                Debug.LogWarning("Error enviando Telemetry, se reintentará automáticamente. " + error.ErrorMessage);
                // No limpiamos el buffer, se reenvía en el próximo Flush
            }
        );
    }

    // -----------------------------
    //   ATAJOS PARA EVENTOS COMUNES
    // -----------------------------

    public void LevelStart(int level)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("level", level);
        LogEvent("level_start", data, "gameplay");
    }

    public void LevelComplete(int level, float time)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("level", level);
        data.Add("time", time);
        data.Add("score", 150);
        LogEvent("level_complete", data, "gameplay");
    }

    public void PlayerDied(string reason, float time, Vector3 pos)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("reason", reason);
        data.Add("timeAlive", time);
        //data.Add("diePos", pos);

        LogEvent("player_died", data, "gameplay");

        //que es lo mismo que esto
        //LogEvent("player_died", new Dictionary<string, object> {
        //    { "reason", reason }
        //}, "gameplay");
    }

    public void ItemCollected(string itemId)
    {
        LogEvent("item_collected", new Dictionary<string, object> {
            { "item_id", itemId }
        }, "economy");
    }

    public void SessionStart()
    {
        LogEvent("session_start", null, "system");
    }

    public void SessionEnd()
    {
        LogEvent("session_end", null, "system");
        Flush();
    }

    //Eventos Fco

    public void EnemyKilled(int killed)
    {
        LogEvent("enemy_Killed", new Dictionary<string, object> {
            { "Killed", killed }
        }, "gameplay");
    }
    public void EnemyDamage(int damage)
    {
        LogEvent("enemy_Damage", new Dictionary<string, object> {
            { "Damage", damage }
        }, "gameplay");
    }
    public void Points(int score) 
    {
        LogEvent("player_Score", new Dictionary<string, object> {
            { "playerScore", score }
        }, "leaderBoard");
    }
    public void Collections(int gasoline)
    {
        LogEvent("enemy_list", new Dictionary<string, object> {
            { "enemy_Id", gasoline }
        }, "gameplay");
    }
    public void TimeDuration(int time)
    {
        LogEvent("enemy_list", new Dictionary<string, object> {
            { "time", time }
        }, "Id_Identificator");
    }
    public void PlayerDeath(string enemyId)
    {
        LogEvent("enemy_list", new Dictionary<string, object> {
            { "enemy_Id", enemyId }
        }, "Id_Identificator");
    }
    public void PlayerPieces(string pieceId)
    {
        LogEvent("piece_list", new Dictionary<string, object> {
            { "piece_Id", pieceId }
        }, "Id_Identificator");
    }
}
