using UnityEngine;
using UnityEngine.XR;
using LuikiBetter.Mods;
using LuikiBetter.GUI;

namespace LuikiBetter
{
    public class LuikiBetterMenu : MonoBehaviour
    {
        private bool menuOpen;
        private bool settingsOpen;

        private MenuPage[] pages;
        private int currentPage;

        private bool previousY;
        private bool previousA;
        private bool previousB;

        private Texture2D backgroundTexture;
        private Texture2D panelTexture;

        private void Start()
        {
            CreatePages();
            CreateTextures();
        }

        // =========================
        // PAGES
        // =========================

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

        // =========================
        // TEXTURES
        // =========================

        private void CreateTextures()
        {
            backgroundTexture =
                MakeTexture(
                    LuikiGUI.Dark
                );

            panelTexture =
                MakeTexture(
                    new Color(
                        0.04f,
                        0.065f,
                        0.10f
                    )
                );
        }

        private Texture2D MakeTexture(
            Color color)
        {
            Texture2D texture =
                new Texture2D(1, 1);

            texture.SetPixel(
                0,
                0,
                color
            );

            texture.Apply();

            return texture;
        }

        // =========================
        // UPDATE
        // =========================

        private void Update()
        {
            InputDevice left =
                InputDevices.GetDeviceAtXRNode(
                    XRNode.LeftHand
                );

            InputDevice right =
                InputDevices.GetDeviceAtXRNode(
                    XRNode.RightHand
                );

            // Y = open/close menu

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

        // =========================
        // GUI
        // =========================

        private void OnGUI()
        {
            if (!menuOpen)
                return;

            LuikiGUI.MenuRect =
                GUI.Window(
                    999,
                    LuikiGUI.MenuRect,
                    DrawMenu,
                    ""
                );
        }

        // =========================
        // MAIN MENU
        // =========================

        private void DrawMenu(
            int windowID)
        {
            GUI.DrawTexture(
                new Rect(
                    0,
                    0,
                    LuikiGUI.MenuRect.width,
                    LuikiGUI.MenuRect.height
                ),
                backgroundTexture
            );

            GUILayout.BeginVertical();

            GUILayout.Space(20);

            // Header

            GUI.color =
                LuikiGUI.Cyan;

            GUILayout.Label(
                "LUIKI BETTER",
                LuikiGUI.PageStyle(),
                GUILayout.Height(55)
            );

            GUI.color =
                Color.white;

            GUILayout.Label(
                "V1.0",
                LuikiGUI.SmallStyle(),
                GUILayout.Height(25)
            );

            GUILayout.Space(20);

            // Tabs

            DrawTabs();

            GUILayout.Space(20);

            // Content

            if (settingsOpen)
                DrawSettings();
            else
                DrawCurrentPage();

            GUILayout.FlexibleSpace();

            // Footer

            DrawFooter();

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        // =========================
        // TABS
        // =========================

        private void DrawTabs()
        {
            GUILayout.BeginHorizontal();

            for (int i = 0;
                 i < pages.Length;
                 i++)
            {
                Color old =
                    GUI.backgroundColor;

                GUI.backgroundColor =
                    i == currentPage
                        ? LuikiGUI.Cyan
                        : LuikiGUI.Button;

                if (GUILayout.Button(
                    pages[i].Name,
                    LuikiGUI.ButtonStyle(),
                    GUILayout.Height(55)))
                {
                    currentPage = i;
                    settingsOpen = false;
                }

                GUI.backgroundColor =
                    old;
            }

            GUILayout.EndHorizontal();
        }

        // =========================
        // CURRENT PAGE
        // =========================

        private void DrawCurrentPage()
        {
            MenuPage page =
                pages[currentPage];

            GUILayout.Space(10);

            GUILayout.Label(
                page.Name.ToUpper(),
                LuikiGUI.PageStyle(),
                GUILayout.Height(45)
            );

            GUILayout.Space(15);

            foreach (string mod in page.Mods)
            {
                DrawMod(mod);

                GUILayout.Space(8);
            }
        }

        // =========================
        // MOD BUTTON
        // =========================

        private void DrawMod(
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

            if (LuikiGUI.Button(
                text,
                58))
            {
                HandleMod(mod);
            }
        }

        // =========================
        // SETTINGS
        // =========================

        private void DrawSettings()
        {
            LuikiGUI.DrawSettings(
                () =>
                {
                    settingsOpen = false;
                }
            );
        }

        // =========================
        // FOOTER
        // =========================

        private void DrawFooter()
        {
            GUILayout.BeginHorizontal();

            if (LuikiGUI.Button(
                "SETTINGS",
                50))
            {
                settingsOpen =
                    !settingsOpen;
            }

            if (LuikiGUI.Button(
                "EXIT",
                50))
            {
                menuOpen = false;
                settingsOpen = false;
            }

            GUILayout.EndHorizontal();

            LuikiGUI.DrawPageIndicator(
                currentPage,
                pages.Length
            );
        }

        // =========================
        // MOD HANDLER
        // =========================

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
}
