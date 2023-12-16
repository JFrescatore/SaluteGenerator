using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VoiceLibrary
{
    [System.Serializable]
    public class VoiceProfile
    {
        [HideInInspector] public string profile;
        [HideInInspector] public List<string> callsigns;
        [HideInInspector] public List<string> callsign_Transcript;
        [HideInInspector] public List<string> over;
        [HideInInspector] public List<string> thisIs;
        [HideInInspector] public List<string> radioCheck;
        [HideInInspector] public List<string> readBack;
        [HideInInspector] public List<string> roger;
        [HideInInspector] public List<string> line1;
        [HideInInspector] public List<string> line1_Transcript;
        [HideInInspector] public List<string> line2;
        [HideInInspector] public List<string> line2_Transcript;
        [HideInInspector] public List<string> line3;
        [HideInInspector] public List<string> line3_Transcript;
        [HideInInspector] public List<string> line4;
        [HideInInspector] public List<string> line4_Transcript;
        [HideInInspector] public List<string> line5;
        [HideInInspector] public List<string> line5_Transcript;
        [HideInInspector] public List<string> line6;
        [HideInInspector] public List<string> line6_Transcript;

        public List<int> callsign;
        public List<int> unit_Data;
        public List<string> audio_Files;
        public List<string> transcripts;
        public List<AudioClip> radio;
        public List<AudioClip> prepareCopy;
        public List<AudioClip> salute;
        public List<AudioClip> final;

        public void BuildSequence()
        {
            callsign.Clear();
            unit_Data.Clear();
            audio_Files.Clear();
            transcripts.Clear();
            radio.Clear();
            prepareCopy.Clear();
            salute.Clear();
            final.Clear();
            BuildChatter();
            BuildSalute();
            BuildReadBack();
        }

        void BuildChatter()
        {
            int me_Callsign = Random.Range(0, 3);
            int you_Callsign = Random.Range(4, 7);

            string audio_Files = "" + callsigns[you_Callsign] + "\n" + thisIs[0] + "\n"
                + callsigns[me_Callsign] + "\n" + radioCheck[0];
            string transcript = "" + callsign_Transcript[you_Callsign] + ", This Is " + callsign_Transcript[me_Callsign] + ". Radio Check. Over.";
            string[] split = audio_Files.Split('\n');
            foreach(string s in split)
                radio.Add(Resources.Load<AudioClip>(s));
            this.audio_Files.Add(audio_Files);
            transcripts.Add(transcript);
            callsign.Add(you_Callsign);
            callsign.Add(me_Callsign);

            audio_Files = "" + roger[0] +"\n" + over[0];
            split = audio_Files.Split('\n');
            foreach (string s in split)
                prepareCopy.Add(Resources.Load<AudioClip>(s));
            transcripts.Add("Roger. Prepare to Copy. Over");
            this.audio_Files.Add(audio_Files);
        }

        void BuildSalute()
        {
            int line1 = Random.Range(0, line1_Transcript.Count);
            int line2 = (line1 == 6) ? 0 : Random.Range(1, line2_Transcript.Count);
            int line3 = Random.Range(0, line3_Transcript.Count);
            int line4 = Random.Range(0, 1);
            int line5 = Random.Range(0, line5_Transcript.Count);

            string audio_Files = this.line1[line1] + "\n" + this.line2[line2] + "\n" + this.line3[line3] + "\n" + this.line4[line4]
                + "\n" + this.line5[line5] + "\n" + line6[line1] + "\n" + over[0];
            string transcript = line1_Transcript[line1] + "\n" + line2_Transcript[line2] + "\n" + line3_Transcript[line3] 
                + "\n" + line4_Transcript[line4] + "\n" + line5_Transcript[line5] + "\n" + line6_Transcript[line1] + " Over.";
            string[] split = audio_Files.Split('\n');
            foreach (string s in split)
                salute.Add(Resources.Load<AudioClip>(s));
            this.audio_Files.Add(audio_Files);
            transcripts.Add(transcript);

            int unit = 0;
            int ech = 0;
            if (line1 == 3)
                unit = 2;
            else if (line1 == 2)
                unit = 1;
            else if (line1 == 6)
                unit = 3;
            else if(line1 == 0)
                ech = 1;

            unit_Data.Add(unit);
            unit_Data.Add(ech);
        }

        void BuildReadBack()
        {
            string audio_Files = "" + callsigns[callsign[0]] + "\n" + readBack[0] + "\n" + over[0];
            string transcript = "" + callsign_Transcript[callsign[0]] + ". Read back. Over";
            string[] split = audio_Files.Split("\n");
            foreach (string s in split)
                final.Add(Resources.Load<AudioClip>(s));
            this.audio_Files.Add(audio_Files);
            transcripts.Add(transcript);
        }
        
    }
}