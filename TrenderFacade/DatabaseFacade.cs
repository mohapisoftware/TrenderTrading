using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace TrenderFacade
{
    public class DatabaseFacade
    {
        string ConnectionString;
        public DatabaseFacade(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            this.ConnectionString = @"Data Source = TrenderDB.db; Version = 3;";
        }
        public List<TrenderScheduledTask> GetScheduledTasksToRun()
        {
            var s = System.AppContext.BaseDirectory;
            List<TrenderScheduledTask> results = new List<TrenderScheduledTask>();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string commandText = "Select * from ScheduledTask";
                using (SQLiteCommand comm = new SQLiteCommand(commandText, conn))
                {
                    SQLiteDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        TrenderScheduledTask scheduledTask = new TrenderScheduledTask();

                        scheduledTask.ScheduledTaskID =Convert.ToInt32( reader["ScheduledTaskID"]);
                        scheduledTask.Symbol = Convert.ToString(reader["Symbol"]);
                        scheduledTask.TaskID = Convert.ToInt32(reader["TaskID"]);
                        scheduledTask.TaskName = Convert.ToString(reader["TaskName"]);
                        scheduledTask.TimeFrame = Convert.ToInt32(reader["TimeFrame"]);

                        results.Add(scheduledTask);
                    }
                }
            }
            return results;
        }
    }

    public class TrenderTask
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
    }
    public class TrenderScheduledTask
    {
        public int ScheduledTaskID { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string Symbol { get; set; }
        public int TimeFrame { get; set; }
    }

}
