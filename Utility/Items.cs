using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll
{
    
    #region Nested type: Items
 
        public class Items
        {
            private string _AliasName;
            private string _RelationName;
            private string _CategoryName;
            private int _Id;
            private int _TableId;

            public Items(int Id, string AliasName,string CategoryName, string RelationName, int TableId)
            {
                _AliasName = AliasName;
                _RelationName = RelationName;
                _Id = Id;
                _TableId = TableId;
                _CategoryName = CategoryName;
            }
 
            public int ID
            {
                get { return _Id; }
            }

            public string AliasName
            {
                get { return _AliasName; }
            }
            public string CategoryName
            {
                get { return _CategoryName; }
            }
            public string RelationName
            {
                get { return _RelationName; }
            }

            public int TableId
            {
                get { return _TableId; }
            }
        }
 
        #endregion
}
 
   