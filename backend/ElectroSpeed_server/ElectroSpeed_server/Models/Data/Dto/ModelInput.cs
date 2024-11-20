using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class ModelInput
    {
        [LoadColumn(0)]
        [ColumnName(@"text")]
        public string Text { get; set; }

        [LoadColumn(1)]
        [ColumnName(@"label")]
        public float Label { get; set; }
    }
}
