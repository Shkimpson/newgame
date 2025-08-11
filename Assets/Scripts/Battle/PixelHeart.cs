using UnityEngine;

/// Very small “heart” placeholder that moves inside the soul box.
/// Arrow keys move it; scaled to fit the soul rect from BattleHUDCanvas.
public class PixelHeart : MonoBehaviour
{
    public BattleHUDCanvas Hud;
    public Color color = new Color(1f, 0.9f, 0.2f);
    public Vector2 posNorm = new Vector2(0.5f, 0.5f); // 0..1 within soul rect

    void Awake() { if (!Hud) Hud = FindObjectOfType<BattleHUDCanvas>(); }

    void Update()
    {
        if (!Hud) return;
        float s = 0.9f * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))  posNorm.x -= s;
        if (Input.GetKey(KeyCode.RightArrow)) posNorm.x += s;
        if (Input.GetKey(KeyCode.UpArrow))    posNorm.y -= s;
        if (Input.GetKey(KeyCode.DownArrow))  posNorm.y += s;
        posNorm = Vector2.Max(Vector2.zero, Vector2.Min(Vector2.one, posNorm));
    }

    void OnGUI()
    {
        if (!Hud) return;
        Rect soul = Hud.GetSoulRect();
        float px = Mathf.Min(soul.width, soul.height) * 0.05f;
        float x = soul.x + posNorm.x * (soul.width  - px);
        float y = soul.y + posNorm.y * (soul.height - px);
        var prev = GUI.color;
        GUI.color = color;
        GUI.Box(new Rect(x, y, px, px), GUIContent.none);
        GUI.color = prev;
    }
}
