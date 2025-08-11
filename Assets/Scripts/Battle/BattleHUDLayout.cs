using UnityEngine;

[ExecuteAlways]
public class BattleHUDLayout : MonoBehaviour
{
    [Header("Fractions of screen height/width")]
    [Range(0.05f, 0.40f)] public float MessageHeightFrac = 0.16f; // bottom message box
    [Range(0.05f, 0.35f)] public float MenuHeightFrac    = 0.10f; // menu row
    [Range(0.20f, 0.90f)] public float SoulWidthFrac     = 0.76f; // fight box width
    [Range(0.15f, 0.60f)] public float SoulHeightFrac    = 0.36f; // fight box height
    [Range(0.00f, 0.08f)] public float MarginFrac        = 0.02f;

    // Rects expected by Canvas/Controllers
    public Rect RectEnemy  { get; private set; }  // top bar (enemy name, HP label lives inside)
    public Rect RectSoul   { get; private set; }  // the heart/fight box
    public Rect RectMenu   { get; private set; }  // FIGHT/ACT/ITEM/MERCY row
    public Rect RectMsg    { get; private set; }  // bottom dialog box
    public Rect RectHP     { get; private set; }  // small rect inside top bar (right side)

    public void Recalc(Rect screen)
    {
        float m = Mathf.Round(screen.width * MarginFrac);

        // Top: enemy/name bar
        float enemyH = Mathf.Round(screen.height * MessageHeightFrac);
        RectEnemy = new Rect(screen.xMin + m, screen.yMin + m, screen.width - 2*m, enemyH - m);

        // Middle: soul box
        float soulH = Mathf.Round(screen.height * SoulHeightFrac);
        float soulW = Mathf.Round(screen.width  * SoulWidthFrac);
        float soulY = RectEnemy.yMax + m * 2f;
        float soulX = screen.center.x - soulW * 0.5f;
        RectSoul = new Rect(soulX, soulY, soulW, soulH);

        // Menu row
        float menuH = Mathf.Round(screen.height * MenuHeightFrac);
        RectMenu = new Rect(screen.xMin + m, RectSoul.yMax + m, screen.width - 2*m, menuH);

        // Bottom: message box
        float msgH  = Mathf.Round(screen.height * MessageHeightFrac);
        RectMsg = new Rect(screen.xMin + m, RectMenu.yMax + m, screen.width - 2*m, msgH);

        // Tiny HP box inside the top bar, right-aligned
        float hpW = 160f, hpH = 28f;
        RectHP = new Rect(RectEnemy.xMax - hpW - m, RectEnemy.y + m, hpW, hpH);
    }

    public void Recalc(Vector2Int size) => Recalc(new Rect(0, 0, size.x, size.y));

    void OnValidate() => Recalc(new Rect(0, 0, Screen.width, Screen.height));
    void Update()     => Recalc(new Rect(0, 0, Screen.width, Screen.height));
}
