using UnityEngine;

public static class GUIStyleSet
{
    public static GUIStyle title
    {
        get
        {
            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold
            };

            return style;
        }
    }
}
