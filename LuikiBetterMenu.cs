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

    private void Start()
    {
        // Create pages
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

        // Title style
        titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 28;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;

        // Button style
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 18;
        buttonStyle.normal.textColor = Color.white;
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
        // Cyan menu
        GUI.backgroundColor = Color.cyan;

        GUILayout.BeginVertical();

        GUILayout.Space(10);

        GUILayout.Label(
            "LUIKI BETTER",
            titleStyle
        );

        GUILayout.Label(
            "V1.0",
            titleStyle
        );

        GUILayout.Space(10);

        // Page navigation
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", buttonStyle))
        {
            currentPage--;

            if (currentPage < 0)
                currentPage = pages.Length - 1;
        }

        GUILayout.Label(
            pages[currentPage].Name,
            titleStyle
        );

        if (GUILayout.Button(">", buttonStyle))
        {
            currentPage++;

            if (currentPage >= pages.Length)
                currentPage = 0;
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        // Current page
        DrawCurrentPage();

        GUILayout.Space(15);

        // Exit
        if (GUILayout.Button("EXIT", buttonStyle))
        {
            menuOpen = false;
        }

        GUILayout.EndVertical();

        GUI.DragWindow();
    }

    private void DrawCurrentPage()
    {
        MenuPage page = pages[currentPage];

        foreach (string mod in page.Mods)
        {
            if (GUILayout.Button(mod, buttonStyle))
            {
                HandleMod(mod);
            }
        }
    }

    private void HandleMod(string mod)
    {
        Debug.Log("Luiki Better: " + mod + " selected.");
    }
}
