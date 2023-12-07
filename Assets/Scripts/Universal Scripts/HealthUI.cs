using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Image healthUI;

    void Awake()
    {
        healthUI = GameObject.FindWithTag(Tags.HEALTH_UI_TAG).GetComponent<Image>();
    }

    public void DisplayHealth(float val)
    {
        val /= 100f;
        Mathf.Max(val, 0f);
        healthUI.fillAmount = val;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
