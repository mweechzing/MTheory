using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
	public enum _notes
	{
		NOTE_E = 0,
		NOTE_F,         //1
		NOTE_FS,        //2
		NOTE_G,         //3
		NOTE_GS,        //4
		NOTE_A,         //5
		NOTE_AS,        //6
		NOTE_B,         //7
		NOTE_C,         //8
		NOTE_CS,        //9
		NOTE_D,         //10
		NOTE_DS,        //11
		NOTE_E2,        //12

	};

	public enum _arpeggioStyle
	{
		Accending = 0,
		Decending,         
		AccendDecend       
	};


	public const int MaxFrets = 25;
	public const int MaxStrings = 6;
	public const int MaxLevels = 8;
	public const int _maxFormData = 16;
	public const int _maxForms = 128;
	public const int FormQuantum = 16;
	public const int KeyNoteBucket = 16;
	public const int OctaveLen = 12;
}

public class FormData : MonoBehaviour 
{



	[HideInInspector]
	public int[] gFormData = new int[]
	{
		//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Major Scale and Modes
		2,2,1,2,2,2,1,8,0,0,0,0,  0,1,0,0,	//major
		2,1,2,2,2,1,2,8,0,0,0,0,  0,0,0,0,	//dorian
		1,2,2,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//phrygian
		2,2,2,1,2,2,1,8,0,0,0,0,  0,0,0,0,	//lydian
		2,2,1,2,2,1,2,8,0,0,0,0,  0,1,0,0,	//mixolydian
		2,1,2,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//minor
		1,2,2,1,2,2,2,8,0,0,0,0,  0,0,0,0,	//locrian

		1,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,	//space

		//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Harmonic Minor Scale and Modes
		2,1,2,2,1,3,1,8,0,0,0,0,  0,1,0,0,	//harmonic minor
		1,2,2,1,3,1,2,8,0,0,0,0,  0,0,0,0,	//hm 2
		2,2,1,3,1,2,1,8,0,0,0,0,  0,0,0,0,	//hm 3
		2,1,3,1,2,1,2,8,0,0,0,0,  0,0,0,0,	//hm 4
		1,3,1,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//hm 5
		3,1,2,1,2,2,1,8,0,0,0,0,  0,0,0,0,	//hm 6
		1,2,1,2,2,1,3,8,0,0,0,0,  0,0,0,0,	//hm 7

		2,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,	//space

		//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Melodic Minor Scale and Modes
		2,1,2,2,2,2,1,8,0,0,0,0,  0,1,0,0,	//melodic minor
		1,2,2,2,2,1,2,8,0,0,0,0,  0,0,0,0,	//jm 2
		2,2,2,2,1,2,1,8,0,0,0,0,  0,0,0,0,	//jm 3
		2,2,2,1,2,1,2,8,0,0,0,0,  0,0,0,0,	//jm 4 (overtone dom) 
		2,2,1,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//jm 5
		2,1,2,1,2,2,2,8,0,0,0,0,  0,0,0,0,	//jm 6
		1,2,1,2,2,2,2,8,0,0,0,0,  0,0,0,0,	//jm 7 (altered dom)

		3,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,	//space

		//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Other Scales
		2,2,3,2,3,8,0,0,0,0,0,0,  0,1,0,0,	//maj pentonic
		3,2,2,3,2,8,0,0,0,0,0,0,  0,1,0,0,	//min pentonic
		3,2,1,1,3,2,8,0,0,0,0,0,  0,1,0,0,	//blues
		2,1,2,1,3,1,2,8,0,0,0,0,  0,0,0,0,	//plain mi7b5
		2,2,2,2,2,2,8,0,0,0,0,0,  0,0,0,0,	//whole tone
		1,2,1,2,1,2,1,2,8,0,0,0,  0,0,0,0,	//half whole 
		2,1,2,1,2,1,2,1,8,0,0,0,  0,0,0,0,	//whole half 
		1,3,1,2,2,1,2,8,0,0,0,0,  0,0,0,0,	//13b9 scale (29)

		4,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,	//space

		//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Major Arpeggios
		4,3,5,8,0,0,0,0,0,0,0,0,  0,1,0,0,	//maj
		4,3,4,1,8,0,0,0,0,0,0,0,  0,0,0,0,	//maj 7
		4,3,2,3,8,0,0,0,0,0,0,0,  0,0,0,0,	//maj 6
		2,2,3,2,3,8,0,0,0,0,0,0,  0,0,0,0,	//maj 6/9
		2,2,3,4,1,8,0,0,0,0,0,0,  0,0,0,0,	//maj 9
		2,2,2,1,4,1,8,0,0,0,0,0,  0,0,0,0,	//maj 9#11
		2,2,3,2,3,8,0,0,0,0,0,0,  0,0,0,0,	//maj 13
		2,2,2,1,2,1,8,0,0,0,0,0,  0,0,0,0,	//maj 13#11
		2,2,2,1,2,3,8,0,0,0,0,0,  0,0,0,0,	//maj 6/9#11
		2,2,2,5,1,8,0,0,0,0,0,0,  0,0,0,0,	//maj 9b5
		4,2,5,1,8,0,0,0,0,0,0,0,  0,0,0,0,	//maj 7b5
		2,2,2,3,2,1,8,0,0,0,0,0,  0,0,0,0,	//maj 13b5
		2,2,3,5,8,0,0,0,0,0,0,0,  0,0,0,0,	//maj add 9
		4,4,3,1,8,0,0,0,0,0,0,0,  0,0,0,0,	//majmin7
		3,4,4,1,8,0,0,0,0,0,0,0,  0,0,0,0,	//maj 7#5 (44)

		5,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,	//space

		//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Minor Arpeggios
		3,4,5,8,0,0,0,0,0,0,0,0,  0,1,0,0,	//minor
		3,4,2,3,8,0,0,0,0,0,0,0,  0,0,0,0,	//min 6
		2,1,4,2,3,8,0,0,0,0,0,0,  0,0,0,0,	//min 6/9
		3,4,2,1,2,8,0,0,0,0,0,0,  0,0,0,0,	//min 6/7
		2,1,4,2,1,2,8,0,0,0,0,0,  0,0,0,0,	//min 6/9/7
		2,1,3,1,2,3,8,0,0,0,0,0,  0,0,0,0,	//min 6/9/#11
		3,2,2,2,3,8,0,0,0,0,0,0,  0,0,0,0,	//min 6/11
		3,4,3,2,8,0,0,0,0,0,0,0,  0,0,0,0,	//min 7
		3,2,2,3,2,8,0,0,0,0,0,0,  0,0,0,0,	//min 7/11
		2,1,4,3,2,8,0,0,0,0,0,0,  0,0,0,0,	//min 9
		2,1,2,2,3,2,8,0,0,0,0,0,  0,0,0,0,	//min 11
		2,1,4,5,8,0,0,0,0,0,0,0,  0,0,0,0,	//min add 9
		3,3,4,2,8,0,0,0,0,0,0,0,  0,0,0,0,	//min 7b5
		3,2,1,4,2,8,0,0,0,0,0,0,  0,0,0,0,	//min 7b5/11
		3,5,2,2,8,0,0,0,0,0,0,0,  0,0,0,0,	//min 7#5 (59)

		6,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,	//space

		//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Dominate Arpeggios
		4,3,3,2,8,0,0,0,0,0,0,0,  0,1,0,0,	//7
		4,3,2,1,2,8,0,0,0,0,0,0,  0,0,0,0,	//7/6
		2,2,3,3,2,8,0,0,0,0,0,0,  0,0,0,0,	//9
		2,2,1,2,2,1,2,8,0,0,0,0,  0,0,0,0,	//13
		5,2,3,2,8,0,0,0,0,0,0,0,  0,0,0,0,	//7sus
		5,2,2,1,2,8,0,0,0,0,0,0,  0,0,0,0,	//7/6sus
		2,2,1,2,3,2,8,0,0,0,0,0,  0,0,0,0,	//11
		2,3,2,2,1,2,8,0,0,0,0,0,  0,0,0,0,	//13sus (67)
		4,2,4,2,8,0,0,0,0,0,0,0,  0,0,0,0,	//7b5
		4,4,2,2,8,0,0,0,0,0,0,0,  0,0,0,0,	//7#5
		1,3,3,3,2,8,0,0,0,0,0,0,  0,0,0,0,	//7b9
		3,1,3,3,2,8,0,0,0,0,0,0,  0,0,0,0,	//7#9
		2,2,2,4,2,8,0,0,0,0,0,0,  0,0,0,0,	//9b5
		2,2,4,2,2,8,0,0,0,0,0,0,  0,0,0,0,	//9#5
		1,3,2,4,2,8,0,0,0,0,0,0,  0,0,0,0,	//7b9b5
		1,3,4,2,2,8,0,0,0,0,0,0,  0,0,0,0,	//7b9#5
		3,1,2,4,2,8,0,0,0,0,0,0,  0,0,0,0,	//7#9b5
		3,1,4,2,2,8,0,0,0,0,0,0,  0,0,0,0,	//7#9#5 (77)
		1,3,3,2,1,2,8,0,0,0,0,0,  0,0,0,0,	//13b9
		3,1,3,2,1,2,8,0,0,0,0,0,  0,0,0,0,	//13#9
		1,3,2,3,1,2,8,0,0,0,0,0,  0,0,0,0,	//13b9b5
		2,2,2,1,2,1,2,8,0,0,0,0,  0,0,0,0,	//13#11
		2,2,2,1,3,2,8,0,0,0,0,0,  0,0,0,0,	//#11
		1,3,1,2,3,2,8,0,0,0,0,0,  0,0,0,0,	//11b9
		1,3,1,3,2,2,8,0,0,0,0,0,  0,0,0,0,	//11b9#5
		4,1,2,3,2,8,0,0,0,0,0,0,  0,0,0,0,	//7/11
		4,1,2,2,1,2,8,0,0,0,0,0,  0,0,0,0,	//7/6/11
		3,3,3,3,8,0,0,0,0,0,0,0,  0,0,0,0,	//dim7
		4,4,4,8,0,0,0,0,0,0,0,0,  0,0,0,0,	//aug (88)

		7,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,	//space

		3,4,5,8,0,0,0,0,0,0,0,0,  0,0,0,0,//3 4 5  Peruvian tritonic 2 	
		3,7,2,8,0,0,0,0,0,0,0,0,  0,0,0,0,//3 7 2  Ute tritonic
		4,3,5,8,0,0,0,0,0,0,0,0,  0,0,0,0,//4 3 5  Peruvian tritonic 1 
		5,2,5,8,0,0,0,0,0,0,0,0,  0,0,0,0,//5 2 5  Warao tritonic
		5,5,2,8,0,0,0,0,0,0,0,0,  0,0,0,0,//5 5 2  Sansagari
		6,1,5,8,0,0,0,0,0,0,0,0,  0,0,0,0,//6 1 5  Raga Ongkari

		1,5,1,5,8,0,0,0,0,0,0,0,  0,0,0,0,//1 5 1 5  Messiaen truncated mode 5
		5,1,5,1,8,0,0,0,0,0,0,0,  0,0,0,0,//5 1 5 1  Messiaen truncated mode 5 inverse
		2,4,2,4,8,0,0,0,0,0,0,0,  0,0,0,0,//2 4 2 4  Messiaen truncated mode 6
		4,2,4,2,8,0,0,0,0,0,0,0,  0,0,0,0,//4 2 4 2  Messiaen truncated mode 6 inverse
		1,4,3,4,8,0,0,0,0,0,0,0,  0,0,0,0,//1 4 3 4  Raga Lavangi 
		2,1,7,2,8,0,0,0,0,0,0,0,  0,0,0,0,//2 1 7 2  Warao tetratonic
		2,2,3,5,8,0,0,0,0,0,0,0,  0,0,0,0,//2 2 3 5  Eskimo tetratonic (Alaska: Bethel)
		2,3,2,5,8,0,0,0,0,0,0,0,  0,0,0,0,//2 3 2 5  Genus primum
		5,2,3,2,8,0,0,0,0,0,0,0,  0,0,0,0,//2 3 2 5  Genus primum inverse
		2,3,4,3,8,0,0,0,0,0,0,0,  0,0,0,0,//2 3 4 3  Raga Bhavani
		2,4,5,1,8,0,0,0,0,0,0,0,  0,0,0,0,//2 4 5 1  Raga Sumukam 
		4,3,3,2,8,0,0,0,0,0,0,0,  0,0,0,0,//4 3 3 2  Raga Mahathi
		3,4,3,2,8,0,0,0,0,0,0,0,  0,0,0,0,//3 4 3 2  Bi Yu: China

		2,3,2,1,4,8,0,0,0,0,0,0,  0,0,0,0,//2 3 2 1 4  Han-kumoi: Japan, Raga Shobhav
		2,1,4,1,4,8,0,0,0,0,0,0,  0,0,0,0,//2 1 4 1 4  Hira-joshi, Kata-kumoi: Japan 
		1,4,2,3,2,8,0,0,0,0,0,0,  0,0,0,0,//1 4 2 1 4  Hon-kumoi-joshi, Sakura, Akebo
		1,4,2,3,2,8,0,0,0,0,0,0,  0,0,0,0,//1 4 2 3 2  Kokin-joshi, Miyakobushi, Han-
		1,4,1,4,2,8,0,0,0,0,0,0,  0,0,0,0,//1 4 1 4 2  Iwato: Japan  
		2,3,2,2,3,8,0,0,0,0,0,0,  0,0,0,0,//2 3 2 2 3  Ritusen, Ritsu (Gagaku): Japan
		2,2,3,2,3,8,0,0,0,0,0,0,  0,0,0,0,//2 2 3 2 3  Major Pentatonic, Ryosen: Japa
		2,3,2,3,2,8,0,0,0,0,0,0,  0,0,0,0,//2 3 2 3 2  Yo: Japan, Suspended Pentatoni
		2,3,3,2,2,8,0,0,0,0,0,0,  0,0,0,0,//2 3 3 2 2  Chaio: China  
		2,2,2,3,3,8,0,0,0,0,0,0,  0,0,0,0,//2 2 2 3 3  Kung: China  
		1,4,2,2,3,8,0,0,0,0,0,0,  0,0,0,0,//1 4 2 2 3  Altered Pentatonic, Raga Manar
		2,1,2,4,3,8,0,0,0,0,0,0,  0,0,0,0,//2 1 2 4 3  Raga Abhogi  
		4,2,1,4,1,8,0,0,0,0,0,0,  0,0,0,0,//4 2 1 4 1  Raga Amritavarshini, Malashri,
		4,2,1,4,1,8,0,0,0,0,0,0,  0,0,0,0,//2 1 2 3 4  Raga Audav Tukhari  
		4,1,4,2,1,8,0,0,0,0,0,0,  0,0,0,0,//4 1 4 2 1  Raga Bhinna Shadja, Kaushikdhv
		1,2,4,1,4,8,0,0,0,0,0,0,  0,0,0,0,//1 2 4 1 4  Balinese Pelog, Raga Bhupalam,

		//2 3 2 5  Genus primum
		2,3,2,5,8,0,0,0,0,0,0,0,  0,0,0,0,	//Genus primum

		//1 1 1 2 2 1 3 1  Harmonic and Neapolitan Minor mixed  
		1,1,1,2,2,1,3,1,8,0,0,0,  0,0,0,0,

		//2 3 1 1 3 1 1  Chromatic Mixolydian inverse  
		2,3,1,1,3,1,1,8,0,0,0,0,  0,0,0,0,

		//2 1 4 2 2 1  Hawaiian 
		2,1,4,2,2,1,8,0,0,0,0,0,  0,0,0,0,

		//1 1 2 1 1 1 1 2 1 1  Symmetrical Decatonic  
		1,1,2,1,1,1,1,2,1,1,8,0,  0,0,0,0,

		//2 1 2 1 3 2 1  Jeths' mode
		2,1,4,1,3,2,1,8,0,0,0,0,  0,0,0,0,

		//Egyptian                      1245b7        23232*    22233
		2,3,2,3,2,8,0,0,0,0,0,0,  0,0,0,0,

		//Chinese                       13#457        42141     11244
		4,2,1,4,1,8,0,0,0,0,0,0,  0,0,0,0,

	};


