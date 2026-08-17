using UnityEngine;
using UnityEngine.XR;
using LuikiBetter.Mods;
using LuikiBetter.GUI;

namespace LuikiBetter
{
    public class LuikiBetterMenu : MonoBehaviour
    {
        private bool menuOpen;
        private int currentPage;

        private MenuPage[] pages;

        private bool previousY;
        private bool previousA;
        private bool previousB;

        private Texture2D backgroundTexture;

        private void Start()
        {
            CreatePages();
            CreateTextures();
        }

        // =========================
        // V1.0 PAGES
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
                    "Safety",
                    "Anti-Kick",
                    "Anti-Ban",
                    "Accept ToS"
                ),

                new MenuPage(
                    "Overpowered",
                    "Kick Gun",
                    "Kick All",
                    "Crash Gun",
                    "Crash All",
                    "Reverse Card"
                )
            };
        }

        // =========================
        // BACKGROUND
        // =========================

        private void CreateTextures()
        {
            backgroundTexture =
                MakeTexture(
                    LuikiGUI.Dark
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
        // CONTROLLER
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

            // Y = OPEN / CLOSE MENU

            if (left.TryGetFeatureValue(
                CommonUsages.primaryButton,
                out bool yPressed))
            {
                if (yPressed && !previousY)
                    menuOpen = !menuOpen;

                previousY = yPressed;
            }

            // B = GHOST MONKEY ONLY

            if (right.TryGetFeatureValue(
                CommonUsages.secondaryButton,
                out bool bPressed))
            {
                if (bPressed && !previousB)
                    Movement.ToggleGhostMonkey();

                previousB = bPressed;
            }

            // A = INVISIBLE MONKEY ONLY

            if (right.TryGetFeatureValue(
                CommonUsages.primaryButton,
                out bool aPressed))
            {
                if (aPressed && !previousA)
                    Movement.ToggleInvisibleMonkey();

                previousA = aPressed;
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

            DrawPageButtons();

            GUILayout.Space(20);

            DrawCurrentPage();

            GUILayout.FlexibleSpace();

            DrawFooter();

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        // =========================
        // PAGE BUTTONS
        // =========================

        private void DrawPageButtons()
        {
            GUILayout.BeginHorizontal();

            for (int i = 0;
                 i < pages.Length;
                 i++)
            {
                Color oldColor =
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
                }

                GUI.backgroundColor =
                    oldColor;
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

            GUILayout.Label(
                page.Name.ToUpper(),
                LuikiGUI.PageStyle(),
                GUILayout.Height(45)
            );

            GUILayout.Space(15);

            foreach (string mod in page.Mods)
            {
                DrawModButton(mod);

                GUILayout.Space(8);
            }
        }

        // =========================
        // MOD BUTTONS
        // =========================

        private void DrawModButton(
            string mod)
        {
            string text =
                GetModText(mod);

            if (LuikiGUI.Button(
                text,
                58))
            {
                HandleMod(mod);
            }
        }

        private string GetModText(
            string mod)
        {
            switch (mod)
            {
                case "Platforms":
                    return "Platforms  " +
                        (Movement.PlatformsEnabled
                            ? "ON"
                            : "OFF");

                case "Ghost Monkey":
                    return "Ghost Monkey  " +
                        (Movement.GhostMonkeyEnabled
                            ? "ON"
                            : "OFF");

                case "Invisible Monkey":
                    return "Invisible Monkey  " +
                        (Movement.InvisibleMonkeyEnabled
                            ? "ON"
                            : "OFF");

                case "Anti-Kick":
                    return "Anti-Kick  " +
                        (Safety.AntiKickEnabled
                            ? "ON"
                            : "OFF");

                case "Anti-Ban":
                    return "Anti-Ban  " +
                        (Safety.AntiBanEnabled
                            ? "ON"
                            : "OFF");

                default:
                    return mod;
            }
        }

        // =========================
        // MOD ACTIONS
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

        // =========================
        // FOOTER
        // =========================

        private void DrawFooter()
        {
            if (LuikiGUI.Button(
                "EXIT",
                50))
            {
                menuOpen = false;
            }

            LuikiGUI.DrawPageIndicator(
                currentPage,
                pages.Length
            );
        }
    }
}
}
    }
}
