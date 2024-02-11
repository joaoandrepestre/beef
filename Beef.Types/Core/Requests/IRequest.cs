using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(nameof(Beef))]
namespace Beef.Types.Core.Requests;

internal interface IRequest {
    string GetUrlParams();
}