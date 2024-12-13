using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    // Factor by which to slow down the game (e.g., 0.5 for half speed)
    public float slowMotionFactor = 0.5f;

    // Duration of slow motion in seconds
    public float slowMotionDuration = 2f;

    private bool isSlowMotionActive = false;

    private void Start()
    {
        // Reset time scale to normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        isSlowMotionActive = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !isSlowMotionActive)
        {
            StartCoroutine(ActivateSlowMotion());
        }

    }

    public void SlowMo()
    {
        StartCoroutine(ActivateSlowMotion());
    }

    System.Collections.IEnumerator ActivateSlowMotion()
    {
        isSlowMotionActive = true;

        Time.timeScale = slowMotionFactor;

        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(slowMotionDuration);
        
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        isSlowMotionActive = false;
    }


}
