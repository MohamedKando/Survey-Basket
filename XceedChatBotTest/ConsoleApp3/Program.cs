
using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace XceedTest
{
    public class ChatBotTraining
    {
        static MLContext mlContext;
        static ITransformer trainedModel;
        static PredictionEngine<InputData, OutputData> predictionEngine;
        static HashSet<string> stopWords;
        static string modelFilePath = @"trainedModelMentalHealth.zip"; // Path to save/load the trained model
        public static void Main(string[] args)
        {
            
            InitializeChatbot();

            if (predictionEngine != null)
            {
                Console.WriteLine("Chatbot is ready. Type 'exit' to quit.");
                while (true)
                {
                    Console.WriteLine("You: ");
                    string userInput = Console.ReadLine();
                    if (string.Equals(userInput?.ToLower(), "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    
                    string botResponse = GenerateBotResponse(userInput);
                    Console.WriteLine($"Bot: {botResponse}");
                }
            }
            else
            {
                Console.WriteLine("Chatbot model is not ready.");
            }
        }
        public static void ModelTrainer()
        {
            var intents = LoadDataFromJson();
            var data = PreprocessData(intents);
            trainedModel = TrainModel(data);
            SaveTrainedModel();
        }
        public static void InitializeChatbot()
        {
            mlContext = new MLContext();

            bool trainNewModel = AskUserToTrainModel(); 
            if (trainNewModel)
            {
                ModelTrainer();
            }
            else
            {
                LoadTrainedModel(); 
            }

            if (trainedModel == null)
            {
                return;
            }

            predictionEngine = mlContext.Model.CreatePredictionEngine<InputData, OutputData>(trainedModel);
            

        }

        static bool AskUserToTrainModel()
        {
            Console.WriteLine("Do you want to train a new model? (y/n)");
            string input = Console.ReadLine();
            return string.Equals(input?.ToLower(), "y", StringComparison.OrdinalIgnoreCase);
        }

        public static string GenerateBotResponse(string userInput)
        {
            string processedInput = PreprocessInput(userInput);
            var input = new InputData { Text = processedInput };
            var prediction = predictionEngine.Predict(input);
            return prediction.PredictedLabel;
        }

        public static List<Intent> LoadDataFromJson()
        {
            //string jsonData = ReadFileFromResources("filepath.json"); // use this line if u want to load data from resources
            string jsonData = File.ReadAllText(@"D:\Work\intents.json");
            IntentData intentData = JsonConvert.DeserializeObject<IntentData>(jsonData);
            return intentData?.Intents ?? new List<Intent>();
        }

       /* private static string ReadFileFromResources(string fileName)
        {
            Stream stream = typeof(ChatBotTraining).Assembly.GetManifestResourceStream(fileName);

            if (stream == null)
            {
                throw new FileNotFoundException("File not found in resources");
            }

            StreamReader reader = new StreamReader(stream);

            string data = reader.ReadToEnd();
            reader.Close();
            return data;
        }*/
        static List<InputData> PreprocessData(List<Intent> intents)
        {
            var preprocessedData = new List<InputData>();
            foreach (var intent in intents)
            {
                foreach (var pattern in intent.Patterns)
                {
                    string processedText = PreprocessText(pattern);
                    foreach (var response in intent.Responses)
                    {
                        string processedResponse = PreprocessText(response);
                        preprocessedData.Add(new InputData { Text = processedText, Response = processedResponse });
                    }
                }
            }

            return preprocessedData;
        }

        static string PreprocessText(string text)
        {

            return text;
        }

       /* static HashSet<string> LoadStopWords(string filePath)
        {
            //string jsonData = ReadFileFromResources("VCON.AI.Resources.stopwords.txt");
            var stopWords = new HashSet<string>(jsonData.Split(',').Select(x => x.ToLower()));
            return stopWords;
        }*/

        static string PreprocessInput(string input)
        {
            // Tokenization: Split input into words
            var tokens = input.ToLower().Split(' ');

            // Remove stop words
            //tokens = tokens.Where(token => !stopWords.Contains(token)).ToArray();

            // Join tokens back into a string
            string processedInput = string.Join(" ", tokens);
            return processedInput;
        }

        static ITransformer TrainModel(IEnumerable<InputData> data)
        {
            var pipeline = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(InputData.Text))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(InputData.Response)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainingData = mlContext.Data.LoadFromEnumerable(data);
            return pipeline.Fit(trainingData);
        }

        static void SaveTrainedModel()
        {
            mlContext.Model.Save(trainedModel, null, modelFilePath);
        }

        static void LoadTrainedModel()
        {
            if (File.Exists(modelFilePath))
            {
                trainedModel = mlContext.Model.Load(modelFilePath, out _);
            }
            else
            {

            }
        }

        public class InputData
        {
            public string Text { get; set; }
            public string Response { get; set; }
        }

        public class OutputData
        {
            [ColumnName("PredictedLabel")]
            public string PredictedLabel { get; set; }
        }

        public class Intent
        {
            public string Tag { get; set; }
            public List<string> Patterns { get; set; }
            public List<string> Responses { get; set; }
        }

        public class IntentData
        {
            public List<Intent> Intents { get; set; }
        }
    }
}

