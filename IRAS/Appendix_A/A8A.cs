using System;
using System.Collections.Generic;

using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;

namespace IRAS.Appendix_A
{
    [Serializable]
    [XmlRoot("A8A",Namespace="http://www.iras.gov.sg/A8ADef")]  

    public class A8A
    {
        private A8AHeader _A8AHeader;
        private Details _Details;

        public A8A()
        {
        }

        public A8A(A8AHeader mA8AHeader, Details mDetails)
        {
            _A8AHeader = mA8AHeader;
            _Details = mDetails;
        }

        [XmlElement("A8AHeader")]
        public A8AHeader A8AHeader
        {
            get
            {
                if (_A8AHeader == null) _A8AHeader = new A8AHeader();
                return _A8AHeader;
            }
            set { _A8AHeader = value; }
        }


         [XmlElement("Details")]
        public Details Details
        {
            get
            {
                if (_Details == null) _Details = new Details();
                return _Details;
            }
            set { _Details = value; }
        }
         public static string Serialize(XmlSerializer serializer,Encoding encoding,
                            XmlSerializerNamespaces ns,
                            object objectToSerialize)
         {
             MemoryStream ms = new MemoryStream();
             XmlTextWriter xmlTextWriter = new XmlTextWriter(ms, encoding);
             xmlTextWriter.Formatting = Formatting.Indented;
             serializer.Serialize(xmlTextWriter, objectToSerialize, ns);
             ms = (MemoryStream)xmlTextWriter.BaseStream;
             return encoding.GetString(ms.ToArray());
         }

        //public static void SerializeToXml<T>(T obj, string fileName)
        //{

        //    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //    ns.Add("", "http://www.iras.gov.sg/A8ADef");


        //    XmlSerializer ser = new XmlSerializer(typeof(T));
        //    //Create a FileStream object connected to the target file   
        //    // FileStream fileStream = new FileStream(fileName, FileMode.Create);
        //    XmlWriterSettings stting = new XmlWriterSettings();
        //    stting.Encoding = Encoding.UTF8;
        //    using (XmlWriter xw = XmlWriter.Create(fileName, stting))
        //    {
        //        ser.Serialize(xw, obj, ns);
        //    }
        //    // fileStream.Close();

        //    //SqlXml sqlXml;
        //    // MemoryStream stream = new MemoryStream();
        //    // using (XmlWriter writer = XmlWriter.Create(stream))
        //    //  {
        //    //  ser.Serialize(writer, obj);
        //    // sqlXml = new SqlXml(stream);
        //    // }
        //    // return sqlXml;
        //}
        public static MemoryStream SerializeToXml<T>(T obj,MemoryStream ms)
        {

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://www.iras.gov.sg/A8ADef");

            StreamWriter streamWriter = new StreamWriter(ms, System.Text.Encoding.UTF8);

           

            XmlSerializer ser = new XmlSerializer(typeof(T));



            ser.Serialize(streamWriter, obj, ns);

            return ms;
           
        }


    }


  [Serializable]
    public class ESubmissionSDSC
    {
        
        
        public ESubmissionSDSC()
        {
        }


      

        public ESubmissionSDSC(FileHeaderST mFileHeaderST)
        {
            _FileHeaderST = mFileHeaderST;
            
        }

        private FileHeaderST _FileHeaderST;

        public FileHeaderST FileHeaderST
        {
            get
            {
                if (_FileHeaderST == null) _FileHeaderST = new FileHeaderST();
                return _FileHeaderST;
            }
            set { _FileHeaderST = value; }
        }

        


    }
    public class A8AHeader
    {

        public A8AHeader()
        {
        }

        public A8AHeader(ESubmissionSDSC mESubmissionSDSC)
        {
            _ESubmissionSDSC = mESubmissionSDSC;
        }
        private ESubmissionSDSC _ESubmissionSDSC;



     
       
        public ESubmissionSDSC ESubmissionSDSC
        {
            get
            {
                if (_ESubmissionSDSC == null) _ESubmissionSDSC = new ESubmissionSDSC();
                return _ESubmissionSDSC;
            }
            set { _ESubmissionSDSC = value; }
        }





    }

    public class ESubmissionSDSC_D
    {

        public ESubmissionSDSC_D()
        {
         }
        public ESubmissionSDSC_D(List<A8AST> mA8AST)
        {
            _A8AST = mA8AST;
        }

        private List<A8AST> _A8AST;
        public List<A8AST> A8AST
        {
            get
            {
                //if (_A8AST == null) _A8AST = new List<A8AST>();
                return _A8AST;
            }
            set { _A8AST = value; }
        }
    }
    public class Details
    {
        public Details()
        {

        }
        public Details(A8ARecord mA8ARecord)
        {
            _A8ARecord = mA8ARecord;
        }
        private A8ARecord _A8ARecord;

        public A8ARecord A8ARecord
        {
            get
            {
                if (_A8ARecord == null) _A8ARecord = new A8ARecord();
                return _A8ARecord;
            }
            set { _A8ARecord = value; }
        }
    }

    public class A8ARecord
    {
        public A8ARecord()
        {
        }
        public A8ARecord(ESubmissionSDSC_D mESubmissionSDSC_D)
        {
            _ESubmissionSDSC_D = mESubmissionSDSC_D;
        }

        
        private ESubmissionSDSC_D _ESubmissionSDSC_D;

     
        public ESubmissionSDSC_D ESubmissionSDSC
        {
            get
            {
                if (_ESubmissionSDSC_D == null) _ESubmissionSDSC_D = new ESubmissionSDSC_D();
                return _ESubmissionSDSC_D;
            }
            set { _ESubmissionSDSC_D = value; }
        }
    }



























}