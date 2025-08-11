using UnityEngine;

[CreateAssetMenu(fileName = "UIStyle", menuName = "Weavebound/UI Style")]
public class UIStyle : ScriptableObject
{
    [Header("Font")]
    public Font font;
    public int fontSize = 22;

    [Header("Colors")]
    public Color text = Color.white;
    public Color panel = Color.black;
    public Color border = Color.white;

    [Header("Geometry")]
    [Min(1)] public int borderPx = 2;

    GUIStyle _label;
    public GUIStyle Label
    {
        get
        {
            if (_label == null) _label = new GUIStyle(GUI.skin.label);
            _label.font = font;
            _label.fontSize = fontSize;
            _label.normal.textColor = text;
            return _label;
        }
    }
}
