using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
	public AudioSource[] sfxButtonClick;
	public AudioSource[] NoteSequences;
	public AudioSource[] NoteKeySeqs;

	private AudioClip[] noteClips = new AudioClip[12];

	//note animation
	private bool PlayingNoteSequence = false;

	float elaspedTime = 0f;
	float waitTime = 0.25f;

	public const int MaxNotes = 16;
	private int NumNotesInSequence;
	private int CurrentNoteInSequence;
	private int[] noteData;
	private int[] intervalData;

	public static AudioController Instance;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{
		intervalData = new int[16] {0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0}; 
		noteData = new int[16] {-1,-1,-1,-1,-1,-1,-1,-1, -1,-1,-1,-1,-1,-1,-1,-1}; 

		MakeSubClips (0);
	}

	void Update () 
	{
		if (PlayingNoteSequence == true) {

			float delta = Time.deltaTime;
			elaspedTime += delta;
			if (elaspedTime > waitTime) {
				elaspedTime = 0f;


				int index = Random.Range (0, NumNotesInSequence);

				int nindex = CurrentNoteInSequence;

				PlayNote (noteData[nindex]);

				CurrentNoteInSequence++;
				if (CurrentNoteInSequence >= NumNotesInSequence) {
					CurrentNoteInSequence = 0;
				}
			}
		}

	}

	public void PlayButtonClick (int index) 
	{
		sfxButtonClick[index].Play();
	}

	public void PlayNote (int index) 
	{
		NoteKeySeqs[index].clip = noteClips [index];
		NoteKeySeqs[index].Play ();
	}

	public void StartPlaySequence () 
	{
		PlayingNoteSequence = true;
		elaspedTime = 0f;
		CurrentNoteInSequence = 0;
	}

	public void StopPlaySequence () 
	{
		PlayingNoteSequence = false;
		elaspedTime = 0f;
	}





	public void ApplyFormAudio(int key, int formIndex)
	{
		for (int i = 0; i < MaxNotes; i++) {
			intervalData [i] = 0;
			noteData [i] = -1;
		}

		NumNotesInSequence =  FormData.Instance.GetKeyNoteBucket(key, formIndex, ref noteData, ref intervalData);
	}






	private void MakeSubClips (int seqIndex) 
	{
		AudioSource asource = NoteSequences[seqIndex]; 

		AudioClip csource = asource.clip;

		noteClips [0] = MakeSubclip (csource, 0.0f, 0.8f, 0);
		noteClips [1] = MakeSubclip (csource, 1.0f, 1.8f, 1);
		noteClips [2] = MakeSubclip (csource, 2.0f, 2.8f, 2);
		noteClips [3] = MakeSubclip (csource, 3.0f, 3.8f, 3);
		noteClips [4] = MakeSubclip (csource, 4.0f, 4.8f, 4);
		noteClips [5] = MakeSubclip (csource, 5.0f, 5.8f, 5);
		noteClips [6] = MakeSubclip (csource, 6.0f, 6.8f, 6);
		noteClips [7] = MakeSubclip (csource, 7.0f, 7.8f, 7);
		noteClips [8] = MakeSubclip (csource, 8.0f, 8.8f, 8);
		noteClips [9] = MakeSubclip (csource, 9.0f, 9.8f, 9);
		noteClips [10] = MakeSubclip (csource, 10.0f, 10.8f, 10);
		noteClips [11] = MakeSubclip (csource, 11.0f, 11.8f, 11);
	}



	/**
      * Creates a sub clip from an audio clip based off of the start time
      * and the stop time. The new clip will have the same frequency as
      * the original.
      */
	private AudioClip MakeSubclip(AudioClip clip, float start, float stop, int index)
	{
		/* Create a new audio clip */
		int frequency = clip.frequency;
		float timeLength = stop - start;
		int samplesLength = (int)(frequency * timeLength);
		AudioClip newClip = AudioClip.Create(clip.name + "-sub" + index, samplesLength, 2, frequency, false);

		/* Create a temporary buffer for the samples */
		float[] data = new float[samplesLength];

		/* Get the data from the original clip */
		clip.GetData(data, (int)(frequency * start));

		/* Transfer the data to the new clip */
		newClip.SetData(data, 0);

		/* Return the sub clip */
		return newClip;
	}
}
