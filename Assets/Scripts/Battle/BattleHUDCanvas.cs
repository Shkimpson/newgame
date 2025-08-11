using UnityEngine;
using Weavebound.UI; // for UIStyle (your ScriptableObject)

public class BattleHUDCanvas : MonoBehaviour
{
    [Header("Wiring")]
    public BattleHUDLayout Layout;
    public UIStyle Style;

    [Header("State (set by controllers)")]
    public string   EnemyName = "Sprout";
    public int      HPCurrent = 20, HPMax = 20;
    [TextArea] public string Message = "The air hums.";
    public string[] Menu = new[] { "FIGHT", "ACT", "ITEM", "MERCY" };
    [Range(0, 3)] public int MenuIndex = 0;

    // ---- API kept for existing controllers ----
    public void SetEnemy(string name)               => EnemyName = name;
    public void SetHP(int cur, int max)             { HPCurrent = cur; HPMax = max; }
    public void SetMessage(string msg)              => Message = msg;
    public void SetMenu(string[] items, int index)  { Menu = items; MenuIndex = Mathf.Clamp(index, 0, Mathf.Max(0, items.Length - 1)); }
    public void SetMenuIndex(int index)             => MenuIndex = Mathf.Clamp(index, 0, Mathf.Max(0, (Menu?.Length ?? 1) - 1));
    public Rect GetSoulRect()                       => Layout ? Layout.RectSoul : new Rect(0,0,0,0);
	// Back-compat overloads so older calls still compile.
	public void SetHP(int current)
	{
		SetHP(current, HPMax);        // use the current max we’re already tracking
	}
	
	public void SetMenu(string[] labels)
	{
		SetMenu(labels, MenuIndex);   // keep the currently selected index
	}
    // -------------------------------------------

    void Reset()
    {
        if (!Layout) Layout = GetComponent<BattleHUDLayout>();
    }
    void OnEnable() { if (!Layout) Layout = GetComponent<BattleHUDLayout>(); }
    void Update()   { if (Layout)  Layout.Recalc(new Rect(0,0,Screen.width, Screen.height)); }

    // very light IMGUI to visualize while we don’t have Unity UI prefabs
    void OnGUI()
    {
        if (!Layout) return;

        DrawPanel(Layout.RectEnemy);               // top bar
        DrawText(Layout.RectEnemy, EnemyName, UIAlign.Left);

        // simple HP text on right (you can replace with bar later)
        var hpRect = Layout.RectHP;
        DrawText(hpRect, $"HP  {HPCurrent}/{HPMax}", UIAlign.Right);

        DrawPanel(Layout.RectSoul);                // heart box area
        DrawPanel(Layout.RectMenu);                // menu row
        DrawMenu(Layout.RectMenu);

        DrawPanel(Layout.RectMsg);                 // message box
        DrawText(Layout.RectMsg, Message, UIAlign.Left);
    }

    enum UIAlign { Left, Center, Right }

    void DrawPanel(Rect r)
    {
        // fill
        var old = GUI.color;
        GUI.color = Style ? Style.panel : Color.black;
        GUI.Box(r, GUIContent.none);
        GUI.color = old;

        // simple 1-px border (thicker if you want)
        DrawBorder(r, Style ? Style.border : Color.white, Mathf.Max(1, Style ? Style.borderPx : 2));
    }

    void DrawBorder(Rect r, Color c, float px)
    {
        var t = Texture2D.whiteTexture;
        var old = GUI.color; GUI.color = c;
        GUI.DrawTexture(new Rect(r.x, r.y, r.width, px), t);                     // top
        GUI.DrawTexture(new Rect(r.x, r.yMax - px, r.width, px), t);             // bottom
        GUI.DrawTexture(new Rect(r.x, r.y, px, r.height), t);                    // left
        GUI.DrawTexture(new Rect(r.xMax - px, r.y, px, r.height), t);            // right
        GUI.color = old;
    }

    void DrawText(Rect r, string text, UIAlign align)
    {
        var style = Style ? Style.Label : GUI.skin.label;
        switch (align)
        {
            case UIAlign.Center: style.alignment = TextAnchor.MiddleCenter; break;
            case UIAlign.Right:  style.alignment = TextAnchor.MiddleRight;  break;
            default:             style.alignment = TextAnchor.MiddleLeft;   break;
        }
        GUI.Label(new Rect(r.x + 8, r.y, r.width - 16, r.height), text ?? "", style);
    }

    void DrawMenu(Rect r)
    {
        if (Menu == null || Menu.Length == 0) return;

        int n = Menu.Length;
        float w = r.width / n;
        for (int i = 0; i < n; i++)
        {
            var cell = new Rect(r.x + i * w, r.y, w, r.height);
            if (i == MenuIndex) DrawBorder(cell, Color.yellow, 2f);
            DrawText(cell, Menu[i], UIAlign.Center);
        }
    }
}
