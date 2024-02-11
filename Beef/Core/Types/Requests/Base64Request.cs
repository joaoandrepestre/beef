using System.Text;

namespace Beef.Core.Types.Requests; 

internal class Base64Request : IRequest {
    public virtual string Language { get; set; }

    public override string ToString() =>
        $"{{\"language\":\"{Language}\"}}";
    
    private string ToBase64Url() {
        var bytes = Encoding.UTF8.GetBytes(ToString());
        return Convert.ToBase64String(bytes);
    }

    public string GetUrlParams() => ToBase64Url();
}