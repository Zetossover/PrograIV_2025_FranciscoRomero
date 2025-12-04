using Unity.Services.Analytics;
//Cambiamos el nombre de mi clase al mismo que de mi evento
public class MyFirstCustomEvent: Event
{
    //al constructor le ponemos el mismo tiipo que de la clase
    public MyFirstCustomEvent() : base("myFirstCustomEvent")//base("Siempre va el nombre del evento igual que el dashboard)
    {
    }
    //aca bajo vamos a poner los mismas variables que en el dashboard
    public float MFCE_LindoFloat { set { SetParameter("mFCE_LindoFloat", value); } }
}
public class EnemyKilled : Event
{
    public EnemyKilled() : base("enemyKilled")//base("Siempre va el nombre del evento igual que el dashboard)
    {
    }
    //aca bajo vamos a poner los mismas variables que en el dashboard
    public string EnemyKilledId { set { SetParameter("enemyKilledId", value); } }

}
public class LogEvent : Event
{
    public LogEvent() : base("logEvent")//base("Siempre va el nombre del evento igual que el dashboard)
    {
    }
    //aca bajo vamos a poner los mismas variables que en el dashboard
    public int Log_Event { set { SetParameter("Log_Event", value); } }

}
public class TimeEvent : Event
{
    public TimeEvent() : base("timeEvent")//base("Siempre va el nombre del evento igual que el dashboard)
    {
    }
    //aca bajo vamos a poner los mismas variables que en el dashboard
    public float Time_Event { set { SetParameter("time_Event", value); } }

}
public class CollectorEvent : Event
{
    public CollectorEvent() : base("collectorEvent") //base("Siempre va el nombre del evento igual que el dashboard)
    {
    }
    //aca bajo vamos a poner los mismas variables que en el dashboard
    public int Collector_Event { set { SetParameter("collector_EventInt", value); } }

}
public class ScoreEvent : Event
{
    public ScoreEvent() : base("scoreEvent") //base("Siempre va el nombre del evento igual que el dashboard)
    {
    }
    //aca bajo vamos a poner los mismas variables que en el dashboard
    public int Score_Event { set { SetParameter("score_Event", value); } }

}
public class TankIdEvent : Event
{
    public TankIdEvent() : base("tankIdEvent") { }

    public string Track_Id { set { SetParameter("trackId", value); } }
    public string Hull_Id { set { SetParameter("hullId", value); } }
    public string Tower_Id { set { SetParameter("towerId", value); } }
    public string Gun_Id { set { SetParameter("gunId", value); } }
    public string Connector_Id { set { SetParameter("connectorId", value); } }
    public string Projectile_Id { set { SetParameter("projectileId", value); } }
}
public class MySecondCustomEvent : Event
{
    public MySecondCustomEvent() : base("mySecondCustomEvent")//base("Siempre va el nombre del evento igual que el dashboard)
    {
    }
    //aca bajo vamos a poner los mismas variables que en el dashboard
    public bool MSCE_LindoBool { set { SetParameter("mFCE_LindoBool", value); } }
    public string MSCE_LindoString { set { SetParameter("mFCE_LindoString", value); } }
    public int MSCE_LindoInt { set { SetParameter("mFCE_LindoInt", value); } }

}