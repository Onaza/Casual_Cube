using System;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Action MatchTheTile;

    [SerializeField]
    private Plane pairPlane;

    private SpriteRenderer spriteRenderer;
    public int type { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Set(int type, Sprite sprite)
    {
        this.type = type;
        spriteRenderer.sprite = sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        Tile tile = other.GetComponent<Tile>();
        if (tile == null)
            return;

        if (pairPlane.type != tile.type)
            return;

        // Effect
        EffectManager.Instance.SetAndPlay(tile.transform, tile.type);

        // 鸥老 秦力
        tile.Release();

        // 鸥老 老摹
        MatchTheTile?.Invoke();
    }
}