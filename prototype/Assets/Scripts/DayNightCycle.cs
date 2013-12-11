using UnityEngine;
using System.Collections;


public class DayNightCycle : MonoBehaviour
{
    public const float SUMMER_INCLINATION = 90;
    public const float WINTER_INCLINATION = 25;

    [Range(WINTER_INCLINATION, SUMMER_INCLINATION)]
    public float summerness = 50;
    public float dayDurationSeconds = 120;
    public float nightDurationSeconds = 60;

    private float dayTimer;
    private bool isDay;
    public bool IsDay
    {
        get { return isDay; }
    }

    private Light sun;

    // Use this for initialization
    void Start()
    {
        dayTimer = dayDurationSeconds / 2;  //starts at noon
        isDay = true;
        sun = GetComponentInChildren<Light>();
        sun.transform.localRotation = Quaternion.identity;
        transform.rotation = Quaternion.Euler(summerness,0,0);
        setAllFlashlights(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDay)
        {
            dayTimer += Time.deltaTime;
            sun.transform.localRotation = Quaternion.Euler(0, 180 * dayProgress() - 90, 0);

            if (dayTimer > dayDurationSeconds)
            {
                StartCoroutine(night());
            }
        }
    }

    public float dayProgress()
    {
        return (dayTimer / dayDurationSeconds); 
    }

    private IEnumerator night()
    {
        setAllFlashlights(true);
        sun.enabled = false;
        isDay = false;
        yield return new WaitForSeconds(nightDurationSeconds);
        setAllFlashlights(false);
        sun.enabled = true;
        isDay = true;
        dayTimer = 0;
    }

    private void setAllFlashlights(bool on)
    {
        FlashLight[] list = FindObjectsOfType<FlashLight>();
        foreach (FlashLight script in list)
        {
            script.toggleOnOff(on);
        }
    }
}
