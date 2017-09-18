//
//  FormData.m
//  GuitarProto
//
//  Created by Dave Hards on 09/10/08.
//  Copyright 2008 Slippery Salmon Software. All rights reserved.
//

#include "GuitarNeck.h"
#include "FormData.h"

namespace HandArcade
{

unsigned int gFormData[] = 
{
	//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Major Scale and Modes
	2,2,1,2,2,2,1,8,0,0,0,0,  0,1,0,0,	//major
	2,1,2,2,2,1,2,8,0,0,0,0,  0,0,0,0,	//dorian
	1,2,2,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//phrygian
	2,2,2,1,2,2,1,8,0,0,0,0,  0,0,0,0,	//lydian
	2,2,1,2,2,1,2,8,0,0,0,0,  0,1,0,0,	//mixolydian
	2,1,2,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//minor
	1,2,2,1,2,2,2,8,0,0,0,0,  0,0,0,0,	//locrian
		
	//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Harmonic Minor Scale and Modes
	2,1,2,2,1,3,1,8,0,0,0,0,  0,1,0,0,	//harmonic minor
	1,2,2,1,3,1,2,8,0,0,0,0,  0,0,0,0,	//hm 2
	2,2,1,3,1,2,1,8,0,0,0,0,  0,0,0,0,	//hm 3
	2,1,3,1,2,1,2,8,0,0,0,0,  0,0,0,0,	//hm 4
	1,3,1,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//hm 5
	3,1,2,1,2,2,1,8,0,0,0,0,  0,0,0,0,	//hm 6
	1,2,1,2,2,1,3,8,0,0,0,0,  0,0,0,0,	//hm 7
		
	//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Melodic Minor Scale and Modes
	2,1,2,2,2,2,1,8,0,0,0,0,  0,1,0,0,	//melodic minor
	1,2,2,2,2,1,2,8,0,0,0,0,  0,0,0,0,	//jm 2
	2,2,2,2,1,2,1,8,0,0,0,0,  0,0,0,0,	//jm 3
	2,2,2,1,2,1,2,8,0,0,0,0,  0,0,0,0,	//jm 4 (overtone dom) 
	2,2,1,2,1,2,2,8,0,0,0,0,  0,0,0,0,	//jm 5
	2,1,2,1,2,2,2,8,0,0,0,0,  0,0,0,0,	//jm 6
	1,2,1,2,2,2,2,8,0,0,0,0,  0,0,0,0,	//jm 7 (altered dom)
		
	//0,8,0,0,0,0,0,0,0,0,0,0,  1,1,0,0,	//Label - Other Scales
	2,2,3,2,3,8,0,0,0,0,0,0,  0,1,0,0,	//maj pentonic
	3,2,2,3,2,8,0,0,0,0,0,0,  0,1,0,0,	//min pentonic
	3,2,1,1,3,2,8,0,0,0,0,0,  0,1,0,0,	//blues
	2,1,2,1,3,1,2,8,0,0,0,0,  0,0,0,0,	//plain mi7b5
	2,2,2,2,2,2,8,0,0,0,0,0,  0,0,0,0,	//whole tone
	1,2,1,2,1,2,1,2,8,0,0,0,  0,0,0,0,	//half whole 
	2,1,2,1,2,1,2,1,8,0,0,0,  0,0,0,0,	//whole half 
	1,3,1,2,2,1,2,8,0,0,0,0,  0,0,0,0,	//13b9 scale (29)
		
		
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


static const char *gFormText[]
{
	//@"Major Scale & Modes",
	"Major",
	"Dorian",
	"Phrygian",
	"Lydian",
	"Mixolydian",
	"Aeolian",
	"Locrian",
    
	//@"Harmonic Minor Scale & Modes",
	"Harmonic Minor",
	"H.M.2",
	"H.M.3",
	"H.M.4",
	"H.M.5",
	"H.M.6",
	"H.M.7",
    
	//@"Melodic Minor Scale & Modes",
    "Melodic Minor",
	"M.M.2",
	"M.M.3",
	"M.M.4",
	"M.M.5",
	"M.M.6",
	"M.M.7 ",
	
	//@"Other Scales",
	"Major Pentonic",
	"Minor Pentonic",
	"Blues",
	"Plain mi7b5",
	"Whole Tone",
	"Half Whole",
	"Whole Half",
	"13b9 scale",
    
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
    
    const char *gKeyNames1[]
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
    
    const char *gKeyNamesSharp[]
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
    const char *gKeyNamesFlat[]
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

    const char *gIntervalText[]
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

    const char *gIntervalExtendedText[]
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
    
    const char *gDisplayStyleText[]
    {
        "#",
        "b",
        "2nd",
        "9th",
    };

char *GetFormNameAtIndex(int index)
{
    return( (char *)gFormText[index] );
}

    
void GetFormIntervalString(int index, char intervalString[256])
{
    int intervalData[16] = {0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0};
    int bucketData[16];
    int numInBucket =  GetKeyNoteBucket(NOTE_E, index, bucketData, intervalData);
    
    sprintf(intervalString, " ");

    char miniBuf[126];
    sprintf(miniBuf, "%s", gIntervalText[intervalData[0]]);
    
    for(int i = 1; i < numInBucket+1; i++)
    {
        
        sprintf(intervalString, "%s %s", intervalString, miniBuf);
        
        
        sprintf(miniBuf, "%s", gIntervalText[intervalData[i]]);

    }
    
    
}


void GetFormData(int *tableData, int formIndex)
{	
	for(int k = 0; k < _maxFormData; k++)
	{
		tableData[k] = gFormData[formIndex * FormQuantum + k];
	}
}

int GetKeyNoteBucket(int key, int formIndex, int *bucketData, int *intervalData)
{
    CCLOG("GetKeyNoteBucket Key = %d", key);

	int formData[_maxFormData];
	GetFormData(formData, formIndex);
    
    

	int _keyNote = key;
	int _intervalSum = 0;

	int _index = 0;

	bucketData[_index++] = _keyNote;
	for(int k = 0; k < _maxFormData; k++)
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
        CCLOG("_keyNote = %d + _interval = %d", _keyNote, _interval);

		_keyNote += _interval;
		if(_keyNote > NOTE_DS)
        {
            CCLOG("......_keyNote > NOTE_DS :: _keyNote = %d : _interval = %d", _keyNote, _interval);
            
			_keyNote = _keyNote - NOTE_E2;
            CCLOG("......_keyNote = %d", _keyNote);
        }
        
        
        CCLOG("Store _keyNote = %d at Index = %d", _keyNote, _index);
        CCLOG(" ");
        bucketData[_index] = _keyNote;

        
        _intervalSum += _interval;
        intervalData[_index++] = _intervalSum;
        
	}

    CCLOG("Note Data:     %d %d %d %d %d %d %d %d %d %d",
              bucketData[0],
              bucketData[1],
              bucketData[2],
              bucketData[3],
              bucketData[4],
              bucketData[5],
              bucketData[6],
              bucketData[7],
              bucketData[8],
              bucketData[9]);

    CCLOG("Interval Data: %d %d %d %d %d %d %d %d %d %d",
          intervalData[0],
          intervalData[1],
          intervalData[2],
          intervalData[3],
          intervalData[4],
          intervalData[5],
          intervalData[6],
          intervalData[7],
          intervalData[8],
          intervalData[9]);

	return _index;
}

void InitForms()
{
	
	InitFormData();
}

void InitFormData()
{
}




}