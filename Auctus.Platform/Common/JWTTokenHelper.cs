using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPlatform.Common
{
    public static class JWTTokenHelper
    {

        public static int SetTimeOut(int t)
        {
            if (t==0)
            {
                t = ConfigHelper.GetConfigInt("TokenTimeOut");//过期时间：分钟
            }
            return t;
        }
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="dic">用户信息</param>
        /// <returns></returns>
        public static string GenerateToken(Dictionary<string,object> dic,int timeout)
        {            
            JWT.Builder.JwtBuilder b = new JWT.Builder.JwtBuilder();
            string secret = ConfigHelper.GetConfigString("JWTSecret");
            if (string.IsNullOrEmpty(secret))
            {
                throw new Exception("Token密钥未设置！");
            }
            if (timeout==0)
            {
                timeout = 60;
            }
            //用户信息
            var payload = dic;
            //过期时间
            dic.Add("exp",UnixTimeStampUTC(DateTime.UtcNow.AddMinutes(timeout)));
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret);
            return token;
        }
        /// <summary>
        /// 验证token是否有效
        /// </summary>
        /// <param name="token">token</param>
        public static Dictionary<string,object> ValidateToken(string token)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string secret = ConfigHelper.GetConfigString("JWTSecret");
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var json = decoder.Decode(token, secret, verify: true);
                result.Add("S", true);
                result.Add("M", "");
            }
            catch (TokenExpiredException)
            {
                //TODO:Token验证返回信息
                Console.WriteLine("Token has expired");
                result.Add("S", false);
                result.Add("M", "Token has expired");
            }
            catch (SignatureVerificationException)
            {
                //TODO:Token验证返回信息
                Console.WriteLine("Token has invalid signature");
                result.Add("S", false);
                result.Add("M", "Token has invalid signature");
            }
            catch (Exception)
            {
                result.Add("S",false);
                result.Add("M","令牌格式不对！");
            }
            return result;
        }
        /// <summary>
        /// 日期转换成秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long UnixTimeStampUTC(DateTime dateTime)
        {
            Int32 unixTimeStamp;
            DateTime zuluTime = dateTime.ToUniversalTime();
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            unixTimeStamp = (Int32)(zuluTime.Subtract(unixEpoch)).TotalSeconds;
            return unixTimeStamp;
        }
    }
}
