using Extism;

var req = new HttpRequest(
    "https://httpbin.org/anything")
{
    Method = HttpMethod.GET
};

var res = Pdk.SendRequest(req);

Pdk.SetOutput(res.Body);
