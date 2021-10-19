using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Action MatchTheTile;

    public bool isShake { get; set; }

    [SerializeField]
    private Plane[] planes;

    private Sprite[] sprites;
    private NDRandom ndRamdom;
    
    private void Awake()
    {
        ndRamdom = new NDRandom(0, 6);

        sprites = Resources.LoadAll<Sprite>("Sprites/Cube");

        for (int i = 0; i < planes.Length; i++)
            planes[i].MatchTheTile += OnMatchTheTile;
    }

    public void Ready()
    {
        for (int i = 0; i < planes.Length; i++)
        {
            int random = ndRamdom.Get();
            planes[i].Set(random, sprites[random]);
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeLoop());
    }

    private void OnMatchTheTile()
    {
        MatchTheTile?.Invoke();
    }

    IEnumerator ShakeLoop()
    {
        isShake = true;
        float time = 0.0f;
        float amount = 0.1f;
        Vector3 originPosition = this.transform.position;
        while (time < 0.5f)
        {
            this.transform.position = (Vector3)UnityEngine.Random.insideUnitCircle * amount + originPosition;
            time += Time.deltaTime;
            yield return null;
        }
        this.transform.position = originPosition;
        isShake = false;
    }
}