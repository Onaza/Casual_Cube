using System;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    public Func<Transform, Direction, bool> CanMoved;

    [SerializeField]
    private Cube cube;
    [SerializeField]
    private Rotator rotator;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cube.gameObject.SetActive(false);
    }

    public void Ready()
    {
        cube.gameObject.SetActive(true);
    }

    public void OnSwiped(Direction direction)
    {
        if (CanMoved?.Invoke(cube.transform, direction) == false)
        {
            cube.Shake();
            audioSource.Play();
            return;
        }
        rotator.Rotate(direction);
    }
}
