using Beef.Fetchers;


var client = new HttpClient();
var ins = new InstrumentFetcher(client);

var res = await ins.Fetch("PETR4");