	[HideInInspector]
	public string[] gFormText = new string[]
	{
		//@"Major Scale & Modes",
		"Major",
		"Dorian",
		"Phrygian",
		"Lydian",
		"Mixolydian",
		"Aeolian",
		"Locrian",

		"space",

		//@"Harmonic Minor Scale & Modes",
		"Harmonic Minor",
		"H.M.2",
		"H.M.3",
		"H.M.4",
		"H.M.5",
		"H.M.6",
		"H.M.7",

		"space",

		//@"Melodic Minor Scale & Modes",
		"Melodic Minor",
		"M.M.2",
		"M.M.3",
		"M.M.4",
		"M.M.5",
		"M.M.6",
		"M.M.7 ",

		"space",

		//@"Other Scales",
		"Major Pentonic",
		"Minor Pentonic",
		"Blues",
		"Plain mi7b5",
		"Whole Tone",
		"Half Whole",
		"Whole Half",
		"13b9 scale",

		"space",

		//@"Major Arpeggios",
		"Maj",
		"Maj 7",
		"Maj 6",
		"Maj 6/9",
		"Maj 9",
		"Maj 9#11",
		"Maj 13",
		"Maj 13#11",
		"Maj 6/9#11",
		"Maj 9b5",
		"Maj 7b5",
		"Maj 13b5",
		"Maj add 9",
		"Maj 7#5",
		"MaMi 7",

		"space",

		//@"Minor Arpeggios",
		"Min",
		"Min 6",
		"Min 6/9",
		"Min 6/7",
		"Min 6/9/7",
		"Min 6/9/#11",
		"Min 6/11",
		"Min 7",
		"Min 7/11",
		"Min 9",
		"Min 11",
		"Min add 9",
		"Min 7b5",
		"Min 7b5/11",
		"Min 7#5",

		"space",

		//@"Dominate 7th Arpeggios",
		"Dominate 7",
		"Dom 7/6",
		"Dom 9",
		"Dom 13",
		"Dom 7sus",
		"Dom 7/6sus",
		"Dom 11",
		"Dom 13sus",
		"Dom 7b5",
		"Dom 7#5",
		"Dom 7b9",
		"Dom 7#9",
		"Dom 9b5",
		"Dom 9#5",
		"Dom 7b9b5",
		"Dom 7b9#5",
		"Dom 7#9b5",
		"Dom 7#9#5",
		"Dom 13b9",
		"Dom 13#9",
		"Dom 13b9b5",
		"Dom 13#11",
		"Dom #11",
		"Dom 11b9",
		"Dom 11b9#5",
		"Dom 7/11",
		"Dom 7/5/11",
		"dim 7",
		"aug",

		"space",


		//@"Exotic 3 note Scales",
		"Peruvian tritonic 2",
		"Ute tritonic",
		"Peruvian tritonic 1",
		"Warao tritonic",
		"Sansagari",
		"Raga Ongkari",

		//@"Exotic 4 note Scales",
		"Messiaen trunc mode 5",
		"Messiaen trunc mode 5 inv",
		"Messiaen trun mode 6",
		"Messiaen trunc mode 6 inv",
		"Raga Lavangi",
		"Warao tetratonic",
		"Eskimo tetratonic",
		"Genus primum",
		"Genus primum inv",
		"Raga Bhavani",
		"Raga Sumukam",
		"Raga Mahathi",
		"Bi Yu: China",



		//@"Exotic 5 note Scales"}

		//5 note
		"Han-kumoi: Japan",
		"Kata-kumoi: Japan",
		"Hon-kumoi-joshi",
		"Kokin-joshi",
		"Iwato: Japan",
		"Ritsu (Gagaku): Japan",
		"Ryosen: Japan",
		"Suspended Pentatonic",
		"Chaio: China",
		"Kung: China",
		"Altered Pentatonic",
		"Raga Abhogi",
		"Raga Amritavarshini",
		"Raga Audav Tukhari",
		"Raga Bhinna Shadja",
		"ERaga Bhupalam",



		"Genus primum",
		"Harmonic and Neapolitan Minor",
		"Chromatic Mixolydian inverse",
		"Hawaiian",
		"Symmetrical Decatonic",
		"Jeths' mode",
		"Egyptian",
		"Chinese",


	};

