using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using MiniJSON;

namespace TLClient.Net
{

    public class HttpMsgUtils
    {

        public static string DEFAULT_PUBLICKEY = "305C300D06092A864886F70D0101010500034B003048024100B45FDA308CB09152CE496D971128DDC4426BADEAD32AE36CE5BAA601DCB655536F71E038AB27AA025ED7B959D0A58388E3B120ACC8EF3CF2CF991006E6B8A5EB0203010001";
        public static string DEFAULT_RSAMOD = "9446975077577915259011074484362631336959847878979744841824112327212841217175239862620933003732561807083079816738401533735419657074389096559127020850161131";
        public static string DEFAULT_PUBLIC_E = "65537";

        public static string GetRSAString(string text, string publicKey, string modulus)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters param = new RSAParameters();
            param.Exponent = GetBytes(publicKey);
            param.Modulus = GetBytes(modulus);
            rsa.ImportParameters(param);
            byte[] enBytes = rsa.Encrypt(UTF8Encoding.UTF8.GetBytes(text), false);
            return ParseByte2HexStr(enBytes);
        }

        public static string GetRSAStringWithDefaultKey(string text)
        {
            return GetRSAString(text, DEFAULT_PUBLIC_E, DEFAULT_RSAMOD);
        }

        public static string AddRoot(string str, string root = "root")
        {
            //string a="<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            //string before=a+"<"+root+" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">";
            string before = "<" + root + ">";
            string after = "</" + root + ">";
            return (before + str + after);
        }

        public static T ParseXmlString<T>(string xmlString) where T : class
        {
            Type type = typeof(T);
            StringReader sr = new StringReader(xmlString);
            //声明序列化对象实例serializer
            XmlSerializer serializer = new XmlSerializer(type);
            //反序列化，并将反序列化结果值赋给变量
            T msg = serializer.Deserialize(sr) as T;
            return msg;
        }

        public static object ParseXmlString(Type type , string xmlString) 
        {
            StringReader sr = new StringReader(xmlString);
            //声明序列化对象实例serializer
            XmlSerializer serializer = new XmlSerializer(type);
            //反序列化，并将反序列化结果值赋给变量
            var msg = serializer.Deserialize(sr);
            return msg;
        }

        public static T ParseJsonString<T>(string jsonString) where T : class, new()
        {
            var result = Json.Deserialize(jsonString) as Dictionary<string, object>;
            T message = new T();
            Type type = message.GetType();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var prop = type.GetProperty(item.Key);
                    if (prop != null)
                    {
                        prop.SetValue(message, Convert.ChangeType(item.Value, prop.PropertyType), null);
                    }
                }
            }
            //else
            //{
            //    log.Error("Json Parse error !!! \n" + jsonString);
            //}
            return message;
        }

        /**
         * 将16进制转换为二进制
         * 
         * @param hexStr
         * @return
         */
        public static byte[] ParseHexStr2Byte(string hexStr)
        {
            if (hexStr.Length < 1)
                return null;
            byte[] result = new byte[hexStr.Length / 2];
            for (int i = 0; i < hexStr.Length / 2; i++)
            {
                int high = Convert.ToInt32(hexStr.Substring(i * 2, 1), 16);
                int low = Convert.ToInt32(hexStr.Substring(i * 2 + 1, 1), 16);
                result[i] = (byte)(high * 16 + low);
            }
            return result;
        }

        /**
         * 将二进制转换成16进制
         * 
         * @param buf
         * @return
         */
        public static string ParseByte2HexStr(byte[] buf)
        {
            string s = "";
            for (int i = 0; i < buf.Length; i++)
            {
                String hex = Convert.ToString(buf[i] & 0xFF, 16);
                if (hex.Length == 1)
                {
                    hex = '0' + hex;
                }
                s += hex.ToUpper();
            }
            return s;
        }

        public static byte[] GetBytes(String num)
        {
            BigInteger n = new BigInteger(num, 10);
            String s = n.ToString(2);
            if (s.Length % 8 > 0)
            {
                s = new String('0', 8 - s.Length % 8) + s;
            }
            byte[] data = new byte[s.Length / 8];
            String ocetstr;
            for (int i = 0; i < data.Length; i++)
            {
                ocetstr = s.Substring(8 * i, 8);
                data[i] = Convert.ToByte(ocetstr, 2);
            }
            return data;
        }

    }
}
