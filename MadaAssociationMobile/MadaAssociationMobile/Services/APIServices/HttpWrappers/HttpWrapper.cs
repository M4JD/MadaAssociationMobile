using MadaAssociationMobile.Common.Exceptions;
using Flurl;
using Flurl.Http;
using MadaAssociationMobile.Common.Global;
using MadaAssociationMobile.Interfaces;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;

namespace MadaAssociationMobile.APIService.HttpWrappers
{
    public class HttpWrapper : IHttpWrapper
    {
        public HttpWrapper(ISettings settings)
        {
            iSettings = settings;
            var pauseBetweenFailures = TimeSpan.FromSeconds(AppSharedPrefences.RetryDelayInSeconds);
            policyRetry = Policy
            .Handle<FlurlHttpException>(HandleHttpException)
            .WaitAndRetryAsync(AppSharedPrefences.APICallsRetries, i => pauseBetweenFailures
            , (exception, timeSpan) =>
            {
                Crashes.TrackError(exception);
                Debug.WriteLine("API Error: " + exception.ToString());
            });
        }
        private bool HandleHttpException(FlurlHttpException ex)
        {
            HttpStatusCode httpStatusCode;
            var parsed = Enum.TryParse(ex.StatusCode.ToString(), out httpStatusCode);
            if (parsed)
            {
                return httpStatusCode == HttpStatusCode.RequestTimeout;
            }
            else
                return false;
        }
        private ISettings iSettings;
        private AsyncRetryPolicy policyRetry;
        public async Task<T> PostAPICallAsync<T>(string controller, string action, object bodyParam, string urlAmmendement,
                                                    CancellationToken cancellationToken = default)
        {
            try
            {
                var usedURL = iSettings.APIURL + urlAmmendement;
                if (GlobalSettings.HasProxyURL && !string.IsNullOrWhiteSpace(AppSharedPrefences.Instance.ProxyURL))
                {
                    FlurlHttp.Configure(settings =>
                    {
                        settings.HttpClientFactory = new ProxyHttpClientFactory(AppSharedPrefences.Instance.ProxyURL);
                    });
                }
                else
                {
                    FlurlHttp.ConfigureClient(usedURL, cli =>
                 cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
                }

                return await policyRetry.ExecuteAsync(async () =>
                {
                    if (!iSettings.IsConnected()) throw new NoConnectionException();
                    if (string.IsNullOrEmpty(iSettings.APIToken))
                    {
                        return await usedURL
                                     .WithTimeout(AppSharedPrefences.APICallsTimeOut)
                                     .AppendPathSegments(controller, action)
                                     .ConfigureRequest(c => { c.BeforeCall = BeforeCallDelegate; c.AfterCall = AfterCallDelegate; })
                                     .PostJsonAsync(bodyParam, cancellationToken)
                                     .ReceiveJson<T>();
                    }
                    else
                        return await usedURL
                                     .WithTimeout(AppSharedPrefences.APICallsTimeOut)
                                     .WithOAuthBearerToken(iSettings.APIToken)
                                     .AppendPathSegments(controller, action)
                                     .ConfigureRequest(c => { c.BeforeCall = BeforeCallDelegate; c.AfterCall = AfterCallDelegate; })
                                     .PostJsonAsync(bodyParam, cancellationToken)
                                     .ReceiveJson<T>();
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
        public async Task<T> PostMultiPartAPICallAsync<T>(string controller, string action, Stream imageStream, Dictionary<string, object> stringParts, string urlAmmendement,
                                            CancellationToken cancellationToken = default)
        {
            try
            {
                dynamic multiPartString = stringParts.Aggregate(new ExpandoObject() as IDictionary<string, Object>,
                                            (a, p) => { a.Add(p.Key, p.Value); return a; });
                var usedURL = iSettings.APIURL + urlAmmendement;

                if (GlobalSettings.HasProxyURL && !string.IsNullOrWhiteSpace(AppSharedPrefences.Instance.ProxyURL))
                {
                    FlurlHttp.Configure(settings =>
                    {
                        settings.HttpClientFactory = new ProxyHttpClientFactory(AppSharedPrefences.Instance.ProxyURL);
                    });
                }
                else
                {
                    FlurlHttp.ConfigureClient(usedURL, cli =>
                 cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
                }
                return await policyRetry.ExecuteAsync(async () =>
                {
                    if (!iSettings.IsConnected()) throw new NoConnectionException();
                    return await usedURL
                                 .WithTimeout(GlobalSettings.ImageUploadCallsTimeOut)
                                 .WithOAuthBearerToken(iSettings.APIToken)
                                 .AppendPathSegments(controller, action)
                                 .ConfigureRequest(c => { c.BeforeCall = BeforeCallDelegate; c.AfterCall = AfterCallDelegate; })
                                 .PostMultipartAsync(mp => mp
                                    .AddStringParts(multiPartString)
                                    .AddFile("file", imageStream, "image.txt", "image/jpeg"))
                                 .ReceiveJson<T>();
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
        public async Task<T> PostMultiPartDocumentAPICallAsync<T>(string controller, string action, Stream imageStream, Dictionary<string, object> stringParts, string urlAmmendement,
                                    string fileName, string mime, CancellationToken cancellationToken = default)
        {
            try
            {
                dynamic multiPartString = stringParts.Aggregate(new ExpandoObject() as IDictionary<string, Object>,
                                            (a, p) => { a.Add(p.Key, p.Value); return a; });
                var usedURL = iSettings.APIURL + urlAmmendement;

                if (GlobalSettings.HasProxyURL && !string.IsNullOrWhiteSpace(AppSharedPrefences.Instance.ProxyURL))
                {
                    FlurlHttp.Configure(settings =>
                    {
                        settings.HttpClientFactory = new ProxyHttpClientFactory(AppSharedPrefences.Instance.ProxyURL);
                    });
                }
                else
                {
                    FlurlHttp.ConfigureClient(usedURL, cli =>
                 cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
                }
                return await policyRetry.ExecuteAsync(async () =>
                {
                    if (!iSettings.IsConnected()) throw new NoConnectionException();
                    return await usedURL
                                 .WithTimeout(GlobalSettings.ImageUploadCallsTimeOut)
                                 .WithOAuthBearerToken(iSettings.APIToken)
                                 .AppendPathSegments(controller, action)
                                 .ConfigureRequest(c => { c.BeforeCall = BeforeCallDelegate; c.AfterCall = AfterCallDelegate; })
                                 .PostMultipartAsync(mp => mp
                                    .AddStringParts(multiPartString)
                                    .AddFile("file", imageStream, fileName, mime))
                                 .ReceiveJson<T>();
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
        public async Task<T> PostDocumentAPICallAsync<T>(string controller, string action, Stream imageStream, string urlAmmendement,
                            string fileName, string mime, CancellationToken cancellationToken = default)
        {
            try
            {
                var usedURL = iSettings.APIURL + urlAmmendement;

                if (GlobalSettings.HasProxyURL && !string.IsNullOrWhiteSpace(AppSharedPrefences.Instance.ProxyURL))
                {
                    FlurlHttp.Configure(settings =>
                    {
                        settings.HttpClientFactory = new ProxyHttpClientFactory(AppSharedPrefences.Instance.ProxyURL);
                    });
                }
                else
                {
                    FlurlHttp.ConfigureClient(usedURL, cli =>
                 cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
                }
                return await policyRetry.ExecuteAsync(async () =>
                {
                    if (!iSettings.IsConnected()) throw new NoConnectionException();
                    return await usedURL
                                 .WithTimeout(GlobalSettings.ImageUploadCallsTimeOut)
                                 .WithOAuthBearerToken(iSettings.APIToken)
                                 .AppendPathSegments(controller, action)
                                 .ConfigureRequest(c => { c.BeforeCall = BeforeCallDelegate; c.AfterCall = AfterCallDelegate; })
                                 .PostMultipartAsync(mp => mp
                                    .AddFile("file", imageStream, fileName, mime))
                                 .ReceiveJson<T>();
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
        public async Task<T> PostAPITokenCallAsync<T>(string controller, string action, object bodyParam, string urlAmmendement)
        {
            try
            {
                var usedURL = iSettings.APIURL + urlAmmendement;
                if (GlobalSettings.HasProxyURL && !string.IsNullOrWhiteSpace(AppSharedPrefences.Instance.ProxyURL))
                {
                    FlurlHttp.Configure(settings =>
                    {
                        settings.HttpClientFactory = new ProxyHttpClientFactory(AppSharedPrefences.Instance.ProxyURL);
                    });
                }
                else
                {
                    FlurlHttp.ConfigureClient(usedURL, cli =>
                 cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
                }
                policyRetry = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(AppSharedPrefences.PublicTokenCallRetries, i => TimeSpan.FromSeconds(AppSharedPrefences.RetryDelayInSeconds)
                , (exception, timeSpan) =>
                {
                    Debug.WriteLine("Get token Error: " + exception.ToString());
                });
                return await policyRetry.ExecuteAsync(async () =>
                {
                    if (!iSettings.IsConnected()) throw new NoConnectionException();
                    //return await usedURL
                    //             .AppendPathSegments(controller, action)
                    //             .WithHeader("org_id", iSettings.GetOrgID())
                    //             .PostUrlEncodedAsync(bodyParam).ReceiveJson<T>();
                    return await usedURL
                    .ConfigureRequest(c => { c.BeforeCall = BeforeCallDelegateToken; c.AfterCall = AfterCallDelegate; })
                      .AppendPathSegments(controller, action)
                      .PostUrlEncodedAsync(bodyParam).ReceiveJson<T>();
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }
        public async Task<T> GetAPICallAsync<T>(string controller, string action, string urlAmmendement)
        {
            try
            {
                var usedURL = iSettings.APIURL + urlAmmendement;
                if (GlobalSettings.HasProxyURL && !string.IsNullOrWhiteSpace(AppSharedPrefences.Instance.ProxyURL))
                {
                    FlurlHttp.Configure(settings =>
                    {
                        settings.HttpClientFactory = new ProxyHttpClientFactory(AppSharedPrefences.Instance.ProxyURL);
                    });
                }
                else
                {
                    FlurlHttp.ConfigureClient(usedURL, cli =>
                 cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
                }
                return await policyRetry.ExecuteAsync(async () =>
                {
                    if (!iSettings.IsConnected()) throw new NoConnectionException();
                    return await usedURL
                        .AppendPathSegments(controller, action)
                        .ConfigureRequest(c => { c.BeforeCall = BeforeCallDelegate; c.AfterCall = AfterCallDelegate; })
                        .GetAsync()
                        .ReceiveJson<T>();
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw ex;
            }
        }

        #region Delegates
        private void BeforeCallDelegateToken(FlurlCall Call)
        {
            //Call.Request.Headers.Add("did", iSettings.GetDID());
            //Call.Request.Headers.Add("org_id", iSettings.GetOrgID());
        }
        private void BeforeCallDelegate(FlurlCall Call)
        {
            // Append headers here
            //var langEnm = (LanguageEnum)Enum.Parse(typeof(LanguageEnum), AppSharedPrefences.Instance.SelectedLanguage.ToString());
            //var apiLanguage = "";
            //switch (langEnm)
            //{
            //    case LanguageEnum.Ar:
            //        apiLanguage = "ar";
            //        break;
            //    case LanguageEnum.En:
            //        apiLanguage = "en";
            //        break;
            //    default:
            //        apiLanguage = "en";
            //        break;
            //}
            //Call.Request.Headers.Add("Accept-Language", apiLanguage);
            //Call.Request.Headers.Add("org_id", iSettings.GetOrgID());
            //Call.Request.Headers.Add("did", iSettings.GetDID());
            //Call.Request.Headers.Add("Ocp-Apim-Subscription-Key", iSettings.GetOcpApimSubsrictionKey());
        }
        private void AfterCallDelegate(FlurlCall Call)
        {
            try
            {
                // Utilities.Log.DebugLogFile.LogObjectToFile(string.Format("Response Data:", httpContextAccessor?.HttpContext?.TraceIdentifier));
                // Utilities.Log.DebugLogFile.LogObjectToFile(Call.Response.Headers);
            }
            catch
            {

            }
            //if (Call.Response != null && Call.Response.Headers.TryGetValues("ResponseStatus", out values) && values?.Count() > 0)
            //{
            //    throw new FlurlHttpException(Call, values.FirstOrDefault(), new Exception(Call.Response.Headers.TryGetValues("ResponseMessage", out message) ? message.FirstOrDefault() : ""));
            //}
        }
        #endregion
    }


}
