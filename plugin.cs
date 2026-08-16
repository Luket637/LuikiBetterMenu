using BepInEx;
using UnityEngine;
using UnityEngine.XR;

[BepInPlugin("com.luiki.better", "Luiki Better", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private GameObject menuObject;

    private void Awake()
    {
        menuObject = new GameObject("Luiki Better");
        menuObject.AddComponent<LuikiBetterMenu>();

        DontDestroyOnLoad(menuObject);

        Logger.LogInfo("Luiki Better V1.0 loaded!");
    }
}
