namespace Database
{
    public static class AccessDB
    {
        // Indicates the file where the database is located
        public static System.Data.SQLite.SQLiteConnection Connect()
        {
            return new System.Data.SQLite.SQLiteConnection(@"data source=C:\Users\User\SideBattleRPG.db; Version=3; foreign keys=true;");
        }
    }
}
