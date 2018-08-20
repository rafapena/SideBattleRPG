using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Database.Utilities
{
    public interface ObjectOperations
    {
        void InitializeNew();
        string ValidateInputs();
        void ParameterizeInputs();
        void ParameterizeInput(string parameterized, string input);
        void Read();
    }

    public interface ObjectClassOperations : ObjectOperations
    {
        void Create();
        void Update();
        void Delete();
        void Clone();
    }

    public interface ObjectTemplateOperations : ObjectOperations
    {
        void Create(SQLiteConnection conn);
        void Update(SQLiteConnection conn);
        void Delete(SQLiteConnection conn);
        void Clone(SQLiteConnection conn);
    }
}
