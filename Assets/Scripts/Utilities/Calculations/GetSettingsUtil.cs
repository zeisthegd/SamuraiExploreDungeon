using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSettingsUtil : MonoBehaviour
{
    public static void GetDungThemeAndGenSettings(ref DungeonTheme theme, ref GenerationSettings settings)
    {
        GetDungeonGenSettings(ref settings);
        GetDungeonTheme(ref theme);
    }

    public static void GetDungeonGenSettings(ref GenerationSettings settings)
    {
        settings = FindObjectOfType<DungeonGenerator>().GetComponent<DungeonGenerator>().Settings;

    }
    public static void GetDungeonTheme(ref DungeonTheme theme)
    {
        theme = FindObjectOfType<DungeonGenerator>().GetComponent<DungeonGenerator>().Theme;

    }
}
