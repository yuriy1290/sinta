using Microsoft.Data.SqlClient;

namespace ApiSocialNetwork.Models
{
    public class DatabaseBackup
    {
        public static void BackupDatabase(string connectionString, string backupPath)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"BACKUP DATABASE [SocialNetwork] TO DISK = '{backupPath}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
