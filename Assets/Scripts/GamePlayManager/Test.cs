using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Test : Character
{
    public ColorType teamColor;
    public float detectRange = 25f;
    public Transform bridgeEntrance;
    public int carryThreshold = 3;           // đủ bao nhiêu block thì về cầu


    private NavMeshAgent agent;
    private Transform target;
    private enum State { FindBlock, GoBlock, GoBridge, Idle }
    private State state = State.FindBlock;
    Character character;
    Finish finish;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    bool TargetInvalid(Transform tf)
    {
        if (tf == null) return true;
        var go = tf.gameObject;
        if (!go.activeInHierarchy) return true;

        var b = tf.GetComponent<CubeColor>(); // script màu của block
        if (b == null || b.isCollected || b.cubeColor != teamColor) return true;

        return false;
    }
    void Update()
    {
        switch (state)
        {
            case State.FindBlock:
                target = FindNearestBlock();
                // Debug.Log(target.position);
                if (target != null)
                {
                    agent.SetDestination(target.position);
                    state = State.GoBlock;
                }

                break;

            case State.GoBlock:

                if (TargetInvalid(target))
                {
                    StartCoroutine(WaitFind());
                    agent.ResetPath();
                    target = null;
                    state = State.FindBlock;
                    break;
                }
                agent.SetDestination(target.position);

                if (Vector3.Distance(transform.position, target.position) < 1.2f)
                {
                    // ĐÁNH DẤU + ẨN MAP BLOCK, rồi nhặt
                    var cube = target.GetComponent<CubeColor>();
                    if (cube != null)
                    {
                        cube.isCollected = true;
                        StartCoroutine(WaitFind());
                        cube.isCollected = false;
                    }
                    // character.CubeAdd(target.gameObject);
                    target = null;

                    if (cubeLists.Count >= carryThreshold)
                        state = State.GoBridge;
                }
                break;

            case State.GoBridge:
                agent.SetDestination(bridgeEntrance.position);
                if (!HasBlocks())
                    state = State.FindBlock;
                break;
            case State.Idle:
                gameObject.transform.position = Vector3.zero;
                break;
        }
        if (finish == null)
        {
            finish = FindObjectOfType<Finish>();
        }
        else
        {
            if (finish.isFinish)
            {
                state = State.Idle;
                agent.ResetPath();
            }
        }
    }


    private IEnumerator WaitFind()
    {
        yield return new WaitForSeconds(3f);
    }
    bool HasBlocks()
    {
        character = GetComponent<Character>();
        if (character != null && cubeHolder.transform.childCount > 0)
            return true;
        return false;
    }

    [SerializeField] private List<CubeColor> listCube = new List<CubeColor>();
    [SerializeField] private List<CubeColor> cubeDefault = new List<CubeColor>();
    Transform FindNearestBlock()
    {
        listCube = cubeDefault.Where(b => b != null && b.gameObject.activeInHierarchy && b.cubeColor == teamColor).ToList();

        float minDist = Mathf.Infinity;
        Transform best = null;

        foreach (var b in listCube)
        {
            float d = Vector3.Distance(transform.position, b.transform.position);
            if (d < minDist && d < detectRange)
            {
                minDist = d;
                best = b.transform;
            }
        }
        return best;
    }


    Stair FindNearestTile()
    {
        var tiles = FindObjectsOfType<Stair>()
            .Where(t => !t.filled && t.stairColor == teamColor);

        float minDist = Mathf.Infinity;
        Stair best = null;
        foreach (var t in tiles)
        {
            float d = Vector3.Distance(transform.position, t.transform.position);
            if (d < minDist)
            {
                minDist = d;
                best = t;
            }
        }
        return best;
    }
}
