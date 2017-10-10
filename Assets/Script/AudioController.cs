using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
	
	public AudioSource[] sfxButtonClick;
	public AudioSource[] NoteSequences;
	public AudioSource[] NoteKeySeqs;
	public AudioSource[] BassKeySeqs;

	private AudioClip[] noteClips = new AudioClip[24];
	private AudioClip[] bassClips = new AudioClip[24];

	//note animation
	private bool PlayingNoteSequence = false;

	float elaspedTime1 = 0f;
	float elaspedTime2 = 0f;
	float noteWaitTime1 = 1f;
	float noteWaitTime2 = 1f;

	public const int MaxNotes = 16;
	private int OctavePointInSequence;
	private int NumNotesInSequence;
	private int CurrentNoteInSequence;
	private int[] noteData;
	private int[] intervalData;
	private int[] notes2OctaveData;

	private int NoteSourceIndex = 0;
	private int BassSourceIndex = 0;

	private int SampleBank = 0;
	private int DroneBank = 0;

	private int ScaleVoiceIndex = 0;
	private int PedalToneIndex = 0;
	private bool accendingORdecending = true;
	private int RandomChance = 0;
	private int RandomChance2 = 0;
	private int FifthNote = 0;

	[HideInInspector]
	public float randomAmount = 0f;



	public static AudioController Instance;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{
		intervalData = new int[16] {0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0}; 
		noteData = new int[16] {-1,-1,-1,-1,-1,-1,-1,-1, -1,-1,-1,-1,-1,-1,-1,-1}; 
		notes2OctaveData = new int[32]; 

		MakeSubClips (0, 2.5f);//notes
		MakeSubClips (1, 2.5f);//bass
	}

	void Update () 
	{
		if (PlayingNoteSequence == true) {

			float delta = Time.deltaTime;

			//SCALE NOTES
			elaspedTime1 += delta;
			if (elaspedTime1 > noteWaitTime1) {
				elaspedTime1 = 0f;

				bool skipNote = false;

				int octaveAdd = 0;
				if (CurrentNoteInSequence >= OctavePointInSequence) {
					octaveAdd = 12;
				}

				int noteToPlay = notes2OctaveData[CurrentNoteInSequence] + octaveAdd;

				if (ScaleVoiceIndex == (int)Globals._arpeggioStyle.Accending) {

					CurrentNoteInSequence++;
					if (CurrentNoteInSequence >= NumNotesInSequence) {
						CurrentNoteInSequence = 0;
					}

					int chance = Random.Range (0, 100);
					if (chance < RandomChance) {
						int randomNoteIndex = Random.Range (0, NumNotesInSequence);

						if (randomNoteIndex != CurrentNoteInSequence) {
							CurrentNoteInSequence = randomNoteIndex;
						} else {
							skipNote = true;
						}
					}

				
				} else if (ScaleVoiceIndex == (int)Globals._arpeggioStyle.Decending) {
					
					CurrentNoteInSequence--;
					if (CurrentNoteInSequence < 0) {
						CurrentNoteInSequence = NumNotesInSequence - 1;
					}

					int chance = Random.Range (0, 100);
					if (chance < RandomChance) {
						int randomNoteIndex = Random.Range (0, NumNotesInSequence);

						if (randomNoteIndex != CurrentNoteInSequence) {
							CurrentNoteInSequence = randomNoteIndex;
						} else {
							skipNote = true;
						}
					}


				} else if (ScaleVoiceIndex == (int)Globals._arpeggioStyle.AccendDecend) {


					if (accendingORdecending == true) {
					
						CurrentNoteInSequence++;
						if (CurrentNoteInSequence >= NumNotesInSequence) {
							CurrentNoteInSequence = NumNotesInSequence - 2;
							accendingORdecending = false;
						}

					} else {
					
						CurrentNoteInSequence--;
						if (CurrentNoteInSequence < 0) {
							CurrentNoteInSequence = 1;
							accendingORdecending = true;
						}

					}

					int chance = Random.Range (0, 100);
					if (chance < RandomChance) {
						int randomNoteIndex = Random.Range (0, NumNotesInSequence);

						if (randomNoteIndex != CurrentNoteInSequence) {
							CurrentNoteInSequence = randomNoteIndex;
						} else {
							skipNote = true;
						}
					}

				} else if (ScaleVoiceIndex == (int)Globals._arpeggioStyle.RandomChords) {

					//get root note
					if (accendingORdecending == true) {

						CurrentNoteInSequence++;
						if (CurrentNoteInSequence >= NumNotesInSequence) {
							CurrentNoteInSequence = NumNotesInSequence - 2;
							accendingORdecending = false;
						}

					} else {

						CurrentNoteInSequence--;
						if (CurrentNoteInSequence < 0) {
							CurrentNoteInSequence = 1;
							accendingORdecending = true;
						}

					}

					int chance = Random.Range (0, 100);
					if (chance < RandomChance) {
						int randomNoteIndex = Random.Range (0, NumNotesInSequence);

						if (randomNoteIndex != CurrentNoteInSequence) {
							CurrentNoteInSequence = randomNoteIndex;
						} else {
							skipNote = true;
						}
					}

					int randomChordNoteIndex = Random.Range (0, NumNotesInSequence);
					int noteToPlay2 = notes2OctaveData[randomChordNoteIndex] + octaveAdd;
					if(noteToPlay2 != noteToPlay) {
						PlayNote (noteToPlay2);
					}

					randomChordNoteIndex = Random.Range (0, NumNotesInSequence);
					int noteToPlay3 = notes2OctaveData[randomChordNoteIndex] + octaveAdd;
					if(noteToPlay2 != noteToPlay3 && noteToPlay != noteToPlay3) {
						PlayNote (noteToPlay3);
					}



				}
					
				if(skipNote == false) {
					PlayNote (noteToPlay);
				}
			}


			//PEDAL TONES NOTES
			bool dontPlay = false;
			elaspedTime2 += delta;
			if (elaspedTime2 > noteWaitTime2) {
				elaspedTime2 = 0f;

				int noteToPlay = notes2OctaveData[0];

				if(PedalToneIndex == 0) {//just root

					//bool skipNote = false;
					int chance = Random.Range (0, 100);
					if (chance < RandomChance2) {
						
					}

				}
				else if(PedalToneIndex == 1) {//root + 5th

					int chance = Random.Range (0, 100);
					if (chance < 25) {
						if(FifthNote > 0) {
							noteToPlay = FifthNote;
						
						}
					}

				} else {
					dontPlay = true;
				}
					
				if(dontPlay == false) {
					PlayBassNote (noteToPlay);
				}

			}

		}

	}

	public void SetSoundOptions (int scaleVoiceIndex, float tempo1, int randomChance, int pedalToneIndex, float tempo2, int randomChance2) 
	{

		ScaleVoiceIndex = scaleVoiceIndex;
		PedalToneIndex = pedalToneIndex;

		noteWaitTime1 = 60f / tempo1;
		noteWaitTime2 = 60f / tempo2;

		RandomChance = randomChance;
		RandomChance2 = randomChance2;



	}


	public void PlayButtonClick (int index) 
	{
		sfxButtonClick[index].Play();
	}

	public void PlayNote (int index) 
	{
		NoteKeySeqs[NoteSourceIndex].clip = noteClips [index];
		NoteKeySeqs[NoteSourceIndex].Play ();

		NoteSourceIndex++;
		if (NoteSourceIndex >= 12) {
			NoteSourceIndex = 0;
		}
	}

	public void PlayBassNote (int index) 
	{
		BassKeySeqs[BassSourceIndex].clip = bassClips [index];
		BassKeySeqs[BassSourceIndex].Play ();

		BassSourceIndex++;
		if (BassSourceIndex >= 8) {
			BassSourceIndex = 0;
		}
	}

	public void StartPlaySequence () 
	{
		PlayingNoteSequence = true;
		elaspedTime1 = 0f;
		elaspedTime2 = 0f;
		CurrentNoteInSequence = 0;

		Debug.LogError ("NumNotesInSequence = " + NumNotesInSequence);
	}

	public void StopPlaySequence () 
	{
		PlayingNoteSequence = false;
		elaspedTime1 = 0f;
		elaspedTime2 = 0f;
	}

	public void SetSampleBank (int index) 
	{
		SampleBank = index;
		MakeSubClips (SampleBank, 2f);

	}
	public void SetDroneBank (int index) 
	{
		DroneBank = index;

	}




	public void ApplyFormAudio(int key, int formIndex)
	{
		for (int i = 0; i < MaxNotes; i++) {
			intervalData [i] = 0;
			noteData [i] = -1;
		}

		NumNotesInSequence =  FormData.Instance.GetKeyNoteBucket(key, formIndex, ref noteData, ref intervalData);

		//Debug.LogError("intervals : " + intervalData[0] + " " + intervalData[1] + " " + intervalData[2] + " " + intervalData[3] + " " + intervalData[4] +  " " + intervalData[5]);


		FifthNote = 0;
		for (int i = 0; i < MaxNotes; i++) {
			int interval = intervalData[i];
			if(interval == 7){
				FifthNote = noteData[i];
				break;
			}
		}
		if(FifthNote == 0) {
			for (int i = 0; i < MaxNotes; i++) {
				int interval = intervalData[i];
				if(interval == 6){ //b5th
					FifthNote = noteData[i];
					break;
				}
			}
		}
		if(FifthNote == 0) {
			for (int i = 0; i < MaxNotes; i++) {
				int interval = intervalData[i];
				if(interval == 8){ //#5th
					FifthNote = noteData[i];
					break;
				}
			}
		}


		int index = 0;
		OctavePointInSequence = NumNotesInSequence;

		for (int octave = 0; octave < 2; octave++) {
			
			for (int i = 0; i < NumNotesInSequence; i++) {

				int note = noteData [i];

				notes2OctaveData [index] = note;
				index++;
			}
		}

		NumNotesInSequence = NumNotesInSequence * 2;



	}






	private void MakeSubClips (int seqIndex, float startingTime) 
	{
		AudioSource asource = NoteSequences[seqIndex]; 

		AudioClip csource = asource.clip;

		float startTime = startingTime;
		float offsetTime = 2f;
		float duration = 1.7f;
		for(int n = 0; n < 24; n++) {

			float st = startTime + offsetTime * (float)n;

			if (seqIndex == 0) {
				noteClips [n] = MakeSubclip (csource, st, st + duration, n);
			} else if (seqIndex == 1) {
				bassClips [n] = MakeSubclip (csource, st, st + duration, n);			
			}
		}


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
