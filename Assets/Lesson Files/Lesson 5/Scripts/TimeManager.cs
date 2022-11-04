using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Slowmotion Properties")]
    [SerializeField]
    private float slowdownFactor = 0.3f;
    [SerializeField]
    private float slowdownLength = 3.0f;

    public bool bCanSlowdown = false;
    public bool bCanScaleUp = false;


    private void Update()
    {
        ScaleUpTime();
        DoSlowmotion();
    }

    public void DoSlowmotion()
    {
        if (bCanSlowdown)
        {
            //Disable scale up....
            bCanScaleUp = false;
            //Reduce the timeScale to slowdownFactor...
            Time.timeScale = slowdownFactor;
            //FixedDeltaTime and DeltaTime is affected by the change in TimeScale and since our major concern is on physics simulation, we tweak FixedDeltaTime with respect to TimeScale...
            //Set the fixedDeltaTime proportional to the present (timeScale by 0.002f)...
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            bCanSlowdown = false;
        }
    }

    private void ScaleUpTime()
    {
        if (!bCanScaleUp) return;
        Time.timeScale += (1.0f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);
        //If the timeScale is evaluated to be 1.0f then set bCanScaleUp to false because we don't want this operation to be carried out once it is...
        if (Time.timeScale >= 1.0f)
        {
            bCanScaleUp = false;
        }
    }
}
