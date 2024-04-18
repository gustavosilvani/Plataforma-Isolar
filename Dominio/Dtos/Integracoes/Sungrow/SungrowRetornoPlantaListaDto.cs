using Newtonsoft.Json;

namespace Dominio.Dtos.Integracoes.Sungrow
{
    public class SungrowRetornoPlantaListaDto
    {
        [JsonProperty("total_energy")]
        public SungrowUnidadeValorDto EnergiaTotal { get; set; }  // Energia total gerada com unidade e valor.

        [JsonProperty("alarm_count")]
        public int ContagemAlarmes { get; set; }  // Contagem de alarmes ativos.

        [JsonProperty("latitude")]
        public double Latitude { get; set; }  // Latitude geográfica da localização.

        [JsonProperty("description")]
        public string Descricao { get; set; }  // Descrição do dispositivo ou instalação.

        [JsonProperty("total_income_update_time")]
        public string DataAtualizacaoRendaTotal { get; set; }  // Data da última atualização da renda total.

        [JsonProperty("valid_flag")]
        public int IndicadorValidade { get; set; }  // Flag de validade, usado para indicar se os dados são considerados válidos.

        [JsonProperty("curr_power")]
        public SungrowUnidadeValorDto PotenciaAtual { get; set; }  // Potência atual sendo gerada.

        [JsonProperty("ps_fault_status")]
        public int StatusFalhaSistemaEnergia { get; set; }  // Status de falha do sistema de energia.

        [JsonProperty("co2_reduce_update_time")]
        public string DataAtualizacaoReducaoCO2 { get; set; }  // Data da última atualização da redução de CO2.

        [JsonProperty("install_date")]
        public string DataInstalacao { get; set; }  // Data de instalação do sistema.

        [JsonProperty("build_status")]
        public int StatusConstrucao { get; set; }  // Status de construção do sistema.

        [JsonProperty("today_energy_update_time")]
        public string DataAtualizacaoEnergiaHoje { get; set; }  // Data da última atualização da energia gerada hoje.

        [JsonProperty("total_energy_update_time")]
        public string DataAtualizacaoEnergiaTotal { get; set; }  // Data da última atualização da energia total gerada.

        [JsonProperty("ps_type")]
        public int TipoSistemaEnergia { get; set; }  // Tipo de sistema de energia.

        [JsonProperty("longitude")]
        public double Longitude { get; set; }  // Longitude geográfica da localização.

        [JsonProperty("total_capacity_update_time")]
        public string DataAtualizacaoCapacidadeTotal { get; set; }  // Data da última atualização da capacidade total.

        [JsonProperty("equivalent_hour_update_time")]
        public string DataAtualizacaoHoraEquivalente { get; set; }  // Data da última atualização da hora equivalente.

        [JsonProperty("ps_name")]
        public string NomeSistemaEnergia { get; set; }  // Nome do sistema de energia.

        [JsonProperty("co2_reduce_total")]
        public SungrowUnidadeValorDto ReducaoTotalCO2 { get; set; }  // Redução total de CO2 com unidade e valor.

        [JsonProperty("curr_power_update_time")]
        public string DataAtualizacaoPotenciaAtual { get; set; }  // Data da última atualização da potência atual.

        [JsonProperty("today_income")]
        public SungrowUnidadeValorDto RendaHoje { get; set; }  // Renda de hoje com unidade e valor.

        [JsonProperty("grid_connection_status")]
        public int StatusConexaoRede { get; set; }  // Status da conexão à rede.

        [JsonProperty("equivalent_hour")]
        public SungrowUnidadeValorDto HoraEquivalente { get; set; }  // Hora equivalente gerada pelo sistema.

        [JsonProperty("co2_reduce_total_update_time")]
        public string DataAtualizacaoReducaoTotalCO2 { get; set; }  // Data da última atualização da redução total de CO2.

        [JsonProperty("ps_location")]
        public string LocalizacaoSistemaEnergia { get; set; }  // Localização física do sistema.

        [JsonProperty("total_income")]
        public SungrowUnidadeValorDto RendaTotal { get; set; }  // Renda total gerada com unidade e valor.

        [JsonProperty("total_capacity")]
        public SungrowUnidadeValorDto CapacidadeTotal { get; set; }  // Capacidade total do sistema com unidade e valor.

        [JsonProperty("share_type")]
        public string TipoCompartilhamento { get; set; }  // Tipo de compartilhamento.

        [JsonProperty("ps_current_time_zone")]
        public string FusoHorarioAtualSistema { get; set; }  // Fuso horário atual do sistema.

        [JsonProperty("today_income_update_time")]
        public string DataAtualizacaoRendaHoje { get; set; }  // Data da última atualização da renda de hoje.

        [JsonProperty("ps_id")]
        public int IdSistemaEnergia { get; set; }  // Identificador do sistema de energia.

        [JsonProperty("grid_connection_time")]
        public string DataHoraConexaoRede { get; set; }  // Data e hora da conexão à rede.

        [JsonProperty("connect_type")]
        public int TipoConexao { get; set; }  // Tipo de conexão.

        [JsonProperty("today_energy")]
        public SungrowUnidadeValorDto EnergiaHoje { get; set; }  // Energia gerada hoje com unidade e valor.

        [JsonProperty("ps_status")]
        public int StatusOperacionalSistemaEnergia { get; set; }  // Status operacional do sistema de energia.

        [JsonProperty("co2_reduce")]
        public SungrowUnidadeValorDto ReducaoCO2Atual { get; set; }  // Redução de CO2 atual com unidade e valor.

        [JsonProperty("fault_count")]
        public int ContagemFalhas { get; set; }  // Contagem de falhas reportadas pelo sistema.
    }

    public class SungrowUnidadeValorDto
    {
        [JsonProperty("unit")]
        public string Unidade { get; set; }  // Unidade de medida, como kW.

        [JsonProperty("value")]
        public string Valor { get; set; }  // Valor da medida.
    }
}
