using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.WarehouseTHModels
{
    /// <summary>
    /// 0：固定窗体
    /// 1：普通窗体
    /// </summary>
    public enum FormType
    {
        集中监控 = 1,
        实时趋势 = 1,
        参数配置 = 0,
        历史趋势 = 0,
        报警记录 = 0,
        数据报表 = 0,
        用户管理 = 0
    }
}
