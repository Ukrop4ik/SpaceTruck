using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public enum LevelState
    {
        Normal,
        Pause,
        Win,
        Lose
    }

    [Range(0f,5f)]
    public float _timeSpeed = 1f;

    private static World instance;
    public static World Instance() { return instance; }

    public float worldTime = 60f;

    private void Awake()
    {
        instance = this;
    }

    public void SetTimeSpeed(float speed)
    {

    }

    private void Update()
    {
        worldTime -= Time.deltaTime * _timeSpeed;
    }
}
