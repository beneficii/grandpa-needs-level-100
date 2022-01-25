using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisuals : MonoBehaviour
{
    public SpriteRenderer rend;

    public List<Sprite> spawnSprites;
    public List<Sprite> idleSprites;
    public List<Sprite> deathSprites;

    List<Sprite> frames;


    public float frameRate = 0.2f;

    int currentIdx = 0;
    float nextFrame = -1f;

    System.Action currentAction = null;
    bool currentIsLoop;

    public List<Sprite> GetFrames(AnimState state)
    {
        switch (state)
        {
            case AnimState.Spawn: return spawnSprites;
            case AnimState.Idle: return idleSprites;
            case AnimState.Death: return deathSprites;
            default: return null;
        }
    }

    public void LaunchOnce(AnimState state, System.Action onComplete = null)
    {
        currentIsLoop = false;
        currentAction = onComplete;
        SetState(state);
    }

    public void LaunchLoop(AnimState state)
    {
        currentIsLoop = true;
        SetState(state);
    }

    void SetState(AnimState state)
    {
        frames = GetFrames(state);
        if(frames == null || frames.Count == 0)
        {
            currentAction?.Invoke();
            return;
        }
        currentIdx = -1;
        
        NextFrame();
    }

    private void Update()
    {
        if(nextFrame > 0f && Time.time >= nextFrame)
        {
            NextFrame();
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            LaunchOnce(AnimState.Spawn);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            LaunchLoop(AnimState.Idle);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LaunchOnce(AnimState.Death);
        }
    }

    void NextFrame()
    {
        currentIdx++;
        if(currentIdx >= frames.Count)
        {
            if(currentIsLoop)
            {
                currentIdx = 0;
            } else
            {
                nextFrame = -1f;
                currentAction?.Invoke();
                return;
            }
        }

        rend.sprite = frames[currentIdx];
        nextFrame = Time.time + frameRate;
    }
}


public enum AnimState
{
    Spawn,
    Idle,
    Death,
}