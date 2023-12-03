using UnityEngine;

public class Conductor : MonoBehaviour
{
    public bool SpawnerOn = false;
    public float songBpm;
    public float secPerBeat;
    public float songPositionInBeats;
    public float firstBeatOffset;
    public float dspSongTime;
    float songPosition;

    [SerializeField] private GameObject levelClearedScreen;

    [System.Serializable]
    public class Note
    {
        public float beat;
    }

    public Transform[] spawnPoints;
    public GameObject[] notePrefab;
    public Note[] notes;

    int notePos = 0;

    void Start()
    {
        //record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        secPerBeat = 60 / songBpm;
    }

    void FixedUpdate()
    {
        if(!PauseMenu.GameIsPaused)
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

            if(SpawnerOn)
            {
                if (notes[notePos].beat == Mathf.Round(songPositionInBeats))
                {
                    if (notePos < notes.Length - 1)
                    {
                        Debug.Log("Element " + notePos + " : Beat " + notes[notePos].beat);
                        GenerateNote();
                        notePos += 1;
                    }
                    else
                    {
                        Invoke("ShowLevelClearedScreen", 2);
                    }
                }
            }
        }
        else
        {
            songPosition += 0;
            //musicSource.Stop();
        }
    }

    void GenerateNote()
    {
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        int randNotePrefab = Random.Range(0, notePrefab.Length);
        Instantiate(notePrefab[randNotePrefab], spawnPoints[randSpawnPoint].position, transform.rotation);
    }

    void ShowLevelClearedScreen()
    {
        levelClearedScreen.SetActive(true);
    }
}
