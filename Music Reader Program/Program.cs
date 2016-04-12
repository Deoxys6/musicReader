//Weston Wingo
//March 4 2016
//Plays music from a file on the onboard speaker
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;
namespace Music_Reader_Program
{
    class Program
    {
        //Row is first, and note, Colum is Octave range and second
                                                  //37 because console.beep cannot be below 37
        static int[ , ] notesAndFrequencyArray = {{37, 65,  130, 261, 523, 1046, 2093, 4186},    //Note C   0
                                                  {37, 69,  138, 227, 554, 1108, 2217, 4434},    //Note C#  1
                                                  {37, 73,  146, 293, 587, 1174, 2349, 4698},    //Note D   2
                                                  {38, 77,  155, 311, 622, 1244, 2489, 4978},    //Note D#  3
                                                  {41, 82,  164, 329, 659, 1318, 2637, 5274},    //Note E   4
                                                  {43, 87,  174, 349, 698, 1396, 2793, 5587},    //Note F   5
                                                  {46, 92,  185, 369, 739, 1479, 2959, 5919},    //Note F#  6
                                                  {49, 98,  196, 392, 783, 1567, 3135, 6271},    //Note G   7
                                                  {51, 103, 207, 415, 830, 1661, 3322, 6644},    //Note G#  8
                                                  {55, 110, 220, 440, 880, 1760, 3520, 7040},    //Note A   9
                                                  {58, 116, 233, 466, 932, 1864, 3729, 7458},    //Note A#  10
                                                  {61, 123, 246, 493, 987, 1975, 3951, 7902}};   //Note B   11
        //This is the duration of the measure
        static int durationOfAMeasure = 1800;
        static void Main(string[] args)
        {
            String filePath = null;
            Console.WriteLine("Enter the file path");
            filePath = Console.ReadLine().Trim();
            if (filePath == "")
            {
               filePath = Path.Combine("..", "..", "Music", "BONETROUSLE.txt");
            }

            //Lets the user change the txt file being played 

            
            Thread violaThread = new Thread(()=>playInstrument(filePath));
            violaThread.Start();
            //Thread violinThread = new Thread(()=>playInstrument("Music Reader\\Violin.txt"));
            //violinThread.Start();
        }

