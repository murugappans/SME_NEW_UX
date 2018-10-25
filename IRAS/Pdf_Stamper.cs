using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text.pdf;

namespace IRAS
{
    public class Pdf_Stamper: PdfStamper
    {
        AcroFields acro;
        public Pdf_Stamper(PdfReader reader,Stream Os):base(reader,Os)
        {
            acro = this.AcroFields;
        }




        //public  bool Gsetfields<T>(string Name, T Value)
        //{
        //    bool _result;

        //    if (typeof(T) != typeof(String))
        //    {
        //        if (Value <= 0)
        //        {
        //            _result = acro.SetField(Name, "");
        //        }
        //        else
        //        {
        //            _result = acro.SetField(name, Value);
        //        }
        //    }
        //    else
        //    {
        //        _result = acro.SetField(name, Value);
        //    }


        //    return _result;
        //}
     
        public bool set_fields_Int(string name, int Value)
        {

            string str ="";
            if(Value!=0)
            {
              str=  Convert.ToString(Value);
               return acro.SetField(name, str);
            }
            else
            {
             return   acro.SetField(name, str);
            }
        }
        public bool set_fields_str(string name, string Value)
        {

           return acro.SetField(name, Value);
        }

        public bool set_fields_bool(string name,bool _boolvalue)
        {

            string yesno = "NO";
           

            if (_boolvalue)
            {
                yesno = "Yes";
               
            }
            return acro.SetField(name, yesno);

        }


    }
}
