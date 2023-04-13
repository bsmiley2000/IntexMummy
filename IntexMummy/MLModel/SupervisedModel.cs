using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexMummy.MLModel
{
    public class SupervisedModel
    {
        public float Squarenorthsouth { get; set; }
        public float Depth { get; set; }
        public float Southtohead { get; set; }
        public float Squareeastwest { get; set; }
        public float Westtohead { get; set; }
        public float Length { get; set; }
        public float Westtofeet { get; set; }
        public float Southtofeet { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            Squarenorthsouth, Depth, Southtohead, Squareeastwest,
            Westtohead, Length, Westtofeet, Southtofeet
            };
            int[] dimensions = new int[] { 1, 8 };
            return new DenseTensor<float>(data, dimensions);
        }
    }

    public class Prediction
    {
        public string PredictedValue { get; set; }
    }
}
