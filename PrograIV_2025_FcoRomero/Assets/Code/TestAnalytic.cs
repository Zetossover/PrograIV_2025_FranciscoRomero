using UnityEngine;

public class TestAnalytic : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AnalyticsManager.Instance.SaveMyFirstCustomEvent(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AnalyticsManager.Instance.SaveMySecondCustomEvent(1, true, "Pene");
        }
    }
}
