using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RETEKDemo.Models;

namespace RETEKDemo.DataProvider
{
    public class MessageDataProvider : IMessageDataProvider
    {
        private readonly string connectionString = @"Server=DESKTOP-2T3T4QM\SQLEXPRESS;Database=RETEK_Demo;Trusted_Connection=True;";

        private SqlConnection sqlConnection;

        public async Task<bool> IsCorrectParentId(int parentId)
        {
            using (var sqlConnection = new SqlConnection(connectionString)) {
                var sql = @"SELECT COUNT(*) FROM dbo.messages WHERE id = @parentId";
                int itemCount = await sqlConnection.ExecuteScalarAsync<int>(sql, new { parentId });
                if (itemCount > 0)
                    return true;
                return false;
            }
        }

        public async Task<Messages> AddMessage(Messages message)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                var sql = @"INSERT INTO dbo.messages (
                    msg,
                    parent_id)
                    VALUES (
                        @Msg,
                        @Parent_Id
                    )
                    SELECT * FROM dbo.messages
                    WHERE id = SCOPE_IDENTITY()
                    ";
                return await sqlConnection.QuerySingleOrDefaultAsync<Messages>(sql, message);
            }
        }

        public async Task<IEnumerable<Messages>> GetMessages(int parentId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                var sql = @"WITH Recursive (id, msg, parent_id)
                            AS
                            (
                                SELECT m1.id, m1.msg, m1.parent_id
                                FROM dbo.messages m1
                                WHERE m1.id = @parentId
                                UNION ALL
                                SELECT m2.id, m2.msg, m2.parent_id
                                FROM dbo.messages m2
                                    JOIN Recursive r ON m2.parent_id = r.id
                            )
                            SELECT id, msg, parent_id
                            FROM Recursive r";

                return await sqlConnection.QueryAsync<Messages>(sql, new { parentId });
            }
        }
    }
}
