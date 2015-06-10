using System;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Http
{
	public class WebRequestHandlerWithClientcertificates : HttpClientHandler
	{
		bool allowPipelining;
		RequestCachePolicy cachePolicy;
		AuthenticationLevel authenticationLevel;
		TimeSpan continueTimeout;
		TokenImpersonationLevel impersonationLevel;
		int maxResponseHeadersLength;
		int readWriteTimeout;
		RemoteCertificateValidationCallback serverCertificateValidationCallback;
		bool unsafeAuthenticatedConnectionSharing;
		private X509CertificateCollection clientCertificates;

		public void WebRequestHandler()
		{
			allowPipelining = true;
			authenticationLevel = AuthenticationLevel.MutualAuthRequested;
			cachePolicy = System.Net.WebRequest.DefaultCachePolicy;
			continueTimeout = TimeSpan.FromMilliseconds (350);
			impersonationLevel = TokenImpersonationLevel.Delegation;
			maxResponseHeadersLength = HttpWebRequest.DefaultMaximumResponseHeadersLength;
			readWriteTimeout = 300000;
			serverCertificateValidationCallback = null;
			unsafeAuthenticatedConnectionSharing = false;
		}

		public bool AllowPipelining {
			get { return allowPipelining; }
			set {
				//EnsureModifiability ();
				allowPipelining = value;
			}
		}

		public RequestCachePolicy CachePolicy {
			get { return cachePolicy; }
			set {
				//EnsureModifiability ();
				cachePolicy = value;
			}
		}

		public AuthenticationLevel AuthenticationLevel {
			get { return authenticationLevel; }
			set {
				//EnsureModifiability ();
				authenticationLevel = value;
			}
		}

		public X509CertificateCollection ClientCertificates {
			get {
				if (clientCertificates==null) {
					clientCertificates = new X509CertificateCollection();
				}
				return clientCertificates;
			}
			set {
				if (value==null) {
					throw new ArgumentNullException("value");
				}
				//EnsureModifiability ();
				clientCertificates = value;
			}
		}

		//[MonoTODO]
		public TimeSpan ContinueTimeout {
			get { return continueTimeout; }
			set {
				//EnsureModifiability ();
				continueTimeout = value;
			}
		}

		public TokenImpersonationLevel ImpersonationLevel {
			get { return impersonationLevel; }
			set {
				//EnsureModifiability ();
				impersonationLevel = value;
			}
		}

		public int MaxResponseHeadersLength {
			get { return maxResponseHeadersLength; }
			set {
				//EnsureModifiability ();
				maxResponseHeadersLength = value;
			}
		}

		public int ReadWriteTimeout {
			get { return readWriteTimeout; }
			set {
				//EnsureModifiability ();
				readWriteTimeout = value;
			}
		}

		//[MonoTODO]
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback {
			get { return serverCertificateValidationCallback; }
			set {
				//EnsureModifiability ();
				serverCertificateValidationCallback = value;
			}
		}

		public bool UnsafeAuthenticatedConnectionSharing {
			get { return unsafeAuthenticatedConnectionSharing; }
			set {
				//EnsureModifiability ();
				unsafeAuthenticatedConnectionSharing = value;
			}
		}

		/*internal override HttpWebRequest CreateWebRequest (HttpRequestMessage request)
		{
			HttpWebRequest wr = base.CreateWebRequest(request);

			wr.Pipelined = allowPipelining;
			wr.AuthenticationLevel = authenticationLevel;
			wr.CachePolicy = cachePolicy;
			wr.ImpersonationLevel = impersonationLevel;
			wr.MaximumResponseHeadersLength = maxResponseHeadersLength;
			wr.ReadWriteTimeout = readWriteTimeout;
			wr.UnsafeAuthenticatedConnectionSharing = unsafeAuthenticatedConnectionSharing;
			// here : maybe wr.ClientCertificates = ClientCertificates if the line below throws an error in your tests
			wr.ClientCertificates.Add(ClientCertificates);

			return wr;
		}
		*/
	}
}


