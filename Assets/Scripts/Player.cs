using System;
using UnityEngine;
public enum Direction {NONE, UP, DOWN, RIGHT, LEFT, ALL }

public class Player : MonoBehaviour
{
    public Action MatchTheTile;
    public Func<Transform, Direction, bool> CanMoved;

    [SerializeField]
    private Cube cube;
    [SerializeField]
    private Rotator rotator;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        cube.MatchTheTile += OnMatchTheTile;
        cube.gameObject.SetActive(false);
    }

    public void Ready()
    {
        cube.gameObject.SetActive(true);
        cube.Ready();
    }

    public void End()
    {
        cube.gameObject.SetActive(false);
        audioSource.Stop();
    }

    private void OnMatchTheTile()
    {
        MatchTheTile?.Invoke();
    }

    public void OnSwiped(Direction direction)
    {
        if (cube.isShake == true)
            return;

        if (CanMoved?.Invoke(cube.transform, direction) == false)
        {
            audioSource.Play();
            cube.Shake();
            return;
        }
        rotator.Rotate(direction);
    }
}