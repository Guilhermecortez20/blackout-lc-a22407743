using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Blackout
{
    /// <summary>
    /// Persiste o melhor (menor) número de movimentos por dificuldade.
    /// Persistência entre sessões via ficheiro JSON.
    /// </summary>
    public sealed class HighScoreStore
    {
        private sealed class ScoreModel
        {
            public int? EasyMoves { get; set; }
            public int? MediumMoves { get; set; }
            public int? HardMoves { get; set; }
        }

        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public HighScoreStore(string? filePath = null)
        {
            _filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "highscores.json");
        }

        public int? GetHighScore(Difficulty difficulty)
        {
            ScoreModel model = LoadModel();
            return difficulty switch
            {
                Difficulty.Easy => model.EasyMoves,
                Difficulty.Medium => model.MediumMoves,
                Difficulty.Hard => model.HardMoves,
                _ => null
            };
        }

        public bool TryUpdateHighScore(Difficulty difficulty, int moves)
        {
            ScoreModel model = LoadModel();

            int? current = difficulty switch
            {
                Difficulty.Easy => model.EasyMoves,
                Difficulty.Medium => model.MediumMoves,
                Difficulty.Hard => model.HardMoves,
                _ => null
            };

            bool shouldUpdate = current is null || moves < current.Value;
            if (!shouldUpdate) return false;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    model.EasyMoves = moves;
                    break;
                case Difficulty.Medium:
                    model.MediumMoves = moves;
                    break;
                case Difficulty.Hard:
                    model.HardMoves = moves;
                    break;
            }

            SaveModel(model);
            return true;
        }

        private ScoreModel LoadModel()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new ScoreModel();

                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new ScoreModel();

                var model = JsonSerializer.Deserialize<ScoreModel>(json, _jsonOptions);
                return model ?? new ScoreModel();
            }
            catch
            {
                // Em caso de ficheiro corrompido/permissões, não bloquear o jogo.
                return new ScoreModel();
            }
        }

        private void SaveModel(ScoreModel model)
        {
            string dir = Path.GetDirectoryName(_filePath) ?? AppContext.BaseDirectory;
            Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(model, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
    }
}

