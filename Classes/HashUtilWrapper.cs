using Ease.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ease.Extract.Web.Classes
{
    public static class HashUtilWrapper
    {
        private static byte[] _salt = Encoding.UTF8.GetBytes("BE&j2a");

        public static string Hash(string plainText)
        {
            return HashUtil.Hash(plainText, _salt);
        }
    }
}