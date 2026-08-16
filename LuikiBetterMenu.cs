using UnityEngine;
using UnityEngine.XR;

public class LuikiBetterMenu : MonoBehaviour
{
    private bool menuOpen = false;

    private MenuPage[] pages;
    private int currentPage = 0;

    private Rect windowRect = new Rect(100, 100, 500, 600);

    private GUIStyle titleStyle;
    private GUIStyle buttonStyle;
    private GUIStyle pageStyle;

    // Movement toggles
    private bool platforms = false;
    private bool longArms = false;

    private void Start()
    {
        pages = new MenuPage[]
        {
            new MenuPage(
                "Movement",
                "Platforms",
                "Invisible",
                "Ghost Monkey",
                "Invisible Monkey",
                "Long Arms"
            ),

            new MenuPage(
                "Overpowered",
                "Kick Gun",
                "Kick All",
                "Crash Gun",
                "Crash All",
                "Ban Gun",
                "Reverse Card"
            ),

            new MenuPage(
                "Safety",
                "Anti-Kick",
                "Anti-Ban",
                "Accept ToS"
            )
        };

        // Title
        titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 28;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;
        titleStyle.fontStyle = FontStyle.Bold;

        // Buttons
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 18;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.fontStyle = FontStyle.Bold;

        // Page title
        pageStyle = new GUIStyle(GUI.skin.label);
        pageStyle.fontSize = 22;
        pageStyle.alignment = TextAnchor.MiddleCenter;
        pageStyle.normal.textColor = Color.cyan;
        pageStyle.fontStyle = FontStyle.Bold;
    }

    private void Update()
    {
        InputDevice leftController =
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftController.TryGetFeatureValue(
            CommonUsages.primaryButton,
            out bool yPressed))
        {
            if (yPressed)
            {
                menuOpen = !menuOpen;
            }
        }

        if (longArms)
        {
            ApplyLongArms();
        }

        if (platforms)
        {
            ApplyPlatforms();
        }
    }

    private void ApplyLongArms()
    {
        // Reserved for the actual Gorilla Tag player implementation.
    }

    private void ApplyPlatforms()
    {
        // Reserved for the actual Gorilla Tag VR platform implementation.
    }

    private void OnGUI()
    {
        if (!menuOpen)
            return;

        windowRect = GUI.Window(
            999,
            windowRect,
            DrawMenu,
            ""
        );
    }

    private void DrawMenu(int windowID)
    {
        // Cyan theme
        GUI.backgroundColor = Color.cyan;

        GUILayout.BeginVertical();

        GUILayout.Space(15);

        GUILayout.Label(
            "LUIKI BETTER",
            titleStyle
        );

        GUILayout.Label(
            "V1.0",
            pageStyle
        );

        GUILayout.Space(15);

        // Page navigation
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", buttonStyle, GUILayout.Width(60)))
        {
            currentPage--;

            if (currentPage < 0)
                currentPage = pages.Length - 1;
        }

        GUILayout.Label(
            pages[currentPage].Name.ToUpper(),
            pageStyle
        );

        if (GUILayout.Button(">", buttonStyle, GUILayout.Width(60)))
        {
            currentPage++;

            if (currentPage >= pages.Length)
                currentPage = 0;
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        DrawCurrentPage();

        GUILayout.Space(20);

        if (GUILayout.Button("EXIT", buttonStyle))
        {
            menuOpen = false;
        }

        GUILayout.Space(10);

        GUILayout.EndVertical();

        GUI.DragWindow();
    }

    private void DrawCurrentPage()
    {
        MenuPage page = pages[currentPage];

        foreach (string mod in page.Mods)
        {
            string buttonText = mod;

            if (mod == "Platforms")
            {
                buttonText = platforms
                    ? "Platforms: ON"
                    : "Platforms: OFF";
            }

            if (mod == "Long Arms")
            {
                buttonText = longArms
                    ? "Long Arms: ON"
                    : "Long Arms: OFF";
            }

            if (GUILayout.Button(buttonText, buttonStyle))
            {
                HandleMod(mod);
            }

            GUILayout.Space(5);
        }
    }

    private void HandleMod(string mod)
    {
        if (mod == "Platforms")
        {
            platforms = !platforms;

            Debug.Log(
                "Luiki Better: Platforms " +
                (platforms ? "enabled." : "disabled.")
            );

            return;
        }

        if (mod == "Long Arms")
        {
            longArms = !longArms;

            Debug.Log(
                "Luiki Better: Long Arms " +
                (longArms ? "enabled." : "disabled.")
            );

            return;
        }

        Debug.Log(
            "Luiki Better: " +
            mod +
            " selected."
        );
    }
}
