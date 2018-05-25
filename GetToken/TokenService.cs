using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Threading.Tasks;

namespace GetToken
{
    public static class TokenService
    {
        const string ServiceUrl = "";

        public static async Task<string> GetTokenAsync(string username, SecureString password)
        {
            var request = (HttpWebRequest)WebRequest.Create(ServiceUrl);

            request.Method = "AUTH";

            IntPtr passwordPtr = IntPtr.Zero;

            try
            {
                passwordPtr = Marshal.SecureStringToBSTR(password);
                request.Headers.Add("X-AUTH-PWD", Marshal.PtrToStringBSTR(passwordPtr));
            }
            finally
            {
                Marshal.FreeBSTR(passwordPtr);
            }

            request.Headers.Add("X-AUTH-USR", username);

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var serializer = new DataContractJsonSerializer(typeof(AccessToken));
                        var pair = serializer.ReadObject(stream) as AccessToken;

                        return pair.Token;
                    }
                }
                else
                {
                    throw new WebException(response.StatusCode.ToString());
                }
            }
        }
    }
}
