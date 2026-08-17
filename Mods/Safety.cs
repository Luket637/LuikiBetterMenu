using UnityEngine;

namespace LuikiBetter.Mods
{
    public static class Safety
    {
        public static bool AntiKickEnabled;
        public static bool AntiBanEnabled;
        public static bool TosAccepted;

        // =========================
        // ANTI-KICK
        // =========================

        public static void ToggleAntiKick()
        {
            AntiKickEnabled = !AntiKickEnabled;

            Debug.Log(
                "Luiki Better: Anti-Kick " +
                (AntiKickEnabled ? "ON" : "OFF")
            );
        }

        // =========================
        // ANTI-BAN
        // =========================

        public static void ToggleAntiBan()
        {
            AntiBanEnabled = !AntiBanEnabled;

            Debug.Log(
                "Luiki Better: Anti-Ban " +
                (AntiBanEnabled ? "ON" : "OFF")
            );
        }

        // =========================
        // ACCEPT TOS
        // =========================

        public static void AcceptToS()
        {
            TosAccepted = true;

            Debug.Log(
                "Luiki Better: ToS accepted."
            );
        }

        // =========================
        // UPDATE
        // =========================

        public static void Update()
        {
            if (AntiKickEnabled)
            {
                // Safe Anti-Kick implementation
                // goes here.
            }

            if (AntiBanEnabled)
            {
                // Safe Anti-Ban implementation
                // goes here.
            }
        }
    }
}
