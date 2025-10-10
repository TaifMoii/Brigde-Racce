using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class StackManager : MonoBehaviour
{
    public Transform stackRoot;    // Empty object phía sau lưng để xếp block
    public float step = 0.25f;     // khoảng cách giữa các block
    private List<GameObject> stack = new List<GameObject>();
    Character character;


    // kiểm tra có block không
    public bool HasBlocks() => stack.Count > 0;

    // thêm block vào stack
    public void PickUp(GameObject blockGO)
    {
        CubeColor b = blockGO.GetComponent<CubeColor>();
        if (b == null || b.isCollected) return;
        b.isCollected = true;

        stack.Add(blockGO);

        character.AddBrick(blockGO);
        blockGO.transform.localRotation = Quaternion.identity;
        blockGO.transform.localPosition = Vector3.up * step * stack.Count;

        // tắt vật lý để block không rơi
        if (blockGO.TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = true;
        if (blockGO.TryGetComponent<Collider>(out var col)) col.enabled = false;

    }

    // lấy block ra khi đặt xuống cầu
    public GameObject Pop()
    {
        if (stack.Count == 0) return null;
        var last = stack[^1];
        stack.RemoveAt(stack.Count - 1);
        return last;
    }

    public int Count => stack.Count;
    public IEnumerator RespawnBlock(GameObject mapBlock, float delay)
    {
        yield return new WaitForSeconds(delay);
        mapBlock.SetActive(true);
    }
}
