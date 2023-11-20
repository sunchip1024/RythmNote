using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    //private SheetManager sheet;

    public GameObject notePrefab;
    public enum DIRECTION { NORTH, EAST, SOUTH, WEST };

    private Dictionary<DIRECTION, Vector2> spawnPoints = new Dictionary<DIRECTION, Vector2>();

    [SerializeField]
    private const int NOTE_POOL_INIT_SIZE = 50;
    private int notePoolHead = 0;
    private RectTransform parentNote;
    private List<RectTransform> notePool = new List<RectTransform>();

    void Start() {
        initializeSpawnPoint();
        initializeNotePool();
    }

    private void initializeSpawnPoint() {
        spawnPoints.Add(DIRECTION.NORTH, GameObject.Find("N").GetComponent<RectTransform>().anchoredPosition);
        spawnPoints.Add(DIRECTION.SOUTH, GameObject.Find("S").GetComponent<RectTransform>().anchoredPosition);
        spawnPoints.Add(DIRECTION.EAST, GameObject.Find("E").GetComponent<RectTransform>().anchoredPosition);
        spawnPoints.Add(DIRECTION.WEST, GameObject.Find("W").GetComponent<RectTransform>().anchoredPosition);
    }

    private void initializeNotePool() {
        parentNote = GameObject.Find("/Canvas/Notes").GetComponent<RectTransform>();
        while (notePool.Count < NOTE_POOL_INIT_SIZE) {
            notePool.Add(createNote());
        }
    }

    private RectTransform createNote() {
        RectTransform note = Instantiate(notePrefab).GetComponent<RectTransform>();
        note.gameObject.SetActive(false);
        note.SetParent(parentNote);
        return note;
    }

    private RectTransform getNote()
    {
        RectTransform note = notePool[notePoolHead];
        if (note.gameObject.activeSelf) {
            note = createNote();
            notePool.Insert(notePoolHead, note);
        }

        notePoolHead = (notePoolHead + 1) % notePool.Count;
        return note;
    }
}