	[HideInInspector]
	public string[] gKeyNames1 = new string[]
	{
		"E",
		"F",
		"F#",
		"G",
		"G#",
		"A",
		"A#",
		"B",
		"C",
		"C#",
		"D",
		"D#",
		"E",
		"x",
		"y",
		"z",

	};

	[HideInInspector]
	public string[] gKeyNamesSharp = new string[]
	{
		"E",
		"F",
		"F#",
		"G",
		"G#",
		"A",
		"A#",
		"B",
		"C",
		"C#",
		"D",
		"D#",
		"E",
		"x",
		"y",
		"z",

	};

	[HideInInspector]
	public string[] gKeyNamesFlat = new string[]
	{
		"E",
		"F",
		"Gb",
		"G",
		"Ab",
		"A",
		"Bb",
		"B",
		"C",
		"Db",
		"D",
		"Eb",
		"E",
		"x",
		"y",
		"z",

	};

	[HideInInspector]
	public string[] gIntervalText = new string[]
	{
		"R",
		"b2",
		"2",
		"b3",
		"3",
		"4",
		"b5",
		"5",
		"#5",
		"6",
		"b7",
		"7",
		"8",
		"x",
		"y",
		"z",
	};

	[HideInInspector]
	public string[] gIntervalExtendedText = new string[]
	{
		"R",
		"b9",
		"9",
		"b3",
		"3",
		"11",
		"b5",
		"5",
		"#5",
		"13",
		"b7",
		"7",
		"8",
		"x",
		"y",
		"z",
	};

