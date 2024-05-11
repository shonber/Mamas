using System.Runtime.Serialization;

namespace Game2048;

public static class LeaderBoardFileManager
{
    public static readonly string leaderBoardPathName = "./leaderBoard.xml";

    public static void SaveLeaderBoardToXml(Dictionary<DateTime, string[]> leaderBoard){
        // The method will save a dictionary to a XML file.
        
        DataContractSerializer serializer = new(leaderBoard.GetType());

        using var writer = new FileStream(leaderBoardPathName, FileMode.Create, FileAccess.Write);
        serializer.WriteObject(writer, leaderBoard);
    }

    public static Dictionary<DateTime, string[]> ReadLeaderBoardFromXml(Dictionary<DateTime, string[]> leaderBoard){
        // The method will load data to LeaderBoard from a XML file.
        
        DataContractSerializer serializer = new(leaderBoard.GetType());

        using var reader = new FileStream(leaderBoardPathName, FileMode.Open, FileAccess.Read);
        if (reader.Length <= 0)
            return [];

        Dictionary<DateTime, string[]> loaded = (Dictionary<DateTime, string[]>)serializer.ReadObject(reader);
        if (loaded == null)
            return [];

        return loaded;
    }

    public static Dictionary<DateTime, string[]> LoadLeaderBoard(Dictionary<DateTime, string[]> leaderBoard){
        // The method will load a leader board from a XML file.

        if (File.Exists(leaderBoardPathName)) {
            return ReadLeaderBoardFromXml(leaderBoard);
        }
        else {
           using FileStream fs = File.Create(leaderBoardPathName);
           return [];
        }
    }
}