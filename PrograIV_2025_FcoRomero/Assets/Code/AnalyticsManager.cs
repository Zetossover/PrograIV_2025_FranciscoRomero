using System;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Analytics;
using Unity.Services.Core;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    [HideInInspector]public bool isInitialized = false;

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else Destroy(gameObject);
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private async void Start()
    {
       await UnityServices.InitializeAsync();
       AnalyticsService.Instance.StartDataCollection();
       isInitialized = true;
    }

    //void Update()
    //{
    //    if (isFlushing) return;
    //    timer += Time.deltaTime;
    //    if (timer >= autoFlushInterval)
    //    {
    //        Flush();
    //    }
    //}

    public void SaveMyFirstCustomEvent(float MFCE_LindoFloat)
    {
        if (isInitialized)
        {
            Debug.Log("Pne1");
            MyFirstCustomEvent myFirstCustomEvent = new MyFirstCustomEvent()
            {
                MFCE_LindoFloat = MFCE_LindoFloat,
            };
            AnalyticsService.Instance.RecordEvent(myFirstCustomEvent);
        }
    }
    public void SaveMySecondCustomEvent(int MSCE_LindoInt, bool MSCE_LindoBool, string MSCE_LindoString)
    {
        if (isInitialized)
        {
            Debug.Log("Pne2");
            MySecondCustomEvent mySecondCustomEvent = new MySecondCustomEvent()
            {
                MSCE_LindoInt = MSCE_LindoInt,
                MSCE_LindoBool = MSCE_LindoBool,
                MSCE_LindoString = MSCE_LindoString,
            };
            AnalyticsService.Instance.RecordEvent(mySecondCustomEvent);
        }
    }
    public void EnemyKilled(string EnemyKilledId)
    {
        if (isInitialized)
        {
            EnemyKilled enemyKilledEvent = new EnemyKilled()
            {
                EnemyKilledId = EnemyKilledId,
            };
            AnalyticsService.Instance.RecordEvent(enemyKilledEvent);
        }
    }
    public void LogsEvent(int LogSucess)
    {
        if (isInitialized)
        {
            LogEvent logsucessEvent = new LogEvent()
            {
                Log_Event = LogSucess,
            };
            AnalyticsService.Instance.RecordEvent(logsucessEvent);
        }
    }


    // -----------------------------
    //   ATAJOS PARA EVENTOS COMUNES
    // -----------------------------

    public void LevelStart(int level)
    {
       
    }

    public void LevelComplete(int level, float time)
    {
        
    }

    public void PlayerDied(string reason, float time, Vector3 pos)
    {

    }

    public void ItemCollected(string itemId)
    {
       
    }

    public void SessionStart()
    {
       
    }

    public void SessionEnd()
    {
        
    }

    //Eventos Fco

   
    public void EnemyDamage(int damage)
    {
        
    }
    public void Points(int score) 
    {
        
    }
    public void Collections(int gasoline)
    {
       
    }
    public void TimeDuration(int time)
    {
       
    }
    public void PlayerDeath(string enemyId)
    {
       
    }
    public void PlayerPieces(string pieceId)
    {
      
    }
}
