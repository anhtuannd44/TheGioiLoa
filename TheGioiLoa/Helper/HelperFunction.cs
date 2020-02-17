using System;
using System.Text;
using System.Text.RegularExpressions;

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

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');

        }
    }
}