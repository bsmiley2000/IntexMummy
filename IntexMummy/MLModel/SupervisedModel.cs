using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexMummy.MLModel
{
    public class SupervisedModel
    {
        public float squarenorthsouth { get; set; }
        public float depth { get; set; }
        public float southtohead { get; set; }
        public float squareeastwest { get; set; }
        public float westtohead { get; set; }
        public float length { get; set; }
        public float westtofeet { get; set; }
        public float southtofeet { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            squarenorthsouth, depth, southtohead, squareeastwest,
            westtohead, length, westtofeet, southtofeet
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
