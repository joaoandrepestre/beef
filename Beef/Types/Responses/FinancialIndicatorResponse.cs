namespace Beef.Types.Responses; 

public class FinancialIndicatorResponse {
    public long SecurityIdentificationCode { get; set; }
    public string Description { get; set; }
    public string GroupDescription { get; set; }
    public string Value { get; set; }
    public string Rate { get; set; }
    public string LastUpdate { get; set; }
}