using System.Data.SQLite;

namespace Database.Utilities
{
    /// <summary>
    /// An interfae used by Classes, ClassesUnstructured, ClassTemplates, and TableTemplates. This interface enforces the CRUD operations.
    /// It is advisable to never directly use ObjectOperations. It is supposed to act as a superclass
    /// Classes and ClassUnstructured use ObjectPageOperations
    /// ClassTemplates and TableTemplates use ObjectTemplateOperations
    /// </summary>
    public interface ObjectOperations
    {
        void InitializeNew();       // Gets called when the "Add New" button is clicked.It sets up a the creation of a new row.
        string ValidateInputs();    // Checks if all of the input data is valid, before creating or updating the row.
        void ParameterizeInputs();  // Sanitizes the input data. Works with ValidateInputs() to secure data from malformed user inputs.
        void Read();                // Reads the content of the selected database table row
    }

    public interface ObjectPageOperations : ObjectOperations
    {
        void Create();  // Creates a new table row
        void Update();  // Updates the table row
        void Delete();  // Deletes the table row
        void Clone();   // Clones the table row: Does not do anything on its own - Create() is in charge of maintaining this function
    }

    public interface ObjectTemplateOperations : ObjectOperations
    {
        void Create(SQLiteConnection conn);
        void Update(SQLiteConnection conn);
        void Delete(SQLiteConnection conn);
        void Clone(SQLiteConnection conn);
    }
}
