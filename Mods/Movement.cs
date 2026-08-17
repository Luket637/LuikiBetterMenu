using UnityEngine;

namespace LuikiBetter.Mods
{
    public static class Movement
    {
        public static bool PlatformsEnabled;
        public static bool GhostMonkeyEnabled;
        public static bool InvisibleMonkeyEnabled;

        // =========================
        // PLATFORMS
        // =========================

        public static void TogglePlatforms()
        {
            PlatformsEnabled = !PlatformsEnabled;

            Debug.Log(
                "Luiki Better: Platforms " +
                (PlatformsEnabled ? "ON" : "OFF")
            );
        }

        public static void UpdatePlatforms()
        {
            if (!PlatformsEnabled)
                return;

            // Platform implementation goes here.
        }

        // =========================
        // GHOST MONKEY
        // =========================

        public static void ToggleGhostMonkey()
        {
            GhostMonkeyEnabled = !GhostMonkeyEnabled;

            Debug.Log(
                "Luiki Better: Ghost Monkey " +
                (GhostMonkeyEnabled ? "ON" : "OFF")
            );
        }

        public static void UpdateGhostMonkey()
        {
            if (!GhostMonkeyEnabled)
                return;

            // Ghost Monkey implementation goes here.
        }

        // =========================
        // INVISIBLE MONKEY
        // =========================

        public static void ToggleInvisibleMonkey()
        {
            InvisibleMonkeyEnabled =
                !InvisibleMonkeyEnabled;

            Debug.Log(
                "Luiki Better: Invisible Monkey " +
                (InvisibleMonkeyEnabled ? "ON" : "OFF")
            );
        }

        public static void UpdateInvisibleMonkey()
        {
            if (!InvisibleMonkeyEnabled)
                return;

            // Invisible Monkey implementation goes here.
        }

        // =========================
        // LONG ARMS
        // =========================

        public static void ActivateLongArms()
        {
            Debug.Log(
                "Luiki Better: Long Arms activated."
            );

            // Long Arms implementation goes here.
        }

        // =========================
        // UPDATE ALL
        // =========================

        public static void Update()
        {
            UpdatePlatforms();
            UpdateGhostMonkey();
            UpdateInvisibleMonkey();
        }
    }
}
