namespace Beef.Types.Requests; 

public readonly record struct SymbolDate(string Symbol, DateOnly ReferenceDate) {
    public SymbolDate(string symbol, DateTime date) : this(symbol, DateOnly.FromDateTime(date)){}

    public override string ToString() => $"{Symbol}|{ReferenceDate}";
}