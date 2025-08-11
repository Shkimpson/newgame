using UnityEngine;

/// Minimal controller to drive the HUD and menu index.
public class BattleController : MonoBehaviour
{
    public BattleHUDCanvas Hud;
    public PixelHeart Heart;

    string[] menu = { "FIGHT", "ACT", "ITEM", "MERCY" };
    int idx = 0;

    void Awake()
    {
        if (!Hud)   Hud   = FindObjectOfType<BattleHUDCanvas>();
        if (!Heart) Heart = FindObjectOfType<PixelHeart>();

        Hud.SetEnemy("Sprout");
        Hud.SetHP(20, 20);
        Hud.SetMenu(menu);
        Hud.SetMessage("The air hums.");
        Hud.SetMenuIndex(idx);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))  { idx = (idx + menu.Length - 1) % menu.Length; Hud.SetMenuIndex(idx); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { idx = (idx + 1) % menu.Length;              Hud.SetMenuIndex(idx); }
    }
}
