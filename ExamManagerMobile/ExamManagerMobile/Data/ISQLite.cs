using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamManagerMobile.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetDBConnection();
    }
}