	[HideInInspector]
	public string[] gDisplayStyleText = new string[]
	{
		"#",
		"b",
		"2nd",
		"9th",
	};

	[HideInInspector]
	public string[] gNoteGraphicStyleText = new string[]
	{
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
	};

	[HideInInspector]
	public string[] gSampleText = new string[]
	{
		"Prophet",
		"Piano",
		"Guitar",
		"Bass",
	};

	[HideInInspector]
	public string[] gDroneText = new string[]
	{
		"Fog",
		"Rain",
		"Mist",
		"Wind",
	};


	[HideInInspector]
	public string[] gArpeggioStyleText = new string[]
	{
		"Accending",
		"Decending",
		"Accend/Decend",
	};
		
	[HideInInspector]
	public string[] gPedalToneText = new string[]
	{
		"Root Only",
		"Root + 5th",
		"Root + 3rd",
		"Root + 3rd + 5th",
	};










	public string GetFormNameAtIndex(int index)
	{
		return( gFormText[index] );
	}



	struct FormEntry {
		int entryType;
		int[] data;
		string name;
	};

	struct FormList {
		int numFormsLoaded;
		FormEntry[] formList;
	};

	private void GetFormData(ref int[] tableData, int formIndex)
	{	
		for(int k = 0; k < Globals._maxFormData; k++)
		{
			tableData[k] = gFormData[formIndex * Globals.FormQuantum + k];
		}
	}

