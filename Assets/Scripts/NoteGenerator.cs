using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour {
    private const int NOTE_POOL_INIT_SIZE = 30;
    private static int notePoolHead = 0;

    public static GameObject notePrefab;
    private static RectTransform parentNote;
    private static List<Note> notePool = new List<Note>();

    public static readonly Dictionary<DIRECTION, Vector2> spawnPoints = new Dictionary<DIRECTION, Vector2>();
    


    void Start() {
        initializeSpawnPoint();
        initializeNotePool();
        StartCoroutine("ActiveNotePerSecond");
    }

    private void initializeSpawnPoint() {
        spawnPoints.Add(DIRECTION.NORTH, GameObject.FindWithTag("NORTH").GetComponent<RectTransform>().anchoredPosition);
        spawnPoints.Add(DIRECTION.SOUTH, GameObject.FindWithTag("SOUTH").GetComponent<RectTransform>().anchoredPosition);
        spawnPoints.Add(DIRECTION.EAST, GameObject.FindWithTag("EAST").GetComponent<RectTransform>().anchoredPosition);
        spawnPoints.Add(DIRECTION.WEST, GameObject.FindWithTag("WEST").GetComponent<RectTransform>().anchoredPosition);
    }

    private void initializeNotePool() {
        parentNote = GameObject.Find("/Canvas/Notes").GetComponent<RectTransform>();
        notePrefab = parentNote.GetChild(0).gameObject;
        while (notePool.Count < NOTE_POOL_INIT_SIZE) {
            notePool.Add(createNote());
        }
    }

    

    private static Note createNote() {
        Note note = Instantiate(notePrefab).GetComponent<Note>();
        note.gameObject.SetActive(false);
        note.transform.SetParent(parentNote);
        note.onAwake();
        return note;
    }

    public static Note activeNote(DIRECTION dir) {
        Note note = notePool[notePoolHead];

        if (note.gameObject.activeSelf) {
            note = createNote();
            notePool.Insert(notePoolHead, note);
        }

        notePoolHead = (notePoolHead + 1) % notePool.Count;

        note.onStart(dir);
        return note;
    }

    IEnumerator ActiveNotePerSecond() {
        while(true) {
            activeNote(DIRECTION.NORTH);
            yield return new WaitForSeconds(0.25f);
            activeNote(DIRECTION.EAST);
            yield return new WaitForSeconds(0.25f);
            activeNote(DIRECTION.SOUTH);
            yield return new WaitForSeconds(0.25f);
            activeNote(DIRECTION.WEST);
            yield return new WaitForSeconds(0.25f);
        }
    }
}