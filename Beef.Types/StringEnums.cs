using Beef.Types.Core;
using Beef.Types.Core.Requests;

namespace Beef.Types;

public class StringEnum {
    private readonly string _value;

    protected StringEnum(string value) {
        _value = value;
    }

    public sealed override string ToString() => _value;
    public static implicit operator string(StringEnum s) => s.ToString();

    public static readonly StringEnum Empty = new("");
}

public class PagedRequestFieldSelector : StringEnum {
    private PagedRequestFieldSelector(string value) : base(value){ }
    public static readonly PagedRequestFieldSelector Language = new(nameof(PagedRequest.Language));
    public static readonly PagedRequestFieldSelector PageNumber = new(nameof(PagedRequest.PageNumber));
    public static readonly PagedRequestFieldSelector PageSize = new(nameof(PagedRequest.PageSize));
    public static readonly PagedRequestFieldSelector TradingName = new(nameof(PagedRequest.TradingName));
    public static readonly PagedRequestFieldSelector Company = new(nameof(PagedRequest.Company));
}

public class SupportedEndpoints : StringEnum {
    private SupportedEndpoints(string value) : base(value) { }
    public static readonly SupportedEndpoints CompanySearch = new("https://sistemaswebb3-listados.b3.com.br/listedCompaniesProxy/CompanyCall/GetInitialCompanies/");
    public static readonly SupportedEndpoints CashProvisions = new("https://sistemaswebb3-listados.b3.com.br/listedCompaniesProxy/CompanyCall/GetListedCashDividends/");
    public static readonly SupportedEndpoints TradingData = new("https://arquivos.b3.com.br/apinegocios/ticker/");
    public static readonly SupportedEndpoints FinancialIndicators = new("https://sistemaswebb3-derivativos.b3.com.br/financialIndicatorsProxy/FinancialIndicators/GetFinancialIndicators/");
    
    public static implicit operator SupportedEndpoints(PagedEndpoints endpoint) => new(endpoint);
    public static implicit operator SupportedEndpoints(UnpagedEndpoints endpoint) => new(endpoint);
    public static implicit operator SupportedEndpoints(Base64Endpoints endpoint) => new(endpoint);
}

public sealed class PagedEndpoints : StringEnum {
    private PagedEndpoints(string value) : base(value) { }
    public static readonly PagedEndpoints CompanySearch = new(SupportedEndpoints.CompanySearch);
    public static readonly PagedEndpoints CashProvisions = new(SupportedEndpoints.CashProvisions);
}

public sealed class UnpagedEndpoints : StringEnum {
    private UnpagedEndpoints(string value) : base(value) { }
    
    public static readonly UnpagedEndpoints TradingData = new(SupportedEndpoints.TradingData);
}

public sealed class Base64Endpoints : StringEnum {
    private Base64Endpoints(string value) : base(value) { }
    
    public static readonly Base64Endpoints FinancialIndicators = new(SupportedEndpoints.FinancialIndicators);
}