	public string GetFormIntervalString(int index)
	{
		string intervalString = "";

		int[] intervalData = new int[Globals._maxFormData] {0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0}; 

		int[] bucketData = new int[Globals._maxFormData];

		int numInBucket =  GetKeyNoteBucket((int)Globals._notes.NOTE_E, index, ref bucketData, ref intervalData);

		string miniBuf = gIntervalText[intervalData[0]];
		for(int i = 1; i < numInBucket+1; i++)
		{
			intervalString += " " + miniBuf;
			miniBuf = gIntervalText[intervalData[i]];
		}

		return intervalString;

	}

	public int GetKeyNoteBucket(int key, int formIndex, ref int[] bucketData, ref int[] intervalData)
	{
		//Debug.Log("GetKeyNoteBucket Key = " + key);
		int[] formData = new int[Globals._maxFormData]; 
		GetFormData(ref formData, formIndex);

		int _keyNote = key;
		int _intervalSum = 0;

		int _index = 0;

		bucketData[_index++] = _keyNote;
		for(int k = 0; k < Globals._maxFormData; k++)
		{
			int _interval = formData[k];

			if(_interval >= 8)
			{
				if(_index > 1)
				{
					_index--;
					bucketData[_index] = 0;
					break;
				}
			}
			//Debug.Log("_keyNote = " + _keyNote + " _interval = " + _interval);

			_keyNote += _interval;
			if(_keyNote > (int)Globals._notes.NOTE_DS)
			{
				//Debug.Log("......_keyNote > NOTE_DS :: _keyNote = " + _keyNote + " _interval = " + _interval);

				_keyNote = _keyNote - (int)Globals._notes.NOTE_E2;
				//Debug.Log("......_keyNote = " + _keyNote);
			}


			//Debug.Log("Store _keyNote = " + _keyNote + " _index = " + _index);
			bucketData[_index] = _keyNote;


			_intervalSum += _interval;
			intervalData[_index++] = _intervalSum;

		}

		return _index;
	}
		

	public static FormData Instance;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{

		int[] tdat = new int[Globals._maxFormData]; 
		GetFormData(ref tdat, 0);

		//Debug.Log(tdat[0].ToString() + tdat[1] + tdat[2] + tdat[3] + tdat[4] + tdat[5] + tdat[6] + tdat[7] + tdat[8] + tdat[9]); 


		string intervalText = GetFormIntervalString(0);
		//Debug.Log(intervalText);
	}
	
}
