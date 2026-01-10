

using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    private HashSet<string> unlockedCodes = new HashSet<string>();

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Unlock(AchievementEntity achievement)
    {
        unlockedCodes.Add(achievement.code);
        Save();
    }

    private void Save()
    {
        string data = string.Join(",", unlockedCodes);
        PlayerPrefs.SetString("Achievements", data);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        string data = PlayerPrefs.GetString("Achievements");

        unlockedCodes = new HashSet<string>(data.Split(','));
    }

    public bool IsUnlocked(string code)
    {
        return unlockedCodes.Contains(code);
    }

    public AchievementEntity GetAchievementByCode(string code)
    {
        AchievementEntity[] allAchievements = Resources.LoadAll<AchievementEntity>("Achievements");

        foreach (var achievement in allAchievements)
        {
            if (achievement.code == code)
                return achievement;
        }
        return null;
    }

}