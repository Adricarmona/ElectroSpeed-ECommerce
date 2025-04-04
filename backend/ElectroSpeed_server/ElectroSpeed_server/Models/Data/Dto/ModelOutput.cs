﻿using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class ModelOutput
    {
        [ColumnName(@"text")]
        public string Text { get; set; }

        [ColumnName(@"label")]
        public uint Label { get; set; }

        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")]
        public float[] Score { get; set; }

    }
}
