using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records.Langs
{
    public class LangTextRelator
    {
        public static string FetchQuery = "SELECT * FROM langs";
    }

    [TableName("langs")]
    public class LangText : IAutoGeneratedRecord
    {
        // Primitive properties

        #region ILangText Members

        [PrimaryKey("Id", false)]
        public uint Id
        {
            get;
            set;
        }

        
        [NullString]
        public string French
        {
            get;
            set;
        }

        
        [NullString]
        public string English
        {
            get;
            set;
        }

        
        [NullString]
        public string German
        {
            get;
            set;
        }

        
        [NullString]
        public string Spanish
        {
            get;
            set;
        }

        
        [NullString]
        public string Italian
        {
            get;
            set;
        }

        
        [NullString]
        public string Japanish
        {
            get;
            set;
        }

        
        [NullString]
        public string Dutsh
        {
            get;
            set;
        }

        
        [NullString]
        public string Portugese
        {
            get;
            set;
        }

        
        [NullString]
        public string Russish
        {
            get;
            set;
        }

        #endregion
    }
}