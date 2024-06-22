using Newtonsoft.Json;

namespace Dominio.Dtos.Integracoes.Sungrow
{
    public class SungrowRetornoAlertaPlantaDto
    {
        [JsonProperty("ps_id")]
        public int IdPs { get; set; }

        [JsonProperty("ps_key")]
        public string ChavePs { get; set; }

        [JsonProperty("fault_type_code")]
        public int CodigoTipoFalha { get; set; }

        [JsonProperty("fault_code")]
        public string CodigoFalha { get; set; }

        [JsonProperty("fault_type")]
        public int TipoFalha { get; set; }

        [JsonProperty("fault_level")]
        public int NivelFalha { get; set; }

        [JsonProperty("fault_desc")]
        public string DescricaoFalha { get; set; }

        [JsonProperty("process_status")]
        public int StatusProcesso { get; set; }

        [JsonProperty("create_time")]
        public string DataCriacao { get; set; }

        [JsonProperty("process_time")]
        public long TempoProcesso { get; set; }

        [JsonProperty("fault_reason")]
        public string MotivoFalha { get; set; }

        [JsonProperty("device_model_code")]
        public string CodigoModeloDispositivo { get; set; }

        [JsonProperty("device_model")]
        public string ModeloDispositivo { get; set; }

        [JsonProperty("device_name")]
        public string NomeDispositivo { get; set; }

        [JsonProperty("device_type")]
        public int TipoDispositivo { get; set; }

        [JsonProperty("ps_name")]
        public string NomePs { get; set; }

        [JsonProperty("uuid")]
        public int Uuid { get; set; }

        [JsonProperty("type_name")]
        public string NomeTipo { get; set; }

        [JsonProperty("fault_name")]
        public string NomeFalha { get; set; }

        [JsonProperty("over_time")]
        public string TempoExcedido { get; set; }
    }
}
