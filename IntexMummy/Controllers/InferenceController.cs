using System;
using System.Collections.Generic;
using System.Linq;
using IntexMummy.MLModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace IntexMummy.Controllers
{
    [ApiController]
    [Route("/predict")]
    public class InferenceController : ControllerBase
    {
        private InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        [HttpPost]
        public ActionResult Score(SupervisedModel data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
                });
            Tensor<string> score = result.First().AsTensor<string>();
            var categories = new[] { "W", "E" };
            int predictionIndex = Array.IndexOf(score.ToArray(), score.Max());
            var prediction = new Prediction { PredictedValue = categories[predictionIndex] };
            return Ok(prediction);
        }
    }
}