        public static void playInstrument(String filePath)
        {
            int noteType = 13;
            int octaveRange = 13;
            //Make its own method for reading the file and converting the things
            //Read the File for the translated Music 
            FileStream outFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(outFile);
            //Array for the contents of the file per line
            String line;
            List<String> contentsOfFile = new List<String>();
            //Find the amount of lines in the file being read
            while((line = reader.ReadLine()) != null)
            {
                contentsOfFile.Add(line.ToUpper());
            }
            //The array that is actuall going to be played
            int[ , ] theActualArrayThatIsNeededToBePlayed = new int[2,contentsOfFile.Count];
            Char[] theDurationArrayAsChar = new Char[contentsOfFile.Count];
            //Declare the array for the split noteWithOctave and Duration 
            Int32[] noteTypeArray = new Int32[contentsOfFile.Count];

            for (int x = 0; x < contentsOfFile.Count; x++)
            {
                //This is the index for the note
                //Declare the array that will be used temporarily for the splitting
                String[] localNoteTypeAndFrequencyArray = contentsOfFile[x].Split(' ');
                //This splits the duration into a char array index 0 is the numerator, index 1 is the slash(division sighn), index 2 is the denominator
                theDurationArrayAsChar = localNoteTypeAndFrequencyArray[1].ToCharArray();
                //This is the list of the note and it's octave range
                Char[] onlyTheNotesAndTheirOctaveRange = localNoteTypeAndFrequencyArray[0].ToCharArray();

                //This is the switch case for the first Index, it is the Note Type
                switch (onlyTheNotesAndTheirOctaveRange[0])
                {
                    case 'A':
                        if (onlyTheNotesAndTheirOctaveRange[1] == '#')
                            noteType = 10;
                        else
                            noteType = 9;
                        break;
                    case 'B':
                        if (onlyTheNotesAndTheirOctaveRange[1] == 'R')
                            noteType = 12;
                        else
                            noteType = 11;
                        break;
                    case 'C':
                        if (onlyTheNotesAndTheirOctaveRange[1] == '#')
                            noteType = 1;
                        else
                            noteType = 0;
                        break;
                    case 'D':
                        if (onlyTheNotesAndTheirOctaveRange[1] == '#')
                            noteType = 3;
                        else
                            noteType = 2;
                        break;
                    case 'E':
                        noteType = 4;
                        break;
                    case 'F':
                        if (onlyTheNotesAndTheirOctaveRange[1] == '#')
                            noteType = 6;
                        else
                            noteType = 5;
                        break;
                    case 'G':
                        if (onlyTheNotesAndTheirOctaveRange[1] == '#')
                            noteType = 8;
                        else
                            noteType = 7;
                        break;
                    default:
                        break;
                }
                //Finds what octave range the note is in
                switch (onlyTheNotesAndTheirOctaveRange[1])
                {
                    case '1':
                        octaveRange = 0;
                        break;
                    case '2':
                        octaveRange = 1;
                        break;
                    case '3':
                        octaveRange = 2;
                        break;
                    case '4':
                        octaveRange = 3;
                        break;
                    case '5':
                        octaveRange = 4;
                        break;
                    case '6':
                        octaveRange = 5;
                        break;
                    case '7':
                        octaveRange = 6;
                        break;
                    case '8':
                        octaveRange = 7;
                        break;
                    //this will handle the exception of the '#'
                    default:
                        break;
                }

                //this is the try catch to make sure the char at 2 does not exist
                try
                {
                    switch (onlyTheNotesAndTheirOctaveRange[2])
                    {
                        case '1':
                            octaveRange = 0;
                            break;
                        case '2':
                            octaveRange = 1;
                            break;
                        case '3':
                            octaveRange = 2;
                            break;
                        case '4':
                            octaveRange = 3;
                            break;
                        case '5':
                            octaveRange = 4;
                            break;
                        case '6':
                            octaveRange = 5;
                            break;
                        case '7':
                            octaveRange = 6;
                            break;
                        case '8':
                            octaveRange = 12;
                            break;
                        //this will handle the exception of the '#'
                        default:
                            break;
                    }
                }
                catch
                {
                }

                double duration;
                //need to split the duration string into a char array so I can get the values
                //this try catch statement is to allow 32nd notes 
                try
                {
                    duration = ((double)Int32.Parse(theDurationArrayAsChar[0].ToString())) / ((((double)Int32.Parse(theDurationArrayAsChar[2].ToString()) + 10) + ((double)Int32.Parse(theDurationArrayAsChar[3].ToString()))));
                }
                catch
                {
                }

                duration = (((double)Int32.Parse(theDurationArrayAsChar[0].ToString())) / ((double)Int32.Parse(theDurationArrayAsChar[2].ToString())));   
                
                theActualArrayThatIsNeededToBePlayed[1, x] = (int)((durationOfAMeasure * duration));
                if (noteType == 12)
                {
                    theActualArrayThatIsNeededToBePlayed[0, x] = 0;
                }
                else
                {
                        //Assign the frequency of the note
                        theActualArrayThatIsNeededToBePlayed[0, x] = notesAndFrequencyArray[noteType, octaveRange];
                }
            }
            while (true)
            {
                for (int x = 0; x < contentsOfFile.Count; x++)
                {
                    if (theActualArrayThatIsNeededToBePlayed[0, x] == 0)
                    {
                        Thread.Sleep(theActualArrayThatIsNeededToBePlayed[1, x]);
                    }
                    else
                        Console.Beep(theActualArrayThatIsNeededToBePlayed[0, x], theActualArrayThatIsNeededToBePlayed[1, x]);
                    //This actually plays the music
                    Console.WriteLine("Note: " + theActualArrayThatIsNeededToBePlayed[0, x]);
                    Console.WriteLine("Duration (in milliseconds): " + theActualArrayThatIsNeededToBePlayed[1, x]);
                }
            }
        }
    }
    
}