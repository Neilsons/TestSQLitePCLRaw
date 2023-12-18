using Microsoft.Extensions.Options;

namespace Test
{
    public class DapperContextOptions : IOptions<DapperContextOptions>
    {
        public string Configuration { get; set; }
        /// <summary>
        /// 数据库地址
        /// </summary>
        public string DataBaseAddress { get; set; }

        DapperContextOptions IOptions<DapperContextOptions>.Value
        {
            get { return this; }
        }
    }
}
