using xbd.NodeSetting.Common;

namespace xbd.WarehouseTHPro
{
    public partial class FrmMain : Form
    {
        /// <summary>已创建的页面缓存：标题 → 窗体</summary>
        private readonly Dictionary<string, Form> _pages = new();

        private readonly Dictionary<string, bool> _formDict = new()
        {
            {"集中监控",  true},
            {"实时趋势",  true},
            {"参数配置",  false},
            {"历史趋势",  false},
            {"报警记录",  false},
            {"数据报表",  false},
            {"用户管理",  false},
        };

        public FrmMain()
        {
            InitializeComponent();

            // 左侧菜单所有按钮统一订阅同一个事件
            foreach (Control c in pnlMenu.Controls)
            {
                if (c is Button btn)
                    btn.Click += MenuButton_Click;
            }

            // TODO: 以后在这里补充用户登录场景
            ShowPage("集中监控");
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Load += FrmMain_Load;
            string configPath = Path.Combine(
    @"D:\.netStudy\winforms\温湿度监控系统\xbd.WarehouseTHPro\xbd.WarehouseTHPro",
    "Config");
            var result = ModbusRTUCFG.LoadDevice(configPath);
            if (result.IsSuccess)
            {
                var List = result.Content;
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        /// <summary>根据标题创建对应的窗体（每个页面第一次打开时调用）</summary>
        private static Form? CreatePage(string title)
        {
            switch (title)
            {
                case "集中监控": return new FrmCentralMonitor();
                case "实时趋势": return new FrmRealtimeTrend();
                case "参数配置": return new FrmParamConfig();
                case "历史趋势": return new FrmHistoryTrend();
                case "报警记录": return new FrmAlarmRecord();
                case "数据报表": return new FrmDataReport();
                case "用户管理": return new FrmUserManage();
                default: return null;   // 未知标题
            }
        }

        /// <summary>左侧菜单统一点击事件</summary>
        private void MenuButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;

            ShowPage(btn.Text.Trim());   // 按钮文字去掉前导空格 = 页面标题

            // 高亮当前激活的菜单按钮
            foreach (Control c in pnlMenu.Controls)
            {
                if (c is Button b)
                    b.BackColor = (b == btn)
                        ? Color.FromArgb(56, 59, 138)   // 选中
                        : Color.FromArgb(43, 50, 120);  // 未选中
            }
        }

        /// <summary>打开页面：第一次创建并嵌入 pnlContent，之后只切换显示</summary>
        private void ShowPage(string title)
        {
            //普通窗体： 还没创建过 → 先创建
            if (!_pages.TryGetValue(title, out var form))
            {
                form = CreatePage(title);
                if (form is null) return;

                //false: 将窗体降级为普通控件，允许嵌入Panel等容器，不再作为独立弹窗
                //true（默认）:独立 Windows 窗口，可以弹窗，不能赋值给 Parent
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                List<string> fixedKeys = _formDict
                    .Where(pair => pair.Value == false)  //筛选：sFixed=false
                    .Select(pair => pair.Key)   //投影：只取出key(窗体名称)
                    .ToList();
                for (int i = 0; i < _pages.Count; i++)
                {
                    foreach (string item in fixedKeys)
                    {
                        if (_pages.Keys.Contains(item))
                        {
                            _pages.Remove(item);
                        }
                    }
                }
                _pages[title] = form;
                pnlContent.Controls.Add(form);

            }

            //只有目标窗体可见，其余隐藏（不销毁，后台照常运行）
            foreach (var c in _pages)
            {
                c.Value.Visible = (c.Value == form);
            }

            form.BringToFront();
        }

        
    }
}
