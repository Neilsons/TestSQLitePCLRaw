using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SQLite;

namespace Test
{
    public  static class DapperContextServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperDBContext(this IServiceCollection services, Action<DapperContextOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            //处理配置文件
            DapperContextOptions options = new DapperContextOptions();
            setupAction(options);

            if (!options.Configuration.Contains(options.DataBaseAddress))
            {
                throw new ArgumentException(nameof(options.DataBaseAddress));
            }
            SQLitePCL.Batteries_V2.Init();
            var connectionString = new SqliteConnectionStringBuilder(options.Configuration)
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = "123"
            }.ToString();
            IDbConnection connection = new SqliteConnection(connectionString);
            if (!File.Exists(options.DataBaseAddress))
            {
                //创建database文件
                SQLiteConnection.CreateFile(options.DataBaseAddress);
                //加密
                //创建表
                connection.Open();
                var createTableSqlStr = @"CREATE TABLE ""SdkACLog"" (
                                      ""Id"" INT NOT NULL,
                                      ""VersionId"" VARCHAR(36)
                                      );";
                var result = connection.Execute(createTableSqlStr);
                if (result < 0)
                {
                    throw new ArgumentNullException(nameof(options.DataBaseAddress));
                }
                connection.Close();
                connection.Dispose();
            }
            services.AddOptions();
            services.Configure(setupAction);
            return services;
        }
    }
}
