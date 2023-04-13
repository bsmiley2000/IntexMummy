using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace IntexMummy.Models
{
    public class OnnxModel
    {
        static void Main(string[] args)
        {
            // Load the ONNX model file
            var modelPath = "wwwroot/model.onnx";
            var session = new InferenceSession(modelPath);

            // Prepare input data
            var inputData = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f };
            var tensor = new DenseTensor<float>(inputData, new[] { 1, 8 });

            // Run the prediction
            var inputMeta = session.InputMetadata;
            var outputMeta = session.OutputMetadata;
            var inputName = inputMeta.Keys.GetEnumerator().Current;
            var outputName = outputMeta.Keys.GetEnumerator().Current;
            var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor<float>("input", tensor) };
            var results = session.Run(inputs);
            var outputTensor = results.First().AsTensor<long>();
            var prediction = outputTensor.ToArray()[0];

            // Print the predicted label
            Console.WriteLine($"The predicted label is: {prediction}");
        }
    }
}