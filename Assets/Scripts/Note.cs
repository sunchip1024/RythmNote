using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    private RectTransform rect;
    private Image image;

    private const int NOTE_MOVING_TIME = 1000;
    private const int GOOD_JUDGE_OFFSET = 80;
    private const int PERFECT_JUDGE_OFFSET = 20;

    private const int FRAME_UNIT = 10;
    private const float MAX_ALPHA = 1f;

    private int timer;
    private float moveSpeed;
    private float alphaSpeed;
    private DIRECTION dir;

    // Update is called once per frame
    void FixedUpdate() {
    }

    public void onAwake() {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void onStart(DIRECTION dir) {
        timer = 0;
        this.dir = dir;
        moveSpeed = FRAME_UNIT * Direction.toSpawnPosition(dir).magnitude / NOTE_MOVING_TIME;
        alphaSpeed = MAX_ALPHA * FRAME_UNIT / GOOD_JUDGE_OFFSET;

        this.tag = Direction.toString(dir);
        rect.anchoredPosition = Direction.toSpawnPosition(dir);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        this.gameObject.SetActive(true);
        StartCoroutine("moveAnimate");
    }

    IEnumerator moveAnimate() {
        int timer = 0;
        while(timer < NOTE_MOVING_TIME) {
            rect.anchoredPosition += moveSpeed * Direction.toMoveDirection(dir);
            timer += FRAME_UNIT;
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine("destroyAnimate");
        yield return null;
    }

    IEnumerator destroyAnimate() {
        int timer = 0;
        while(timer < GOOD_JUDGE_OFFSET) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - alphaSpeed);
            timer += FRAME_UNIT;
            yield return new WaitForFixedUpdate();
        }

        this.gameObject.SetActive(false);
        yield return null;
    }
}
