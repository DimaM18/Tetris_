using Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements;

using UnityEngine;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Submenu
{
    public static class EmptySubmenu
    {
        public static void Create(DDMContext context)
        {
            DDMSubmenu.Create(context, "Test submenu", () => context.Main.CreatePage("HUD", GenerateContent));
        }

        private static void GenerateContent(DDMContext context)
        {
            DDMButton.Create(context, "Do nothing", () => Debug.Log("nothing"));
        }
    }
}