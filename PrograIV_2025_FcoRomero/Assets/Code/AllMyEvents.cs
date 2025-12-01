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