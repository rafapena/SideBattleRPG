﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utilities
{
    public interface ObjectOperations
    {
        void InitializeNew();
        void Automate();
        string ValidateInputs();
        void Create();
        void Read();
        void Update();
        void Delete();
        void Clone();
    }
}
