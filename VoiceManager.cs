using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoiceLibrary;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
class Transcript_Legend
{
    public string file_Name;
    public List<string> callsign_AI;
    public List<string> callsign_Student;
    public List<string> chatter;
    public List<string> line1_Transcript;
    public List<string> line2_Transcript;
    public List<string> line3_Transcript;
    public List<string> line4_Transcript;
    public List<string> line5_Transcript;
    public List<string> line6_Transcript;

    string RemoveSpace(string s)
    {
        string new_S = "";
        foreach (string t in s.Split(' '))
            new_S += t;
        return new_S;
    }

    public string GetChatterFIle(string saying)
    {
        string chat_File = "";
        foreach(string s in chatter)
        {
            if(s == saying)
            {
                chat_File = RemoveSpace(s);
                break;
            }
        }

        return "Resources/VoiceFiles/Chatter/" + chat_File;
    }

    public string GetLineFile(string line, int index)
    {
        string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
            "P","Q","R","S","T","U","V","W","X","Y","Z"};
        return "Resources/VoiceFiles/Salute/" + line + "/" + line + "_" + alphabet[index];
    }

    public string GetCallSignAIFile(int index)
    {
        return "Resources/VoiceFiles/Callsign_AI/" + RemoveSpace(callsign_AI[index]);
    }

    public string GetCallSignStudentFile(int index)
    {
        return "Resources/VoiceFiles/Callsign_Student/" + RemoveSpace(callsign_Student[index]);
    }
}

public class VoiceManager : MonoBehaviour
{
    [SerializeField] Transcript_Legend legend;
    string[] profiles;
    string path_Tail;
    public string selected_Profile;
    

    public bool audio_Speed;

    public int[] callsigns;
    

    public List<string> transcripts;
    public List<string> audio_Files;
    

    private void Awake()
    {
        profiles = new string[] { "Joey", "Salli" };
        legend = JsonUtility.FromJson<Transcript_Legend>(Resources.Load<TextAsset>("Transcript_Profile").text);
        callsigns = new int[2];
        Invoke("NewSequence", 0.5f);
    }

    public void ChangeSpeed(bool speed)
    {
        audio_Speed = speed;
        if (audio_Speed)
            path_Tail = "_Fast_" + selected_Profile;
        else
            path_Tail = "_Slow_" + selected_Profile;
    }

    public void NewSequence()
    {
        selected_Profile = profiles[Random.Range(0, profiles.Length)];
        path_Tail = "_Fast_" + selected_Profile;

        callsigns[0] = Random.Range(0, legend.callsign_AI.Count - 1);
        callsigns[1] = Random.Range(0, legend.callsign_Student.Count - 1);

        BuildContact();
        BuildPrep();
        BuildSalute();
    }

    public void BuildContact()
    {
        string type_Contact = (Random.Range(0, 2) == 0) ? "How Copy" : "Radio Check";

        string audio_Files = legend.GetCallSignStudentFile(callsigns[1]) + path_Tail + "\n"
            + legend.GetChatterFIle("This Is") + path_Tail + "\n" + legend.GetCallSignAIFile(callsigns[0]) + "\n"
            + legend.GetChatterFIle(type_Contact) + path_Tail + "\n" + legend.GetChatterFIle("Over") + path_Tail;
        string transcript = legend.callsign_Student[callsigns[1]] + " This I " + legend.callsign_AI[callsigns[0]]
            + type_Contact + ". Over.";
        transcripts.Add(transcript);
        this.audio_Files.Add(audio_Files);
        this.audio_Files.Add("BREAK");
    }

    public void BuildPrep()
    {
        string audio_Files = legend.GetCallSignStudentFile(callsigns[1]) + path_Tail + "\n" + legend.GetChatterFIle("Roger") + path_Tail + "\n"
            + legend.GetChatterFIle("Prepare to Copy") + path_Tail + "\n" + legend.GetChatterFIle("Over") + path_Tail;
        string transcript = legend.callsign_Student[callsigns[1]] + ". Roger. Prepare to Copy. Over.";
        transcripts.Add(transcript);
        this.audio_Files.Add(audio_Files);
        this.audio_Files.Add("BREAK");
    }

    public void BuildSalute()
    {
        int line1 = Random.Range(0, legend.line1_Transcript.Count);
        int line3 = Random.Range(0, legend.line3_Transcript.Count);
        int line4 = Random.Range(0, legend.line4_Transcript.Count);
        int line5 = Random.Range(0, legend.line5_Transcript.Count);
        int line6;
        if (line1 == 0)
            line6 = Random.Range(0, legend.line6_Transcript.Count);
        else if (line1 == 1)
            line6 = Random.Range(0, 2);
        else
            line6 = Random.Range(3, legend.line6_Transcript.Count);
        int line2 = (line6 != 1) ? Random.Range(0, legend.line2_Transcript.Count - 1) : Random.Range(0, legend.line6_Transcript.Count);

        string audio_Files = legend.GetLineFile("line1", line1) + path_Tail + "\n" + legend.GetLineFile("line2", line2) + "\n"
            + legend.GetLineFile("line3", line3) + path_Tail + "\n" + legend.GetLineFile("line4", line4) + "\n"
            + legend.GetLineFile("line5", line1) + path_Tail + "\n" + legend.GetLineFile("line6", line6);
        string transcript = legend.line1_Transcript[line1] + "\n" + legend.line2_Transcript[line2] + "\n"
            + legend.line3_Transcript[line3] + "\n" + legend.line4_Transcript[line4] + "\n"
            + legend.line5_Transcript[line5] + "\n" + legend.line6_Transcript[line6];
        transcripts.Add(transcript);
        this.audio_Files.Add(audio_Files);
        this.audio_Files.Add("BREAK");
    }

    public void StandBy()
    {
        string audio_Files = legend.GetCallSignStudentFile(callsigns[1]) + path_Tail + "\n"
            + legend.GetChatterFIle("Stand By") + path_Tail + "\n" + legend.GetChatterFIle("Over") + path_Tail;
        string transcript = legend.callsign_Student[callsigns[1]] + " Stand By. Over.";
        transcripts.Add(transcript);
        this.audio_Files.Add(audio_Files);
        this.audio_Files.Add("BREAK");
    }

    

}
