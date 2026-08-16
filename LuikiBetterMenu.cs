using UnityEngine;
using UnityEngine.XR;

public class LuikiBetterMenu : MonoBehaviour
{
    private bool menuOpen = false;
    private int page = 0;

    private Rect windowRect = new Rect(100, 100, 500, 600);

    private GUIStyle titleStyle;
    private GUIStyle buttonStyle;

    private void Start()
    {
        titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 28;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;

        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 18;
    }

    private void Update()
    {
        InputDevice rightController =
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightController.TryGetFeatureValue(
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

        GUILayout.Space(15);

        if (GUILayout.Button("Movement", buttonStyle))
            page = 0;

        if (GUILayout.Button("Overpowered", buttonStyle))
            page = 1;

        if (GUILayout.Button("Safety", buttonStyle))
            page = 2;

        GUILayout.Space(20);

        DrawPage();

        GUILayout.Space(20);

        if (GUILayout.Button("EXIT", buttonStyle))
            menuOpen = false;

        GUILayout.EndVertical();

        GUI.DragWindow();
    }

    private void DrawPage()
    {
        if (page == 0)
        {
            GUILayout.Label("MOVEMENT", titleStyle);

            GUILayout.Button("Platforms", buttonStyle);
            GUILayout.Button("Invisible", buttonStyle);
            GUILayout.Button("Ghost Monkey", buttonStyle);
            GUILayout.Button("Invisible Monkey", buttonStyle);
            GUILayout.Button("Long Arms", buttonStyle);
        }

        else if (page == 1)
        {
            GUILayout.Label("OVERPOWERED", titleStyle);

            GUILayout.Button("Kick Gun", buttonStyle);
            GUILayout.Button("Kick All", buttonStyle);
            GUILayout.Button("Crash Gun", buttonStyle);
            GUILayout.Button("Crash All", buttonStyle);
            GUILayout.Button("Ban Gun", buttonStyle);
            GUILayout.Button("Reverse Card", buttonStyle);
        }

        else if (page == 2)
        {
            GUILayout.Label("SAFETY", titleStyle);

            GUILayout.Button("Anti-Kick", buttonStyle);
            GUILayout.Button("Anti-Ban", buttonStyle);
            GUILayout.Button("Accept ToS", buttonStyle);
        }
    }
