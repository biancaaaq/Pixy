using UnityEngine;

[CreateAssetMenu(menuName = "Achievement", fileName = "achievement")]
public class AchievementEntity : ScriptableObject
{
    public string code;
    public string title;
    public string description;
    public Sprite icon;
}