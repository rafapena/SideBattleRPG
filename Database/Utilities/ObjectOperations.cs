using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utilities
{
    public interface ObjectOperations
    {
        void Automate();
        string ValidateInputs();
        void InitializeNew();
        void Create();
        void Read();
        void Update();
        void Delete();
        void Clone();
    }
}
