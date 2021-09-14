using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
    public class HighscoreManager : MonoBehaviour
    {
        public static HighscoreManager Instance;

        public string CurrentPlayerName;

        public int Score { get; private set; }
        public string PlayerName { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            LoadHighscore();
        }

        public bool UpdateHighscore(int newScore)
        {
            if (newScore < Score) return false;
            
            Score = newScore;
            PlayerName = CurrentPlayerName;
            SaveHighscore();
            
            return true;
        }

        [System.Serializable]
        private class SaveData
        {
            public int Score;
            public string PlayerName;
        }

        public void SaveHighscore()
        {
            var data = new SaveData { PlayerName = PlayerName, Score = Score };

            var json = JsonUtility.ToJson(data);
            
            File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
        }

        public void LoadHighscore()
        {
            var path = Application.persistentDataPath + "/highscore.json";
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<SaveData>(json);

                PlayerName = data.PlayerName;
                Score = data.Score;
            }
        }
    }
}