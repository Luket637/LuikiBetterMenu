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

    // Movement
    private bool platforms = false;
    private bool ghostMonkey = false;
    private bool invisibleMonkey = false;

    // Safety
    private bool antiKick = false;

    // Button states
    private bool previousYPressed = false;
    private bool previousAPressed = false;
    private bool previousBPressed = false;

    private void Start()
    {
        pages = new MenuPage[]
        {
            new MenuPage(
                "Movement",
                "Platforms",
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

        titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 28;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;
        titleStyle.fontStyle = FontStyle.Bold;

        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 18;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.fontStyle = FontStyle.Bold;

        pageStyle = new GUIStyle(GUI.skin.label);
        pageStyle.fontSize = 22;
        pageStyle.alignment = TextAnchor.MiddleCenter;
        pageStyle.normal.textColor = Color.cyan;
        pageStyle.fontStyle = FontStyle.Bold;
    }

    private void Update()
    {
        // Y = Open / Close Menu
        InputDevice leftController =
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftController.TryGetFeatureValue(
            CommonUsages.primaryButton,
            out bool yPressed))
        {
            if (yPressed && !previousYPressed)
            {
                menuOpen = !menuOpen;
            }

            previousYPressed = yPressed;
        }

        // Right controller
        InputDevice rightController =
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // A = Ghost Monkey
        if (rightController.TryGetFeatureValue(
            CommonUsages.primaryButton,
            out bool aPressed))
        {
            if (aPressed && !previousAPressed)
            {
                ghostMonkey = !ghostMonkey;

                Debug.Log(
                    "Luiki Better: Ghost Monkey " +
                    (ghostMonkey ? "enabled." : "disabled.")
                );
            }

            previousAPressed = aPressed;
        }

        // B = Invisible Monkey
        if (rightController.TryGetFeatureValue(
            CommonUsages.secondaryButton,
            out bool bPressed))
        {
            if (bPressed && !previousBPressed)
            {
                invisibleMonkey = !invisibleMonkey;

                Debug.Log(
                    "Luiki Better: Invisible Monkey " +
                    (invisibleMonkey ? "enabled." : "disabled.")
                );
            }

            previousBPressed = bPressed;
        }

        // Movement hooks
        if (platforms)
        {
            ApplyPlatforms();
        }

        if (ghostMonkey)
        {
            ApplyGhostMonkey();
        }

        if (invisibleMonkey)
        {
            ApplyInvisibleMonkey();
        }
    }

    private void ApplyPlatforms()
    {
        // Platform implementation will be added here.
    }

    private void ApplyGhostMonkey()
    {
        // Ghost Monkey implementation will be added here.
    }

    private void ApplyInvisibleMonkey()
    {
        // Invisible Monkey implementation will be added here.
    }

    // Long Arms is an instant action.
    private void ActivateLongArms()
    {
        Debug.Log("Luiki Better: Long Arms activated!");
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

            if (mod == "Ghost Monkey")
            {
                buttonText = ghostMonkey
                    ? "Ghost Monkey: ON"
                    : "Ghost Monkey: OFF";
            }

            if (mod == "Invisible Monkey")
            {
                buttonText = invisibleMonkey
                    ? "Invisible Monkey: ON"
                    : "Invisible Monkey: OFF";
            }

            if (mod == "Anti-Kick")
            {
                buttonText = antiKick
                    ? "Anti-Kick: ON"
                    : "Anti-Kick: OFF";
            }

            // Long Arms intentionally has no ON/OFF state.

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

        if (mod == "Ghost Monkey")
        {
            ghostMonkey = !ghostMonkey;

            Debug.Log(
                "Luiki Better: Ghost Monkey " +
                (ghostMonkey ? "enabled." : "disabled.")
            );

            return;
        }

        if (mod == "Invisible Monkey")
        {
            invisibleMonkey = !invisibleMonkey;

            Debug.Log(
                "Luiki Better: Invisible Monkey " +
                (invisibleMonkey ? "enabled." : "disabled.")
            );

            return;
        }

        if (mod == "Long Arms")
        {
            ActivateLongArms();
            return;
        }

        if (mod == "Anti-Kick")
        {
            antiKick = !antiKick;

            Debug.Log(
                "Luiki Better: Anti-Kick " +
                (antiKick ? "enabled." : "disabled.")
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
