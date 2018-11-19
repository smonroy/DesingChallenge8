using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    public GameObject blockPrefab;
    public float blockHeight;
    public float blockWidth;
    public float blockMargen;
    public float towerHorizontalPosition;
    public Color color1;
    public Color color2;
    public float blockSpeed;

    private float currentPosition;
    private float currentWidth;
    private Stack<GameObject> tower;
    private GameObject movingBlock;
    private bool movingRight;

	// Use this for initialization
	void Start () {
        tower = new Stack<GameObject>();
        movingRight = false;
        RestartTower();
        GetNewBlock();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            float diff = tower.Peek().transform.position.x - movingBlock.transform.position.x;
            currentWidth -= Mathf.Abs(diff);
            if (currentWidth < 0) {
                RestartTower();
            } else {
                movingBlock.transform.localScale = new Vector3(currentWidth, blockHeight, 1);
                movingBlock.transform.position += new Vector3(diff / 2, 0, 0);
                movingBlock.GetComponent<Renderer>().material.color = color1;
                tower.Push(movingBlock);
            }
            GetNewBlock();
        }
    }

    void FixedUpdate () {
        if(movingBlock != null) {
            if (movingRight && movingBlock.transform.position.x > tower.Peek().transform.position.x + currentWidth + blockMargen) {
                movingRight = false;
            } else if (!movingRight && movingBlock.transform.position.x < tower.Peek().transform.position.x - currentWidth - blockMargen) {
                movingRight = true;
            }
            movingBlock.transform.position += new Vector3(movingRight ? blockSpeed : -blockSpeed, 0, 0);
        }
    }

    private void GetNewBlock() {
        currentPosition += blockHeight;
        movingBlock = Instantiate(blockPrefab, new Vector3(towerHorizontalPosition, currentPosition), Quaternion.identity, this.transform);
        movingBlock.transform.localScale = new Vector3(currentWidth, blockHeight, 1);
        movingBlock.GetComponent<Renderer>().material.color = color2;
    }

    private void RestartTower() {
        foreach(GameObject go in tower) {
            Destroy(go);
        }
        if(movingBlock != null) {
            Destroy(movingBlock);
        }
        tower.Clear();
        currentWidth = blockWidth;
        currentPosition = -4.5f + (blockHeight / 2);
        var block = Instantiate(blockPrefab, new Vector3(towerHorizontalPosition, currentPosition), Quaternion.identity, this.transform);
        block.transform.localScale = new Vector3(blockWidth, blockHeight, 1);
        block.GetComponent<Renderer>().material.color = color1;
        tower.Push(block);
    }
}
