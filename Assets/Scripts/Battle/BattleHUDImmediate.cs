using UnityEngine;

/// Thin glue that updates the canvas every frame. Keep logic elsewhere.
public class BattleHUDImmediate : MonoBehaviour
{
    public BattleHUDLayout Layout;
    public BattleHUDCanvas Canvas;
	
    // Example default state
    public string EnemyName = "Sprout";
    public int HPCurrent = 20, HPMax = 20;
    [TextArea] public string Message = "The air hums.";
    public string[] Menu = new[] { "FIGHT", "ACT", "ITEM", "MERCY" };
    [Range(0, 3)] public int MenuIndex = 0;

    void Reset()
    {
        Layout = GetComponent<BattleHUDLayout>();
        Canvas = GetComponent<BattleHUDCanvas>();
    }

    void OnEnable()
    {
        if (Layout == null) Layout = GetComponent<BattleHUDLayout>();
        if (Canvas == null) Canvas = GetComponent<BattleHUDCanvas>();
        Apply();
    }

    void Update() => Apply();

    void Apply()
    {
        if (Canvas == null) return;
        Canvas.Layout    = Layout;
        Canvas.EnemyName = EnemyName;
        Canvas.HPCurrent = HPCurrent;
        Canvas.HPMax     = HPMax;
        Canvas.Message   = Message;
        Canvas.Menu      = Menu;
        Canvas.MenuIndex = MenuIndex;
    }
}
