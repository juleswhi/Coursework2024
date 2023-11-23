namespace FormsLib.Controls;

internal static class ControlHelper
{
    public static void SelectByIndex(this List<MenuOption> options, int index)
    {
        foreach (var option in options) option.Selected = false;
        options[index].Selected = true;
    }

    public static void SelectNext(this List<MenuOption> options, int next)
    {
        bool found = false;
        for (int i = next == 1 ? 0 : options.Count; i < (next == 1 ? options.Count : 0); i = next == 1 ? next + 1 : next - 1)
        {
            if (found) { options[i].Selected = true; return; }

            else if (options[i].Selected) { options[i].Selected = false; found = true; }
        }
    }
}
