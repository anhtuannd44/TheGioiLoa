﻿using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI;

namespace TheGioiLoa.Helper
{
    public class HelperFunction
    {
        public string DeleteSpace(string chuoi)
        {
            StringBuilder kq = new StringBuilder();
            chuoi = chuoi.Trim();
            for (int i = 0; i < chuoi.Length; i++)
            {
                kq.Append(chuoi[i]);
                if (chuoi[i] == ' ')
                {
                    while (chuoi[i] == ' ')
                    {
                        i++;
                    }
                    kq.Append(chuoi[i]);
                }
            }
            return kq.ToString();
        }

        public string CreateUrl(string text)
        {
            for (int i = 32; i < 48; i++)
            {

                text = text.Replace(((char)i).ToString(), " ");

            }
            text = text.Replace(".", "-");

            text = text.Replace(" ", "-");

            text = text.Replace(",", "-");

            text = text.Replace(";", "-");

            text = text.Replace(":", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();

        }

        public string RandomString()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 15; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public string GetYoutubeVideoId(string link)
        {
            link = link.Remove(0, link.IndexOf('=') + 1);
            var index2 = link.IndexOf('&');
            if (index2 != -1)
                link = link.Remove(index2);
            return link;
        }

    }
}