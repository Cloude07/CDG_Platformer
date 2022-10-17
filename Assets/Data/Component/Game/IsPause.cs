using UnityEngine;

public class IsPause : MonoBehaviour
{
    bool isPauses;

    public void Pause()
    {
        isPauses = !isPauses;
        if(isPauses)
        {
            Time.timeScale = 0f;
        }
        else
            Time.timeScale = 1;
    }
}
