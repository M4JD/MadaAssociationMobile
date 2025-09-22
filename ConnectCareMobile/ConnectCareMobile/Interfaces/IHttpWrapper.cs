using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ConnectCareMobile.Interfaces
{
    internal interface IHttpWrapper
    {
        Task<T> PostAPICallAsync<T>(string controller, string action, object bodyParam, string urlAmmendement, CancellationToken cancellationToken = default);
        Task<T> PostMultiPartAPICallAsync<T>(string controller, string action, Stream imageStream, Dictionary<string, object> stringParts, string urlAmmendement, CancellationToken cancellationToken = default);
        Task<T> PostMultiPartDocumentAPICallAsync<T>(string controller, string action, Stream imageStream, Dictionary<string, object> stringParts, string urlAmmendement, string fileName, string mime, CancellationToken cancellationToken = default);
        Task<T> PostDocumentAPICallAsync<T>(string controller, string action, Stream imageStream, string urlAmmendement, string fileName, string mime, CancellationToken cancellationToken = default);
        Task<T> PostAPITokenCallAsync<T>(string controller, string action, object bodyParam, string urlAmmendement);
        Task<T> GetAPICallAsync<T>(string controller, string action, string urlAmmendement);
    }
}
