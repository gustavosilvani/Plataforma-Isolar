namespace Dominio.Dtos.Integrações.Sungrow
{
    public class SungrowRetornoPlantaListaDto
    {
        public SungrowUnidadeValorDto TotalEnergy { get; set; }  // Energia total gerada com unidade e valor.
        public int AlarmCount { get; set; }  // Contagem de alarmes ativos.
        public double Latitude { get; set; }  // Latitude geográfica da localização.
        public string Description { get; set; }  // Descrição do dispositivo ou instalação.
        public string TotalIncomeUpdateTime { get; set; }  // Data da última atualização da renda total.
        public int ValidFlag { get; set; }  // Flag de validade, usado para indicar se os dados são considerados válidos.
        public SungrowUnidadeValorDto CurrentPower { get; set; }  // Potência atual sendo gerada.
        public int PsFaultStatus { get; set; }  // Status de falha do sistema de energia.
        public string Co2ReduceUpdateTime { get; set; }  // Data da última atualização da redução de CO2.
        public string InstallDate { get; set; }  // Data de instalação do sistema.
        public int BuildStatus { get; set; }  // Status de construção do sistema.
        public string TodayEnergyUpdateTime { get; set; }  // Data da última atualização da energia gerada hoje.
        public string TotalEnergyUpdateTime { get; set; }  // Data da última atualização da energia total gerada.
        public int PsType { get; set; }  // Tipo de sistema de energia.
        public double Longitude { get; set; }  // Longitude geográfica da localização.
        public string TotalCapacityUpdateTime { get; set; }  // Data da última atualização da capacidade total.
        public string EquivalentHourUpdateTime { get; set; }  // Data da última atualização da hora equivalente.
        public string PsName { get; set; }  // Nome do sistema de energia.
        public SungrowUnidadeValorDto Co2ReduceTotal { get; set; }  // Redução total de CO2 com unidade e valor.
        public string CurrentPowerUpdateTime { get; set; }  // Data da última atualização da potência atual.
        public SungrowUnidadeValorDto TodayIncome { get; set; }  // Renda de hoje com unidade e valor.
        public int GridConnectionStatus { get; set; }  // Status da conexão à rede.
        public SungrowUnidadeValorDto EquivalentHour { get; set; }  // Hora equivalente gerada pelo sistema.
        public string Co2ReduceTotalUpdateTime { get; set; }  // Data da última atualização da redução total de CO2.
        public string PsLocation { get; set; }  // Localização física do sistema.
        public SungrowUnidadeValorDto TotalIncome { get; set; }  // Renda total gerada com unidade e valor.
        public SungrowUnidadeValorDto TotalCapacity { get; set; }  // Capacidade total do sistema com unidade e valor.
        public string ShareType { get; set; }  // Tipo de compartilhamento.
        public string PsCurrentTimeZone { get; set; }  // Fuso horário atual do sistema.
        public string TodayIncomeUpdateTime { get; set; }  // Data da última atualização da renda de hoje.
        public int PsId { get; set; }  // Identificador do sistema de energia.
        public string GridConnectionTime { get; set; }  // Data e hora da conexão à rede.
        public int ConnectType { get; set; }  // Tipo de conexão.
        public SungrowUnidadeValorDto TodayEnergy { get; set; }  // Energia gerada hoje com unidade e valor.
        public int PsStatus { get; set; }  // Status operacional do sistema de energia.
        public SungrowUnidadeValorDto Co2Reduce { get; set; }  // Redução de CO2 atual com unidade e valor.
        public int FaultCount { get; set; }  // Contagem de falhas reportadas pelo sistema.
    }

    public class SungrowUnidadeValorDto
    {
        public string Unit { get; set; }  // Unidade de medida, como kW.
        public string Value { get; set; }  // Valor da medida.
    }   
    
}
