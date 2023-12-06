using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour
{
    public Image skillImage1;
    public Image skillImage2;

    public void DisplaySkills(Sprite skillSprite1, Sprite skillSprite2)
    {
        skillImage1.sprite = skillSprite1;
        skillImage2.sprite = skillSprite2;
    }
}
