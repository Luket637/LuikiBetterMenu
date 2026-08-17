using UnityEngine;
using UnityEngine.XR;
using LuikiBetter.Mods;

namespace LuikiBetter
{
    public class LuikiBetterMenu : MonoBehaviour
    {
        private bool menuOpen;
        private bool settingsOpen;

        private MenuPage[] pages;
        private int currentPage;

        private Rect windowRect =
            new Rect(100, 100, 620, 680);

        private bool previousY;
        private bool previousA;
        private bool previousB;

        private GUIStyle titleStyle;
        private GUIStyle versionStyle;
        private GUIStyle pageStyle;
        private GUIStyle buttonStyle;
        private GUIStyle tabStyle;
        private GUIStyle statusStyle;

        private Texture2D cyanTexture;
        private Texture2D darkTexture;
        private Texture2D panelTexture;

        private float animationTime;

        private void Start()
        {
            CreatePages();
            CreateTextures();
            CreateStyles();
        }

        private void CreatePages()
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
                    "Reverse Card"
                ),

                new MenuPage(
                    "Safety",
                    "Anti-Kick",
                    "Anti-Ban",
                    "Accept ToS"
                )
            };
        }

        private void CreateTextures()
        {
            cyanTexture = MakeTexture(
                new Color(0f, 0.85f, 1f)
            );

            darkTexture = MakeTexture(
                new Color(0.025f, 0.035f, 0.055f)
            );

            panelTexture = MakeTexture(
                new Color(0.055f, 0.075f, 0.105f)
            );
        }

        private Texture2D MakeTexture(Color color)
        {
            Texture2D texture =
                new Texture2D(1, 1);

            texture.SetPixel(0, 0, color);
            texture.Apply();

            return texture;
        }

        private void CreateStyles()
        {
            titleStyle =
                new GUIStyle(GUI.skin.label);

            titleStyle.fontSize = 34;
            titleStyle.alignment =
                TextAnchor.MiddleCenter;
            titleStyle.fontStyle =
                FontStyle.Bold;

            versionStyle =
                new GUIStyle(GUI.skin.label);

            versionStyle.fontSize = 14;
            versionStyle.alignment =
                TextAnchor.MiddleCenter;

            pageStyle =
                new GUIStyle(GUI.skin.label);

            pageStyle.fontSize = 24;
            pageStyle.alignment =
                TextAnchor.MiddleCenter;
            pageStyle.fontStyle =
                FontStyle.Bold;

            buttonStyle =
                new GUIStyle(GUI.skin.button);

            buttonStyle.fontSize = 18;
            buttonStyle.alignment =
                TextAnchor.MiddleCenter;
            buttonStyle.fontStyle =
                FontStyle.Bold;

            tabStyle =
                new GUIStyle(GUI.skin.button);

            tabStyle.fontSize = 15;
            tabStyle.alignment =
                TextAnchor.MiddleCenter;
            tabStyle.fontStyle =
                FontStyle.Bold;

            statusStyle =
                new GUIStyle(GUI.skin.label);

            statusStyle.fontSize = 13;
            statusStyle.alignment =
                TextAnchor.MiddleCenter;
        }

        private void Update()
        {
            animationTime +=
                Time.deltaTime;

            InputDevice left =
                InputDevices.GetDeviceAtXRNode(
                    XRNode.LeftHand
                );

            InputDevice right =
                InputDevices.GetDeviceAtXRNode(
                    XRNode.RightHand
                );

            // Y = open menu
            if (left.TryGetFeatureValue(
                CommonUsages.primaryButton,
                out bool yPressed))
            {
                if (yPressed && !previousY)
                    menuOpen = !menuOpen;

                previousY = yPressed;
            }

            // A = Ghost Monkey
            if (right.TryGetFeatureValue(
                CommonUsages.primaryButton,
                out bool aPressed))
            {
                if (aPressed && !previousA)
                    Movement.ToggleGhostMonkey();

                previousA = aPressed;
            }

            // B = Invisible Monkey
            if (right.TryGetFeatureValue(
                CommonUsages.secondaryButton,
                out bool bPressed))
            {
                if (bPressed && !previousB)
                    Movement.ToggleInvisibleMonkey();

                previousB = bPressed;
            }

            Movement.Update();
            Safety.Update();
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

        private void DrawMenu(int id)
        {
            GUI.DrawTexture(
                new Rect(
                    0,
                    0,
                    windowRect.width,
                    windowRect.height
                ),
                darkTexture
            );

            GUILayout.BeginVertical();

            GUILayout.Space(12);

            float pulse =
                0.75f +
                Mathf.Sin(
                    animationTime * 3f
                ) * 0.25f;

            Color oldColor = GUI.color;

            GUI.color =
                new Color(
                    0.3f,
                    pulse,
                    1f
                );

            GUILayout.Label(
                "LUIKI BETTER",
                titleStyle,
                GUILayout.Height(45)
            );

            GUI.color = oldColor;

            GUILayout.Label(
                "V1.0",
                versionStyle,
                GUILayout.Height(22)
            );

            GUILayout.Space(12);

            DrawTabs();

            GUILayout.Space(15);

            if (settingsOpen)
                DrawSettings();
            else
                DrawPage();

            GUILayout.FlexibleSpace();

            DrawFooter();

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private void DrawTabs()
        {
            GUILayout.BeginHorizontal();

            for (int i = 0;
                 i < pages.Length;
                 i++)
            {
                GUI.backgroundColor =
                    i == currentPage
                        ? Color.cyan
                        : new Color(
                            0.12f,
                            0.15f,
                            0.20f
                        );

                if (GUILayout.Button(
                    pages[i].Name,
                    tabStyle,
                    GUILayout.Height(38)))
                {
                    currentPage = i;
                    settingsOpen = false;
                }
            }

            GUI.backgroundColor =
                Color.white;

            GUILayout.EndHorizontal();
        }

        private void DrawPage()
        {
            MenuPage page =
                pages[currentPage];

            GUILayout.Label(
                page.Name.ToUpper(),
                pageStyle,
                GUILayout.Height(40)
            );

            GUILayout.Space(10);

            foreach (string mod in page.Mods)
            {
                DrawModButton(mod);
                GUILayout.Space(7);
            }
        }

        private void DrawModButton(
            string mod)
        {
            string text = mod;

            if (mod == "Platforms")
            {
                text =
                    "Platforms  " +
                    (Movement.PlatformsEnabled
                        ? "ON"
                        : "OFF");
            }

            if (mod == "Ghost Monkey")
            {
                text =
                    "Ghost Monkey  " +
                    (Movement.GhostMonkeyEnabled
                        ? "ON"
                        : "OFF");
            }

            if (mod == "Invisible Monkey")
            {
                text =
                    "Invisible Monkey  " +
                    (Movement.InvisibleMonkeyEnabled
                        ? "ON"
                        : "OFF");
            }

            if (mod == "Anti-Kick")
            {
                text =
                    "Anti-Kick  " +
                    (Safety.AntiKickEnabled
                        ? "ON"
                        : "OFF");
            }

            if (mod == "Anti-Ban")
            {
                text =
                    "Anti-Ban  " +
                    (Safety.AntiBanEnabled
                        ? "ON"
                        : "OFF");
            }

            GUI.backgroundColor =
                new Color(
                    0.08f,
                    0.12f,
                    0.17f
                );

            if (GUILayout.Button(
                text,
                buttonStyle,
                GUILayout.Height(48)))
            {
                HandleMod(mod);
            }

            GUI.backgroundColor =
                Color.white;
        }

        private void DrawSettings()
        {
            GUILayout.Space(20);

            GUILayout.Label(
                "SETTINGS",
                pageStyle,
                GUILayout.Height(40)
            );

            GUILayout.Space(20);

            GUILayout.Label(
                "Luiki Better",
                buttonStyle,
                GUILayout.Height(45)
            );

            GUILayout.Label(
                "Version: V1.0",
                versionStyle
            );

            GUILayout.Space(15);

            if (GUILayout.Button(
                "BACK TO MENU",
                buttonStyle,
                GUILayout.Height(45)))
            {
                settingsOpen = false;
            }
        }

        private void DrawFooter()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button(
                "SETTINGS",
                buttonStyle,
                GUILayout.Height(40)))
            {
                settingsOpen =
                    !settingsOpen;
            }

            if (GUILayout.Button(
                "EXIT",
                buttonStyle,
                GUILayout.Height(40)))
            {
                menuOpen = false;
                settingsOpen = false;
            }

            GUILayout.EndHorizontal();

            GUILayout.Label(
                "PAGE " +
                (currentPage + 1) +
                " / " +
                pages.Length,
                statusStyle
            );
        }

        private void HandleMod(
            string mod)
        {
            switch (mod)
            {
                case "Platforms":
                    Movement.TogglePlatforms();
                    break;

                case "Ghost Monkey":
                    Movement.ToggleGhostMonkey();
                    break;

                case "Invisible Monkey":
                    Movement.ToggleInvisibleMonkey();
                    break;

                case "Long Arms":
                    Movement.ActivateLongArms();
                    break;

                case "Anti-Kick":
                    Safety.ToggleAntiKick();
                    break;

                case "Anti-Ban":
                    Safety.ToggleAntiBan();
                    break;

                case "Accept ToS":
                    Safety.AcceptToS();
                    break;

                case "Kick Gun":
                    Overpowered.KickGun();
                    break;

                case "Kick All":
                    Overpowered.KickAll();
                    break;

                case "Crash Gun":
                    Overpowered.CrashGun();
                    break;

                case "Crash All":
                    Overpowered.CrashAll();
                    break;

                case "Reverse Card":
                    Overpowered.ReverseCard();
                    break;
            }
        }
    }
}
    }
}
