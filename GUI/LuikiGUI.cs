using UnityEngine;

namespace LuikiBetter.GUI
{
    public static class LuikiGUI
    {
        // =========================
        // CYAN THEME
        // =========================

        public static readonly Color Cyan =
            new Color(0f, 0.85f, 1f);

        public static readonly Color Dark =
            new Color(0.025f, 0.035f, 0.055f);

        public static readonly Color Button =
            new Color(0.06f, 0.10f, 0.15f);

        public static readonly Color ButtonHover =
            new Color(0.0f, 0.55f, 0.75f);

        // =========================
        // LARGE MENU SIZE
        // =========================

        public static Rect MenuRect =
            new Rect(
                100,
                70,
                850,
                800
            );

        // =========================
        // STYLES
        // =========================

        public static GUIStyle ButtonStyle()
        {
            GUIStyle style =
                new GUIStyle(GUI.skin.button);

            style.fontSize = 20;
            style.fontStyle =
                FontStyle.Bold;

            style.alignment =
                TextAnchor.MiddleCenter;

            style.padding =
                new RectOffset(
                    15,
                    15,
                    12,
                    12
                );

            return style;
        }

        public static GUIStyle PageStyle()
        {
            GUIStyle style =
                new GUIStyle(GUI.skin.label);

            style.fontSize = 26;
            style.fontStyle =
                FontStyle.Bold;

            style.alignment =
                TextAnchor.MiddleCenter;

            return style;
        }

        public static GUIStyle SmallStyle()
        {
            GUIStyle style =
                new GUIStyle(GUI.skin.label);

            style.fontSize = 16;
            style.alignment =
                TextAnchor.MiddleCenter;

            return style;
        }

        // =========================
        // BUTTON
        // =========================

        public static bool Button(
            string text,
            float height = 55f)
        {
            Color oldColor =
                GUI.backgroundColor;

            GUI.backgroundColor =
                Button;

            bool clicked =
                GUILayout.Button(
                    text,
                    ButtonStyle(),
                    GUILayout.Height(height)
                );

            GUI.backgroundColor =
                oldColor;

            return clicked;
        }

        // =========================
        // SETTINGS PANEL
        // =========================

        public static void DrawSettings(
            System.Action closeSettings)
        {
            GUILayout.Space(20);

            GUILayout.Label(
                "SETTINGS",
                PageStyle(),
                GUILayout.Height(45)
            );

            GUILayout.Space(20);

            if (Button(
                "BACK TO MENU",
                55))
            {
                closeSettings?.Invoke();
            }
        }

        // =========================
        // PAGE INDICATOR
        // =========================

        public static void DrawPageIndicator(
            int currentPage,
            int totalPages)
        {
            GUILayout.Space(8);

            GUILayout.Label(
                "PAGE " +
                (currentPage + 1) +
                " / " +
                totalPages,
                SmallStyle()
            );
        }
    }
}
